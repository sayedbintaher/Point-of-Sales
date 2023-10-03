using System.Data;
using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PosAPI.Data;
using PosAPI.Models;
using PosAPI.Models.Common;
using PosAPI.Repository.Interfaces;
using PosAPI.UnitOfWork;
using PosAPI.Utilities;
using PosAPI.ViewModels;
using static PosAPI.Models.Common.AppConstants;

namespace PosAPI.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DapperContext _dapper;
        public ProductRepository(ApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper, DapperContext dapper)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dapper = dapper;
        }
        public async Task<ServiceResponse> AddProduct(ProductCreateVM model)
        {
            var productExist = await _db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Name == model.Name && x.StatusId == (byte)StatusId.Active);
            if (productExist != null)
            {
                return Utility.GetAlreadyExistMsg("Product");
            }
            var productToCreate = _mapper.Map<Product>(model);
            productToCreate.StatusId = (byte)StatusId.Active;
            productToCreate.CreatedBy = 1;
            productToCreate.CreatedAt = CommonMethods.GetBDCurrentTime();
            await _db.AddAsync(productToCreate);
            await _unitOfWork.CommitAsync();

            return Utility.GetSuccessMsg("Successfully saved", productToCreate);
        }

        public async Task<ServiceResponse> DeleteProduct(int id)
        {
            var productToDelete = await _db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.StatusId == (byte)StatusId.Active);
            if (productToDelete == null)
            {
                return Utility.GetNoDataFoundMsg("Product not found");
            }
            productToDelete.UpdatedBy = 1;
            productToDelete.UpdatedAt = CommonMethods.GetBDCurrentTime();
            productToDelete.StatusId = (byte)StatusId.Delete;
            await Task.Run(() => _db.Products.Update(productToDelete));
            await _unitOfWork.CommitAsync();

            return Utility.GetSuccessMsg("Successfully deleted", productToDelete);
        }

        public async Task<ServiceResponse> GetAllProductList()
        {
            using IDbConnection dbConnection = _dapper.CreateConnection();
            string sp = "exec sp_Product_GetAll";
            var products = await dbConnection.QueryAsync<ProductVM>(sp, CommandType.StoredProcedure);
            //var response = _mapper.Map<ProductVM>(product);
            return Utility.GetSuccessMsg("Success", products.ToList());
        }

        public async Task<ProductVM> GetProductById(int id)
        {
            using IDbConnection dbConnection = _dapper.CreateConnection();
            var sp = "exec sp_Product_GetById @id";
            var parameters = new { id = id };
            var product = await dbConnection.QueryFirstOrDefaultAsync<ProductVM>(sp, parameters);
            if (product == null)
            {
                return null;
            }
            return product;
        }

        public async Task<ServiceResponse> UpdateProduct(ProductUpdateVM model)
        {
            var productToUpdate = await _db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id && x.StatusId == (byte)StatusId.Active);
            if (productToUpdate == null)
            {
                return Utility.GetNoDataFoundMsg("Product not found");
            }
            _mapper.Map(model, productToUpdate);
            productToUpdate.UpdatedBy = 1;
            productToUpdate.UpdatedAt = CommonMethods.GetBDCurrentTime();
            await Task.Run(() => _db.Products.Update(productToUpdate));
            await _unitOfWork.CommitAsync();

            return Utility.GetSuccessMsg("Successfully updated", productToUpdate);
        }
    }
}
