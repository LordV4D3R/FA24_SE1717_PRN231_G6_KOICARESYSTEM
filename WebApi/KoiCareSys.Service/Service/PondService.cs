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
    public class PondService : IPondService
    {
        private readonly UnitOfWork _unitOfWork;

        public PondService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IBusinessResult> GetAll(String? search)
        {
            try
            {
                var ponds = _unitOfWork.Pond.GetAllPond(search ?? "");
                if (ponds == null)
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, "Pond not found");
                else
                    return new BusinessResult(Const.SUCCESS_READ_CODE, "Success", ponds);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Create(PondDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return new BusinessResult(Const.ERROR_EXCEPTION, "request cannot be null.");
                }
                Pond create = new Pond()
                {
                    PondName = dto.PondName,
                    Volume = dto.Volume,
                    Depth = dto.Depth,
                    DrainCount = dto.DrainCount,
                    SkimmerCount = dto.SkimmerCount,
                    PumpCapacity = dto.PumpCapacity,
                    ImgUrl = dto.ImgUrl,
                    Note = dto.Note,
                    Description = dto.Description,
                    Status = dto.Status,
                    IsQualified = dto.IsQualified,
                    UserId = dto.UserId

                };
                if (await _unitOfWork.Pond.CreateAsync(create) > 0)
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, "Create pond success", create);
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
                    return new BusinessResult(Const.ERROR_EXCEPTION, "Pond code can not be null");
                var pond = await _unitOfWork.Pond.GetByIdAsync(code);

                if (pond == null)
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, "Pond not found");
                else
                    return new BusinessResult(Const.SUCCESS_READ_CODE, "Success", pond);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
