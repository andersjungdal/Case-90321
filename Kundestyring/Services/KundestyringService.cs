using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kundestyring.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kundestyring.Services
{
    public class KundestyringService : IKundestyringService
    {
        private readonly Db.KundestyringDbContext dbContext;
        private readonly ILogger<KundestyringService> logger;
        private readonly IMapper mapper;
        private readonly IConfigurationProvider configurationProvider;

        public KundestyringService(Db.KundestyringDbContext dbContext, ILogger<KundestyringService> logger, IMapper mapper,
            IConfigurationProvider configurationProvider)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            this.configurationProvider = configurationProvider;
        }


        public async Task<(bool IsSuccess, Models.Kundestyring Kundestyring, string ErrorMessage)> GetKundestyringAsync(int id)
        {
            try
            {
                logger?.LogInformation($"Querying products with id: {id}");
                var product = await dbContext.Kundestyring.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    logger?.LogInformation("Product found");
                    var result = mapper.Map<Models.Kundestyring>(product);
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

        public async Task<(bool IsSuccess, IEnumerable<Models.Kundestyring> Kundestyrings, string ErrorMessage)> GetKundestyringsAsync()
        {
            try
            {
                logger?.LogInformation("Querying products");
                var products = await dbContext.Kundestyring.ToListAsync();
                if (products != null && products.Any())
                {
                    logger?.LogInformation($"{products.Count} Kundestyring(s) found");
                    var result = mapper.Map<IEnumerable<Models.Kundestyring>>(products);
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

        public async Task<(bool IsSuccess, Db.Kundestyring Kundestyring, string ErrorMessage)> PostKundestyringAsync([FromBody] Models.Kundestyring Kundestyring)
        {
            try
            {
                logger?.LogInformation("Creating products");
                var mapper = configurationProvider.CreateMapper();
                var newproduct = mapper.Map<Db.Kundestyring>(Kundestyring);
                if (newproduct != null)
                {
                    await dbContext.Kundestyring.AddAsync(newproduct);
                    await dbContext.SaveChangesAsync();
                    logger?.LogInformation($"Kundestyring created {newproduct}");

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

        public async Task<(bool IsSuccess, Db.Kundestyring Kundestyring, string ErrorMessage)> DeleteKundestyringAsync(int id)
        {
            try
            {
                var findproduct = dbContext.Kundestyring.SingleOrDefault(x => x.Id == id);
                if (findproduct == null) return (false, null, "Not updated");
                dbContext.Kundestyring.Remove(findproduct);
                await dbContext.SaveChangesAsync();
                logger?.LogInformation($"Deleted Kundestyring {findproduct}");
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