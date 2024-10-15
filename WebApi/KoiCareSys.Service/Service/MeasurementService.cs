using AutoMapper;
using KoiCareSys.Common;
using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;
using KoiCareSys.Data;
using KoiCareSys.Serivice.Base;
using KoiCareSys.Service.Service.Interface;

namespace KoiCareSys.Service.Service
{
    public class MeasurementService : IMeasurementService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MeasurementService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAll()
        {
            #region Business Rule
            #endregion
            var measurements = await _unitOfWork.Measurement.GetAllAsync(includeProperties: "MeasureData");
            if (measurements == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                var measurementDTOs = _mapper.Map<List<MeasurementDTO>>(measurements);
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, measurementDTOs);
            }
        }

        public async Task<IBusinessResult> GetById(Guid measurementId)
        {
            #region Business ruke
            #endregion
            var measurement = await _unitOfWork.Measurement.GetByIdAsync(measurementId);
            if (measurement == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                var measurementDTO = _mapper.Map<MeasurementDTO>(measurement);
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, measurementDTO);
            }
        }

        public async Task<IBusinessResult> Save(MeasurementDTO request)
        {
            Measurement measurement = _mapper.Map<Measurement>(request);

            try
            {
                int result = -1;

                var MeasurementTmp = _unitOfWork.Measurement.GetById(measurement.MeasurementId);
                if (MeasurementTmp != null)
                {
                    #region Business rule
                    #endregion Business rule
                    result = await _unitOfWork.Measurement.UpdateAsync(measurement);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                    }
                    else
                    {
                        var measurementDTO = _mapper.Map<MeasurementDTO>(measurement);
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, measurementDTO);
                    }
                }
                else
                {
                    #region Business rule
                    #endregion Business rule

                    result = await _unitOfWork.Measurement.CreateAsync(measurement);

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

        public async Task<IBusinessResult> DeleteById(Guid measurementId)
        {
            #region Business rule

            #endregion Business rule
            try
            {
                var measurement = await _unitOfWork.Measurement.GetByIdAsync(measurementId);
                if (measurement == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    var result = await _unitOfWork.Measurement.RemoveAsync(measurement);
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
