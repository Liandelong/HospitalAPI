using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Applications.User;
using HospitalAPI.Applications.User.Dtos;
using HospitalAPI.Models;
using HospitalAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    //如果访问的路径为404则需要配置路由
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        public readonly DoContext _doContext;
        private readonly IMapper _mapper;
        private readonly IUserAppService _userAppService;

        public LoginController(DoContext doContext,IMapper mapper, IUserAppService userAppService)
        {
            _doContext = doContext;
            _mapper = mapper;
            _userAppService = userAppService;
        }

        [Route("Login")]
        [HttpPost]
        public UserDto Login([FromBody] LoginInput input)
        {
            var user= _doContext.Users.FirstOrDefault(x => x.Password == input.PassWord && x.Telephone == input.Telephone);
           return  _mapper.Map<UserDto>(user);
        }

        [Route("Register")]
        [HttpPost]
        public async Task<UserDto> Register([FromBody] RegisterInput input)
        {
            var createOrEditUserDto = _mapper.Map<CreateOrEditUserDto>(input);
            if (createOrEditUserDto != null)
            {
                createOrEditUserDto.IsAdmin = false;
            }
            return await _userAppService.CreateOrEditUser(createOrEditUserDto);
        }
    }
}