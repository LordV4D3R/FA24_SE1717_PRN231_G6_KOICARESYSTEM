using KoiCareSys.Common;
using KoiCareSys.Data;
using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Enums;
using KoiCareSys.Data.Models;
using KoiCareSys.Serivice.Base;
using KoiCareSys.Service.Service.Interface;

namespace KoiCareSys.Service.Service
{

    public class KoiService : IKoiService
    {
        private readonly UnitOfWork _unitOfWork;

        public KoiService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IBusinessResult> DeleteById(Guid id)
        {
            #region Business rule

            #endregion Business rule
            try
            {
                var koi = await _unitOfWork.Koi.GetByIdAsync(id);
                if (koi == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    var result = await _unitOfWork.Koi.RemoveAsync(koi);
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

        public async Task<IBusinessResult> GetAll()
        {
            #region Business Rule
            #endregion
            var kois = await _unitOfWork.Koi.GetAllAsync();
            if (kois == null)
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG);
            }
            else
            {
                //var measurementDTOs = _mapper.Map<MeasurementDTO>(kois);
                //return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, measurementDTOs);
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, kois);
            }
        }

        public async Task<IBusinessResult> GetById(Guid id)
        {
            #region Business ruke
            #endregion
            var koi = await _unitOfWork.Koi.GetByIdAsync(id);
            if (koi == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                //var measurementDTO = _mapper.Map<MeasurementDTO>(koi);
                //return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, measurementDTO);
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, koi);
            }
        }

        public async Task<IBusinessResult> Save(KoiDTO koiDTO)
        {
            //Unit unit = _mapper.Map<Unit>(unitDTO);
            var koi = new Koi
            {
                Name = koiDTO.Name,
                Physique = koiDTO.Physique,
                Age = koiDTO.Age,
                Length = koiDTO.Length,
                Sex = koiDTO.Sex,
                Category = koiDTO.Category,
                InPondSince = koiDTO.InPondSince, // May 15, 2022
                PurchasePrice = koiDTO.PurchasePrice,
                Status = koiDTO.Status,
                ImgUrl = koiDTO.ImgUrl,
                Origin = koiDTO.Origin,
                Breed = koiDTO.Breed,
                PondId = koiDTO.PondId,
            };

            try
            {
                int result = -1;

                var koiTmp = _unitOfWork.Koi.GetById(koiDTO.Id);
                if (koiTmp != null)
                {
                    #region Business rule
                    #endregion Business rule
                    _unitOfWork.Koi.Update(koi);
                    result = await _unitOfWork.SaveChangesWithTransactionAsync();
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, koi);
                    }
                }
                else
                {
                    #region Business rule
                    #endregion Business rule

                    _unitOfWork.Koi.Create(koi);
                    result = await _unitOfWork.SaveChangesWithTransactionAsync();
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
    }
}
