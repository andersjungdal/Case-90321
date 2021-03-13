using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Kundestyring.Interfaces
{
    public interface IKundestyringService
    {
        Task<(bool IsSuccess, IEnumerable<Models.Kundestyring> Kundestyrings, string ErrorMessage)> GetKundestyringsAsync();
        Task<(bool IsSuccess, Models.Kundestyring Kundestyring, string ErrorMessage)> GetKundestyringAsync(int id);
        Task<(bool IsSuccess, Db.Kundestyring Kundestyring, string ErrorMessage)> PostKundestyringAsync([FromBody] Models.Kundestyring Kundestyring);
        Task<(bool IsSuccess, Db.Kundestyring Kundestyring, string ErrorMessage)> DeleteKundestyringAsync(int id);
        //virker ikke
        //Task<(bool IsSuccess, Db.Product Product, string ErrorMessage)> UpdateProductAsync(int id, [FromBody] Product product);
    }
}