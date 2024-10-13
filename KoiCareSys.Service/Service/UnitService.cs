using AutoMapper;
using KoiCareSys.Common;
using KoiCareSys.Data;
using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;
using KoiCareSys.Serivice.Base;
using KoiCareSys.Service.Service.Interface;

namespace KoiCareSys.Service.Service
{
    public class UnitService : IUnitService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UnitService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAll()
        {
            #region Business Rule
            #endregion
            var unit = await _unitOfWork.Unit.GetAllAsync();
            if (unit == null)
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG);
            }
            else
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, unit);
            }
        }

        public async Task<IBusinessResult> GetById(Guid unitId)
        {
            #region Business ruke
            #endregion
            var unit = await _unitOfWork.Unit.GetByIdAsync(unitId);
            if (unit == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, unit);
            }
        }
        public async Task<IBusinessResult> Save(UnitDTO unitDTO)
        {
            Unit unit = _mapper.Map<Unit>(unitDTO);

            try
            {
                int result = -1;

                var UnitTmp = _unitOfWork.Unit.GetById(unit.UnitId);
                if (UnitTmp != null)
                {
                    #region Business rule
                    #endregion Business rule
                    result = await _unitOfWork.Unit.UpdateAsync(unit);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, unit);
                    }
                }
                else
                {
                    #region Business rule
                    #endregion Business rule

                    result = await _unitOfWork.Unit.CreateAsync(unit);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                    }
                }

            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<IBusinessResult> DeleteById(Guid unitId)
        {
            #region Business rule

            #endregion Business rule
            try
            {
                var unit = await _unitOfWork.Unit.GetByIdAsync(unitId);
                if (unit == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    var result = await _unitOfWork.Unit.RemoveAsync(unit);
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
