using AutoMapper;
using KoiCareSys.Common;
using KoiCareSys.Data;
using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;
using KoiCareSys.Serivice.Base;
using KoiCareSys.Service.Service.Interface;
using KoiCareSys.Service.SignalR;

namespace KoiCareSys.Service.Service
{

    public class KoiService : IKoiService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        private readonly SignalRHub _hub;

        public KoiService(UnitOfWork unitOfWork, IMapper mapper, SignalRHub hub)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hub = hub;
        }

        public async Task<IBusinessResult> GetAll()
        {
            #region Business Rule
            #endregion
            var kois = await _unitOfWork.Koi.GetAllAsync();
            if (kois != null)
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, kois);
            }
            else
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
        }

        public async Task<IBusinessResult> GetById(Guid koiId)
        {
            #region Business ruke
            #endregion
            var koi = await _unitOfWork.Koi.GetByIdAsync(koiId);
            if (koi == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, koi);
            }
        }
        public async Task<IBusinessResult> Save(Guid koiId = default, KoiDTO dto = default)
        {
            try
            {
                var koi = _mapper.Map<Koi>(dto);

                int result = -1;
                var koiTmp = _unitOfWork.Koi.GetById(koiId);
                if (koiTmp is not null)
                {
                    #region Business rule
                    #endregion Business rule
                    koiTmp.Name = dto.Name;
                    koiTmp.PurchasePrice = dto.PurchasePrice;
                    koiTmp.InPondSince = dto.InPondSince;
                    koiTmp.Status = dto.Status;
                    koiTmp.Age = dto.Age;
                    koiTmp.Breed = dto.Breed;
                    koiTmp.Category = dto.Category;
                    koiTmp.Physique = dto.Physique;
                    koiTmp.Sex = dto.Sex;
                    koiTmp.Length = dto.Length;
                    koiTmp.ImgUrl = dto.ImgUrl;
                    koiTmp.Origin = dto.Origin;

                    _unitOfWork.Koi.Update(koiTmp);
                    result = _unitOfWork.SaveChangesWithTransaction();
                    if (result > 0)
                    {
                        await _hub.SendMessage($"Updated Koi with id: {koiTmp.Id}");
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, dto);
                    }
                }
                else
                {
                    #region Business rule
                    #endregion Business rule
                    _unitOfWork.Koi.CreateKoi(koi);
                    result = _unitOfWork.SaveChangesWithTransaction();
                    if (result > 0)
                    {
                        await _hub.SendMessage($"Created new Koi with id: {koi.Id}");
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
        public async Task<IBusinessResult> DeleteById(Guid koiId)
        {
            #region Business rule

            #endregion Business rule
            try
            {
                var koi = await _unitOfWork.Koi.GetByIdAsync(koiId);
                if (koi == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    var result = await _unitOfWork.Koi.RemoveAsync(koi);
                    if (result)
                    {
                        await _hub.SendMessage($"Removed Koi with id: {koi.Id}");
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
