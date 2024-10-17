using KoiCareSys.Common;
using KoiCareSys.Data;
using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;
using KoiCareSys.Serivice.Base;
using KoiCareSys.Service.Service.Interface;

namespace KoiCareSys.Service.Service
{
    public class DevelopmentStageSvc : IDevelopmenStageSvc
    {
        private readonly UnitOfWork _unitOfWork;

        public DevelopmentStageSvc(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IBusinessResult> AddDevelopmenStage(DevelopmentStageDTO developmenStage)
        {
            try
            {
                if (developmenStage == null)
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, message: Const.FAIL_CREATE_MSG);
                }

                DevelopmentStageDTO create = new DevelopmentStageDTO()
                {
                    StageName = developmenStage.StageName,
                    RequiredFoodAmount = developmenStage.RequiredFoodAmount
                };

                if (await _unitOfWork.DevelopmentStage.AddDevelopmentStageAsync(create))
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }


            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> DeleteDevelopmenStage(Guid id)
        {
            try
            {
                if (id == null)
                {
                    return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }
                DevelopmentStage found = await _unitOfWork.DevelopmentStage.GetDevelopmentStageByIdAsync(id);
                if (found == null) return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                await _unitOfWork.DevelopmentStage.RemoveDevelopmentStage(id);
                return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);


            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllDevelopmenStage()
        {
            try
            {
                var result = await _unitOfWork.DevelopmentStage.GetAllDevelopmentStagesAsync();

                if (result == null) return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);

                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);

            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);

            }
        }

        public async Task<IBusinessResult> GetAllDevelopmenStageByKeyword(string keyword)
        {
            try
            {
                var result = await _unitOfWork.DevelopmentStage.GetAllDevelopmentStagesByKewordsAsync(keyword);

                if (result == null) return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);

                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);

            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetDevelopmenStageById(Guid id)
        {
            try
            {

                if (id == null) return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
                var result = await _unitOfWork.DevelopmentStage.GetDevelopmentStageByIdAsync(id);

                if (result == null) return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);

                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> UpdateDevelopmenStage(DevelopmentStageUpdateDTO developmenStage)
        {
            try
            {
                if (developmenStage == null) return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);

                DevelopmentStageUpdateDTO update = new DevelopmentStageUpdateDTO()
                {
                    Id = developmenStage.Id,
                    StageName = developmenStage.StageName,
                    RequiredFoodAmount = developmenStage.RequiredFoodAmount
                };


                if (await _unitOfWork.DevelopmentStage.UpdateDevelopmentStageAsync(update))
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);

                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);

            }
        }
    }
}
