using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Varelagerstyring.Interfaces;

namespace Varelagerstyring.Providers
{
    public class VarelagerstyringService : IVarelagerstyringService
    {
        private readonly Db.VarelagerstyringDbContext dbContext;
        private readonly ILogger<VarelagerstyringService> logger;
        private readonly IMapper mapper;
        private readonly IConfigurationProvider configurationProvider;

        public VarelagerstyringService(Db.VarelagerstyringDbContext dbContext, ILogger<VarelagerstyringService> logger, IMapper mapper,
            IConfigurationProvider configurationProvider)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            this.configurationProvider = configurationProvider;
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Varelagerstyring> Varelagerstyring, string ErrorMessage)> GetVarelagerstyringsAsync()
        {
            try
            {
                logger?.LogInformation("Querying products");
                var products = await dbContext.Varelagerstyring.ToListAsync();
                if (products != null && products.Any())
                {
                    logger?.LogInformation($"{products.Count} product(s) found");
                    var result = mapper.Map<IEnumerable<Models.Varelagerstyring>>(products);
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

        public async Task<(bool IsSuccess, Models.Varelagerstyring Varelagerstyring, string ErrorMessage)> GetVarelagerstyringAsync(int id)
        {
            try
            {
                logger?.LogInformation($"Querying products with id: {id}");
                var product = await dbContext.Varelagerstyring.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    logger?.LogInformation("Product found");
                    var result = mapper.Map<Models.Varelagerstyring>(product);
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

        public async Task<(bool IsSuccess, Db.Varelagerstyring Varelagerstyring, string ErrorMessage)> PostVarelagerstyringAsync(Models.Varelagerstyring Varelagerstyring)
        {
            try
            {
                logger?.LogInformation("Creating products");
                var mapper = configurationProvider.CreateMapper();
                var newproduct = mapper.Map<Db.Varelagerstyring>(Varelagerstyring);
                if (newproduct != null)
                {
                    await dbContext.Varelagerstyring.AddAsync(newproduct);
                    await dbContext.SaveChangesAsync();
                    logger?.LogInformation($"product created {newproduct}");

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

        public async Task<(bool IsSuccess, Db.Varelagerstyring Varelagerstyring, string ErrorMessage)> DeleteVarelagerstyringAsync(int id)
        {
            try
            {
                var findproduct = dbContext.Varelagerstyring.SingleOrDefault(x => x.Id == id);
                if (findproduct == null) return (false, null, "Not updated");
                dbContext.Varelagerstyring.Remove(findproduct);
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