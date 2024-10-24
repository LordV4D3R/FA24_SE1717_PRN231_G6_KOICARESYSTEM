using AutoMapper;
using Azure.Core;
using KoiCareSys.Common;
using KoiCareSys.Data;
using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;
using KoiCareSys.Serivice.Base;
using KoiCareSys.Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCareSys.Service.Service
{
    public class ProductService :IProductService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(UnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IBusinessResult> GetAll()
        {
            #region Business Rule
            #endregion
            var koi = await _unitOfWork.Product.GetAllAsync();
            if (koi != null)
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, koi);
            }
            else
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, koi);
            }
        }

        public async Task<IBusinessResult> GetById(Guid KoiNo)
        {
            #region Business ruke
            #endregion
            var koi = await _unitOfWork.Product.GetByIdAsync(KoiNo);
            if (koi == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Product());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, koi);
            }
        }

        public async Task<IBusinessResult> Create(ProductDTO request)
        {
            Product product = _mapper.Map<Product>(request);
                    product.CreateDate = DateTime.Now;
                    product.UpdateDate = DateTime.Now;
                    product.isDeleted = false;
                    product.TotalSold = 0;
            var result = await _unitOfWork.Product.CreateAsync(product);
            if (result > 0)
            {
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
            }
            else
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
        }

        public async Task<IBusinessResult> Update(ProductDTO request)
        {
            try
            {
                Product product = _mapper.Map<Product>(request);
                var result = await _unitOfWork.Product.UpdateAsync(product);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, product);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        //public async Task<IBusinessResult> Save(ProductDTO request)
        //{
        //    Product product = _mapper.Map<Product>(request);

        //    try
        //    {

        //        int result = -1;

        //        var KoiTmp = _unitOfWork.Product.GetById(product.Id);
        //        if (KoiTmp != null)
        //        {
        //            #region Business rule
        //            product.UpdateDate = DateTime.Now;
        //            #endregion Business rule
        //            result = await _unitOfWork.Product.UpdateAsync(product);
        //            if (result > 0)
        //            {
        //                return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, new Product());
        //            }
        //            else
        //            {
        //                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, product);
        //            }
        //        }
        //        else
        //        {
        //            #region Business rule
        //            product.CreateDate = DateTime.Now;
        //            product.UpdateDate = DateTime.Now;  

        //            #endregion Business rule

        //            result = await _unitOfWork.Product.CreateAsync(product);
        //            if (result > 0)
        //            {
        //                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG,product);
        //            }
        //            else
        //            {
        //                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
        //    }
        //}
        public async Task<IBusinessResult> DeleteById(Guid productid)
        {
            #region Business rule

            #endregion Business rule
            try
            {
                var koi = await _unitOfWork.Product.GetByIdAsync(productid);
                if (koi == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Product>());
                }
                else
                {
                    var result = await _unitOfWork.Product.RemoveAsync(koi);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
