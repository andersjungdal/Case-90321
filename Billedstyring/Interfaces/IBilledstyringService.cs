using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Billedstyring.Interfaces
{
    public interface IBilledstyringService
    {
        Task<(bool IsSuccess, IEnumerable<Models.Billedstyring> Billedstyrings, string ErrorMessage)> GetBilledstyringsAsync();
        Task<(bool IsSuccess, Models.Billedstyring Billedstyring, string ErrorMessage)> GetBilledstyringAsync(int id);
        Task<(bool IsSuccess, Db.Billedstyring Billedstyring, string ErrorMessage)> PostBilledstyringAsync([FromBody] Models.Billedstyring Billedstyring);
        Task<(bool IsSuccess, Db.Billedstyring Billedstyring, string ErrorMessage)> DeleteBilledstyringAsync(int id);
        //virker ikke
        //Task<(bool IsSuccess, Db.Product Product, string ErrorMessage)> UpdateProductAsync(int id, [FromBody] Product product);
    }
}