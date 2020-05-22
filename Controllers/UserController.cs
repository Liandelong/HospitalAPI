using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalAPI.Applications.commons;
using HospitalAPI.Applications.User;
using HospitalAPI.Applications.User.Dtos;
using HospitalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    //如果访问的路径为404则需要配置路由
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserAppService _userAppService ;
       public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        //获取用户信息
        [Route("GetUsers")]
        [HttpPost]
        public  OutputList GetUsers([FromBody] Filters input)
        {
            return    _userAppService.GetUsersAsync(input);
        }

        [HttpGet("GetUserByID/{Id}")]
        public async Task<UserDto> GetUserByID(int Id)
        {
            return await _userAppService.GetUserAsync(Id);
        }

        [Route("CreateOrEditUser")]
        [HttpPost]
        public async Task<UserDto> CreateOrEditUser([FromBody] CreateOrEditUserDto input)
        {
            return await _userAppService.CreateOrEditUser(input);
        }


        [HttpDelete("Delete/{Id}")]
        public async Task Delete(int Id)
        {
            await _userAppService.Delete(Id);
        }
    }
}