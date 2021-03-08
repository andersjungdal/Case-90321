using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Product_Service.Db;
using Product_Service.Models;

namespace Product_Service.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id);
        Task<(bool IsSuccess, Db.Product Product, string ErrorMessage)> PostProductAsync([FromBody] Models.Product product);
        Task<(bool IsSuccess, Db.Product Product, string ErrorMessage)> DeleteProductAsync(int id);
        //virker ikke
        //Task<(bool IsSuccess, Db.Product Product, string ErrorMessage)> UpdateProductAsync(int id, [FromBody] Product product);
    }
}