using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Applications.commons;
using HospitalAPI.Applications.Departments.Dtos;
using HospitalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    //如果访问的路径为404则需要配置路由
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly DoContext _doContext;
        private readonly IMapper _mapper;
        public DepartmentController(DoContext doContext, IMapper mapper)
        {
            _doContext = doContext;
            _mapper = mapper;
        }

        [Route("GetDepartments")]
        [HttpPost]
        public OutputList GetDepartments([FromBody] Filters input)
        {
            var departments = _doContext.Departments.ToList()
                                                .WhereIf(!string.IsNullOrEmpty(input.Query), x => x.DepartmentName.Contains(input.Query));

            var totel = departments.Count();

            if(input.PageSize == 0 || input.PageNumber == 0)
            {

            }
            else
            {
                departments = departments.Skip((input.PageNumber - 1) * input.PageSize)
                         .Take(input.PageSize);
            }

            var result=  _mapper.Map<List<DepartmentListDto>>(departments);

            OutputList outputList = new OutputList
            {
                Totel = totel,
                Data = result
            };
            return outputList;
        }

        [HttpGet("GetDepartmentByID/{Id}")]
        public async Task<DepartmentDto> GetDepartmentByID(int Id)
        {
            var department = _doContext.Departments.FirstOrDefault(x => x.Id == Id);
            return _mapper.Map<DepartmentDto>(department);
        }

        [Route("CreateOrEditDepartment")]
        [HttpPost]
        public async Task<DepartmentDto> CreateOrEditDepartment([FromBody] CreateOrEditDepartmentDto input)
        {
            var department = _mapper.Map<Departments>(input);
            if (input.Id.HasValue && input.Id.Value != 0)
            {
                _doContext.Departments.Update(department);
            }
            else
            {
                await _doContext.Departments.AddAsync(department);
            }
            await _doContext.SaveChangesAsync();
            return _mapper.Map<DepartmentDto>(department);
        }

        [HttpDelete("Delete/{Id}")]
        public async Task Delete(int Id)
        {
            var department = _doContext.Departments.SingleOrDefault(x => x.Id == Id);
            if (department == null)
            {

            }
            else
            {
                _doContext.Departments.Remove(department);
                await _doContext.SaveChangesAsync();
            }
        }
    }
}