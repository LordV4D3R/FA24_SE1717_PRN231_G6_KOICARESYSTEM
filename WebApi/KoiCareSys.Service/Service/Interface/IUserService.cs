using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;
using KoiCareSys.Serivice.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCareSys.Service.Service.Interface
{
    public interface IUserService
    {
        Task<IBusinessResult> GetAll(String? search);
        Task<IBusinessResult> Create(RegisterNewUserDTO request);
        Task<IBusinessResult> GetById(Guid code);
        Task<IBusinessResult> UpdateUser(User user, UpdateUserDTO updateUser);
        Task<IBusinessResult> DeleteUser(Guid code);
    }
}
