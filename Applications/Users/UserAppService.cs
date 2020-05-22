using AutoMapper;
using HospitalAPI.Applications.commons;
using HospitalAPI.Applications.User.Dtos;
using HospitalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Applications.User
{
    public class UserAppService : IUserAppService
    {
        private readonly DoContext _doContext;
        private readonly IMapper _mapper;
        public UserAppService(DoContext doContext,IMapper mapper)
        {
            _doContext = doContext;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateOrEditUser(CreateOrEditUserDto input)
        {
            var user = _mapper.Map<Users>(input);
            if (input.Id.HasValue&& input.Id.Value!=0)
            {
                 _doContext.Users.Update(user);
            }
            else
            {
                await  _doContext.Users.AddAsync(user);
            }
            await _doContext.SaveChangesAsync();
            return   _mapper.Map<UserDto>(user);
        }

        public async Task Delete(int Id)
        {
            var user=  _doContext.Users.SingleOrDefault(x => x.Id == Id);
            if (user == null)
            {

            }
            else
            {
                _doContext.Users.Remove(user);
                await _doContext.SaveChangesAsync();
            }
        }

        public async Task<UserDto> GetUserAsync(int Id)
        {
           var user=   _doContext.Users.FirstOrDefault(x => x.Id == Id);
            return _mapper.Map<UserDto>(user);
        }

        public OutputList GetUsersAsync( Filters input)
        {
            var users = _doContext.Users.ToList().WhereIf(!string.IsNullOrEmpty(input.Query), x => x.UserName.Contains(input.Query));

            var totel = users.Count();

            if (input.PageSize == 0 || input.PageNumber == 0)
            {

            }
            else
            {
                users = users.Skip((input.PageNumber - 1) * input.PageSize)
                         .Take(input.PageSize);
            }

            var result = _mapper.Map<List<UserListDto>>(users);

            OutputList outputList = new OutputList
            {
                Totel = totel,
                Data = result
            };
            return outputList;
        }
    }
}
