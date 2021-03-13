using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Billedstyring.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Billedstyring.Providers
{
    public class BilledstyringService : IBilledstyringService
    {
        private readonly Db.BilledstyringDbContext dbContext;
        private readonly ILogger<BilledstyringService> logger;
        private readonly IMapper mapper;
        private readonly IConfigurationProvider configurationProvider;

        public BilledstyringService(Db.BilledstyringDbContext dbContext, ILogger<BilledstyringService> logger, IMapper mapper,
            IConfigurationProvider configurationProvider)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            this.configurationProvider = configurationProvider;
        }


        public async Task<(bool IsSuccess, Models.Billedstyring Billedstyring, string ErrorMessage)> GetBilledstyringAsync(int id)
        {
            try
            {
                logger?.LogInformation($"Querying products with id: {id}");
                var product = await dbContext.Billedstyring.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    logger?.LogInformation("Product found");
                    var result = mapper.Map<Models.Billedstyring>(product);
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

        public async Task<(bool IsSuccess, IEnumerable<Models.Billedstyring> Billedstyrings, string ErrorMessage)> GetBilledstyringsAsync()
        {
            try
            {
                logger?.LogInformation("Querying products");
                var products = await dbContext.Billedstyring.ToListAsync();
                if (products != null && products.Any())
                {
                    logger?.LogInformation($"{products.Count} product(s) found");
                    var result = mapper.Map<IEnumerable<Models.Billedstyring>>(products);
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

        public async Task<(bool IsSuccess, Db.Billedstyring Billedstyring, string ErrorMessage)> PostBilledstyringAsync([FromBody] Models.Billedstyring Billedstyring)
        {
            try
            {
                logger?.LogInformation("Creating products");
                var mapper = configurationProvider.CreateMapper();
                var newproduct = mapper.Map<Db.Billedstyring>(Billedstyring);
                if (newproduct != null)
                {
                    await dbContext.Billedstyring.AddAsync(newproduct);
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

        public async Task<(bool IsSuccess, Db.Billedstyring Billedstyring, string ErrorMessage)> DeleteBilledstyringAsync(int id)
        {
            try
            {
                var findproduct = dbContext.Billedstyring.SingleOrDefault(x => x.Id == id);
                if (findproduct == null) return (false, null, "Not updated");
                dbContext.Billedstyring.Remove(findproduct);
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