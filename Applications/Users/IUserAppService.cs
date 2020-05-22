using HospitalAPI.Applications.commons;
using HospitalAPI.Applications.User.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Applications.User
{
    public interface IUserAppService
    {
        OutputList GetUsersAsync(Filters input);

        Task<UserDto> GetUserAsync(int Id);

        Task<UserDto> CreateOrEditUser(CreateOrEditUserDto input);

        Task Delete(int Id);
    }
}
