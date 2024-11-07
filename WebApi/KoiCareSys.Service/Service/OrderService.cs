using AutoMapper;
using KoiCareSys.Common;
using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;
using KoiCareSys.Data;
using KoiCareSys.Serivice.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiCareSys.Service.Service.Interface;


namespace KoiCareSys.Service.Service
{
    public class OrderService :IOrderService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAll()
        {
            #region Business Rule
            #endregion
            var kois = await _unitOfWork.Order.GetAllAsync();
            if (kois != null)
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, kois);
            }
            else
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
        }

        public async Task<IBusinessResult> GetById(Guid unitId)
        {
            #region Business ruke
            #endregion
            var unit = await _unitOfWork.Order.GetByIdAsync(unitId);
            if (unit == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                var unitDTO = _mapper.Map<Order>(unit);
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, unitDTO);
            }
        }

        public async Task<IBusinessResult> Create(OrderDTO request)
        {
            Order unit = new Order
            {
                OrderDate = DateTime.Now,
                CreateDate = DateTime.Now,
                Status = "Chờ xác nhận",
                Total = 0

            };
            foreach (var od in request.OrderDetails)
            {
                var product = await _unitOfWork.Product.GetByIdAsync(od.ProductId);
                if (product == null)    
                    { continue; }

                unit.OrderDetails.Add(new OrderDetail
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    Name= product.Name,
                    Price = product.Price,
                    CreateDate= DateTime.Now,   
                    Subtotal = od.Quantity*product.Price,
                    UpdateDate= DateTime.Now,
                   
                });
               
            }

            unit.Total = unit.OrderDetails.Sum(od => od.Subtotal);
            var result = await _unitOfWork.Order.CreateAsync(unit);
            if (result > 0)
            {
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
            }
            else
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
        }

        public async Task<IBusinessResult> Update(OrderDTO request)
        {
            try
            {
                var unit = _mapper.Map<Order>(request);
                var result = await _unitOfWork.Order.UpdateAsync(unit);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    var unitDTO = _mapper.Map<UnitDTO>(request);
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, unitDTO);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
