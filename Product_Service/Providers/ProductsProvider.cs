using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Product_Service.Db;
using Product_Service.Domain.CommandHandlers;
using Product_Service.Interfaces;
using RabbitMQ.Bus.Bus.Interfaces;

namespace Product_Service.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly Db.ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;
        private readonly IConfigurationProvider configurationProvider;
        private readonly IEventBus _eventBus;

        public ProductsProvider(Db.ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper,
            IConfigurationProvider configurationProvider, IEventBus eventBus)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            this.configurationProvider = configurationProvider;
            _eventBus = eventBus;
        }


        public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                logger?.LogInformation($"Querying products with id: {id}");
                var product = await dbContext.Product.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    logger?.LogInformation("Product found");
                    var result = mapper.Map<Models.Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                logger?.LogInformation("Querying products");
                var products = await dbContext.Product.ToListAsync();
                if (products != null && products.Any())
                {
                    logger?.LogInformation($"{products.Count} product(s) found");
                    var result = mapper.Map<IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Db.Product Product, string ErrorMessage)> PostProductAsync([FromBody] Models.Product product)
        {
            try
            {
                logger?.LogInformation("Creating products");
                var mapper = configurationProvider.CreateMapper();
                var newproduct = mapper.Map<Db.Product>(product);
                if (newproduct != null)
                {
                    await dbContext.Product.AddAsync(newproduct);
                    await dbContext.SaveChangesAsync();
                    logger?.LogInformation($"product created {newproduct}");

                    var createPostCustomerCommand = new CreatePostProductServiceCommand(newproduct.Id, newproduct.Name, newproduct.Categories);
                    await _eventBus.SendCommand(createPostCustomerCommand);


                    return (true, newproduct, null);
                }
                return (false, null, "Not created");
            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Db.Product Product, string ErrorMessage)> DeleteProductAsync(int id)
        {
            try
            {
                var findproduct = dbContext.Product.SingleOrDefault(x => x.Id == id);
                if (findproduct == null) return (false, null, "Not updated");
                dbContext.Product.Remove(findproduct);
                await dbContext.SaveChangesAsync();
                logger?.LogInformation($"Deleted product {findproduct}");
                return (true, findproduct, null);

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}