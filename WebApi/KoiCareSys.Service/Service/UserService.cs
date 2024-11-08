using KoiCareSys.Common;
using KoiCareSys.Data;
using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;
using KoiCareSys.Serivice.Base;
using KoiCareSys.Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCareSys.Service.Service
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IBusinessResult> GetAll(String? search)
        {
            try
            {
                var users = await _unitOfWork.User.GetAllUser(search ?? "");
                if (users == null)
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, "User not found");
                else
                    return new BusinessResult(Const.SUCCESS_READ_CODE, "Success", users);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Create(RegisterNewUserDTO request)
        {
            try
            {
                if (request == null)
                {
                    return new BusinessResult(Const.ERROR_EXCEPTION, "request cannot be null.");
                }
                User create = new User()
                {
                    Email = request.Email,
                    Password = request.Password,
                    FullName = request.FullName,
                    PhoneNumber = request.PhoneNumber,
                    Role = (int)request.Role,
                    Status = (int)request.Status,

                };
                if (await _unitOfWork.User.CreateAsync(create) > 0)
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, "Create user success", create);
                else
                    return new BusinessResult(Const.FAIL_CREATE_CODE, "Create fail");
            }
            catch (Exception ex) { return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message); }
        }

        public async Task<IBusinessResult> GetById(Guid code)
        {
            try
            {
                if (code == null)
                    return new BusinessResult(Const.ERROR_EXCEPTION, "User code can not be null");
                var user = await _unitOfWork.User.GetByIdAsync(code);

                if (user == null)
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, "No useer data by code");
                else
                    return new BusinessResult(Const.SUCCESS_READ_CODE, "Get user success", user);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> UpdateUser(User user, UpdateUserDTO updateUser)
        {
            try
            {
                if (user == null)
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, "No user data by code");
                else
                {
                    user.Email = updateUser.Email;
                    user.FullName = updateUser.FullName;
                    user.PhoneNumber = updateUser.PhoneNumber;
                    user.Password = updateUser.Password;
                    if (await _unitOfWork.User.UpdateAsync(user) > 0)
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, "Update user success", user);
                    else
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, "Update fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> DeleteUser(Guid code)
        {
            try
            {
                var user = await _unitOfWork.User.GetByIdAsync(code);
                if (user == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    var result = await _unitOfWork.User.RemoveAsync(user);
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
