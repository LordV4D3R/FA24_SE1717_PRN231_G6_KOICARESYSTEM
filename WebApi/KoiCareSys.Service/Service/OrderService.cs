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

namespace KoiCareSys.Service.Service
{
    public class OrderService
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
                var unitDTO = _mapper.Map<UnitDTO>(unit);
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, unitDTO);
            }
        }

        public async Task<IBusinessResult> Create(UnitDTO request)
        {
            Unit unit = _mapper.Map<Unit>(request);
            var result = await _unitOfWork.Unit.CreateAsync(unit);
            if (result > 0)
            {
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
            }
            else
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
        }

        public async Task<IBusinessResult> Update(UnitDTO request)
        {
            try
            {
                Unit unit = _mapper.Map<Unit>(request);
                var result = await _unitOfWork.Unit.UpdateAsync(unit);
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
