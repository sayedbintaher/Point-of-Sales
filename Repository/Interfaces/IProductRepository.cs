using PosAPI.Models;
using PosAPI.Models.Common;
using PosAPI.ViewModels;

namespace PosAPI.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductVM> GetProductById(int id);
        Task<ServiceResponse> GetAllProductList();
        Task<ServiceResponse> AddProduct(ProductCreateVM model);
        Task<ServiceResponse> UpdateProduct(ProductUpdateVM model);
        Task<ServiceResponse> DeleteProduct(int id);
    }
}
