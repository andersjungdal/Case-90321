using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Varelagerstyring.Interfaces
{
    public interface IVarelagerstyringService
    {
        Task<(bool IsSuccess, IEnumerable<Models.Varelagerstyring> Varelagerstyring, string ErrorMessage)> GetVarelagerstyringsAsync();
        Task<(bool IsSuccess, Models.Varelagerstyring Varelagerstyring, string ErrorMessage)> GetVarelagerstyringAsync(int id);
        Task<(bool IsSuccess, Db.Varelagerstyring Varelagerstyring, string ErrorMessage)> PostVarelagerstyringAsync([FromBody] Models.Varelagerstyring Varelagerstyring);
        Task<(bool IsSuccess, Db.Varelagerstyring Varelagerstyring, string ErrorMessage)> DeleteVarelagerstyringAsync(int id);
        //virker ikke
        //Task<(bool IsSuccess, Db.Product Product, string ErrorMessage)> UpdateProductAsync(int id, [FromBody] Product product);
    }
}