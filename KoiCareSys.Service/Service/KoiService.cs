using KoiCareSys.Common;
using KoiCareSys.Data;
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

        public async Task<IBusinessResult> GetAll()
        {
            #region Business Rule
            #endregion
            var koi = await _unitOfWork.Koi.GetAllAsync();
            if (koi == null)
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, koi);
            }
            else
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, koi);
            }
        }

        public async Task<IBusinessResult> GetById(string KoiNo)
        {
            #region Business ruke
            #endregion
            var koi = await _unitOfWork.Koi.GetByIdAsync(KoiNo);
            if (koi == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Koi());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, koi);
            }
        }
        public async Task<IBusinessResult> Save(Koi koi)
        {
            try
            {

                int result = -1;

                var KoiTmp = _unitOfWork.Koi.GetById(koi.Id);
                if (KoiTmp != null)
                {
                    #region Business rule
                    #endregion Business rule
                    result = await _unitOfWork.Koi.UpdateAsync(koi);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, new Koi());
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

                    result = await _unitOfWork.Koi.CreateAsync(koi);
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
        public async Task<IBusinessResult> DeleteById(string KoiNo)
        {
            #region Business rule

            #endregion Business rule
            try
            {
                var koi = await _unitOfWork.Koi.GetByIdAsync(KoiNo);
                if (koi == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Koi>());
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
    }
}
