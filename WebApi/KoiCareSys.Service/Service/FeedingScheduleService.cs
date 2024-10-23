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

        public FeedingScheduleService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IBusinessResult> GetAll(String? search)
        {
            try
            {
                var feedingSchedules = await _unitOfWork.FeedingSchedule.GetAllFeedingSchedules(search ?? "");
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
                if (request == null)
                {
                    return new BusinessResult(Const.ERROR_EXCEPTION, "request cannot be null.");
                }

                FeedingSchedule create = new FeedingSchedule()
                {
                    FeedAt = request.FeedAt,
                    FoodAmount = request.FoodAmount,
                    FoodType = request.FoodType,
                    Note = request.Note,
                    PondId = request.PondId,
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

        public async Task<IBusinessResult> CaculateFoodAmountByKoi(Guid pondId)
        {
            try
            {
                // Lấy danh sách các con cá Koi trong hồ từ PondId
                var pond = await _unitOfWork.Pond.GetByIdAsync(pondId);
                if (pond == null)
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, "Pond not found");

                var koiFishList = pond.Koi; // Giả định danh sách cá Koi có trong hồ

                decimal totalFoodAmount = 0;



                // Tính tổng lượng thức ăn cho tất cả cá trong hồ
                foreach (var koi in koiFishList)
                {
                    totalFoodAmount += GetFoodAmountByLength(koi.Length);
                }

                return new BusinessResult(Const.SUCCESS_BUSSINESS_CODE, "Calculation success", totalFoodAmount);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Update(FeedingSchedule feedingSchedule, FeedingScheduleDTO feedingScheduleDTO)
        {
            try
            {
                if (feedingScheduleDTO == null)
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, "No Feeding Schedule data by code");
                else
                {
                    feedingSchedule.FeedAt = feedingScheduleDTO.FeedAt;
                    feedingSchedule.FoodAmount = feedingScheduleDTO.FoodAmount;
                    feedingSchedule.FoodType = feedingScheduleDTO.FoodType;
                    feedingSchedule.Note = feedingScheduleDTO.Note;
                    feedingSchedule.PondId = feedingScheduleDTO.PondId;
                    if (await _unitOfWork.FeedingSchedule.UpdateAsync(feedingSchedule) > 0)
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, "Update Feeding Schedule success", feedingSchedule);
                    else
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, "Update fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Delete(Guid code)
        {
            try
            {
                var feedingSchedule = await _unitOfWork.FeedingSchedule.GetByIdAsync(code);
                if (feedingSchedule == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    var result = await _unitOfWork.FeedingSchedule.RemoveAsync(feedingSchedule);
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


        // Hàm phụ để tính lượng thức ăn dựa trên chiều dài của cá
        private decimal GetFoodAmountByLength(decimal? length)
        {
            if (length <= 10) return 50;
            if (length <= 15) return 90;
            if (length <= 20) return 120;
            if (length <= 25) return 200;
            if (length <= 30) return 350;
            if (length <= 35) return 600;
            if (length <= 40) return 900;
            if (length <= 45) return 1200;
            if (length <= 50) return 1700;
            if (length <= 55) return 2300;
            if (length <= 60) return 2800;
            if (length <= 65) return 3500;
            if (length <= 70) return 7000;
            if (length <= 75) return 8500;
            if (length <= 80) return 10000;
            if (length <= 85) return 15000;
            if (length <= 90) return 20000;
            return 0; // Nếu không có trong bảng
        }


    }
}
