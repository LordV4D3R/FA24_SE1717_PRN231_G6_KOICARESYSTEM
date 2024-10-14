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
    public class FeedingScheduleService : IFeedingScheduleService
    {
        private readonly UnitOfWork _unitOfWork;

        public FeedingScheduleService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll(String? search)
        {
            try
            {
                var feedingSchedules = _unitOfWork.FeedingSchedule.GetAllFeedingSchedules(search ?? "");
                if (feedingSchedules == null)
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, "Feeding Schedule not found");
                else
                    return new BusinessResult(Const.SUCCESS_READ_CODE, "Success", feedingSchedules);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Create(FeedingScheduleDTO request)
        {
            try
            {
                if(request == null)
                {
                    return new BusinessResult(Const.ERROR_EXCEPTION, "request cannot be null.");
                }

                FeedingSchedule create = new FeedingSchedule()
                {
                    FeedAt = request.FeedAt,
                    FoodAmount = request.FoodAmount,
                    FoodType = request.FoodType,
                    Note = request.Note,
                    KoiId = request.KoiId,
                };
                if (await _unitOfWork.FeedingSchedule.CreateAsync(create) > 0)
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, "Create feeding schedule success", create);
                else
                    return new BusinessResult(Const.FAIL_CREATE_CODE, "Create fail");
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(Guid code)
        {
            try
            {
                if (code == null)
                    return new BusinessResult(Const.ERROR_EXCEPTION, "Feeding schedule code can not be null");
                var feedingSchedule = await _unitOfWork.FeedingSchedule.GetByIdAsync(code);
                if (feedingSchedule == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new FeedingSchedule());
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, feedingSchedule);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
