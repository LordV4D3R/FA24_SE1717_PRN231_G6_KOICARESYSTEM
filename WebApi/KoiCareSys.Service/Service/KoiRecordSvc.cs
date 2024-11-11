using KoiCareSys.Common;
using KoiCareSys.Data;
using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;
using KoiCareSys.Serivice.Base;
using KoiCareSys.Service.Service.Interface;

namespace KoiCareSys.Service.Service
{
    public class KoiRecordSvc : IKoiRecordSvc
    {
        private readonly UnitOfWork _unitOfWork;

        public KoiRecordSvc(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IBusinessResult> AddKoiRecordAsync(KoiRecordDTO request)
        {
            try
            {
                if (request == null)
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, message: Const.FAIL_CREATE_MSG);
                }

                KoiRecord create = new KoiRecord()
                {
                    Id = Guid.NewGuid(),
                    KoiId = request.KoiId,
                    Physique = request.Physique,
                    Length = request.Length,
                    Weight = request.Weight,
                    //Color = request.Color,
                    //Description = request.Description,
                    //HealthIssue = request.HealthIssue,
                    //Price = request.Price,
                    //RecordName = request.RecordName,
                    RecordAt = request.RecordAt,
                    DevelopmentStageId = request.DevelopmentStageId
                };

                if (await _unitOfWork.KoiRecord.CreateAsync(create) > 0)
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

        public async Task<IBusinessResult> GetAllKoiRecordsAsync()
        {
            try
            {
                var result = await _unitOfWork.KoiRecord.GetAllAsync(includeProperties: "Koi,DevelopmentStage");
                if (result != null)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
                }

            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllKoiRecordsByKeywordAsync(string keyword)
        {
            //try
            //{
            //    var result = await _unitOfWork.KoiRecord.GetAllKoiRecordsByKeywordAsync(keyword);
            //    if (result != null)
            //    {
            //        return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
            //    }
            //    else
            //    {
            //        return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            //}
            throw new NotImplementedException();
        }

        public async Task<IBusinessResult> GetKoiRecordByIdAsync(Guid id)
        {
            try
            {
                var result = await _unitOfWork.KoiRecord.GetByIdAsync(id);
                if (result != null)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
                }

            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> RemoveKoiRecord(Guid id)
        {
            try
            {
                if (id == null)
                {
                    return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }

                var found = await _unitOfWork.KoiRecord.GetByIdAsync(id);

                if (await _unitOfWork.KoiRecord.RemoveAsync(found))
                {
                    return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }

            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> UpdateKoiRecordAsync(KoiRecordUpdateDTO request)
        {
            try
            {
                if (request == null)
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, message: Const.FAIL_UPDATE_MSG);
                }

                KoiRecord update = new KoiRecord()
                {
                    Id = request.Id,
                    KoiId = request.KoiId,
                    Physique = request.Physique,
                    Length = request.Length,
                    Weight = request.Weight,
                    RecordAt = request.RecordAt,
                    //Color = request.Color,
                    //Description = request.Description,
                    //HealthIssue = request.HealthIssue,
                    //Price = request.Price,
                    //RecordName = request.RecordName,
                    DevelopmentStageId = request.DevelopmentStageId
                };

                if (await _unitOfWork.KoiRecord.UpdateAsync(update) > 0)
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
