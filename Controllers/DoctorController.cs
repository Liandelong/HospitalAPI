using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Applications.commons;
using HospitalAPI.Applications.Doctors.Dtos;
using HospitalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    //如果访问的路径为404则需要配置路由
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly DoContext _doContext;
        private readonly IMapper _mapper;
        public DoctorController(DoContext doContext, IMapper mapper)
        {
            _doContext = doContext;
            _mapper = mapper;
        }

        //获取用户信息
        [Route("GetDoctors")]
        [HttpPost]
        public  OutputList GetDoctors([FromBody] Filters input)
        {
            var query = from doctors in _doContext.Doctors.ToList()
                        join hospitals in _doContext.Hospitals.ToList()
                        on doctors.HospitalId equals hospitals.Id
                        join departments in _doContext.Departments.ToList()
                        on doctors.DepartmentId equals departments.Id
                        select new DoctorListDto
                        {
                            Id= doctors.Id,
                            DoctorName = doctors.DoctorName,
                            DepartmentName = departments.DepartmentName,
                            HospitalName = hospitals.HospitalName,
                            PictureLink = doctors.PictureLink,
                            Resume = doctors.Resume,
                            DepartmentId=departments.Id,
                            HospitalId=hospitals.Id
                        };

            query = query.WhereIf(!string.IsNullOrEmpty(input.Query), x => x.DoctorName.Contains(input.Query) || x.DepartmentName.Contains(input.Query) || x.HospitalName.Contains(input.Query));
            int totel = query.Count();
            if (input.PageSize == 0 || input.PageNumber == 0)
            {

            }
            else
            {
                        query = query.Skip((input.PageNumber - 1) * input.PageSize)
                         .Take(input.PageSize);
            }
            OutputList outputList = new OutputList
            {
                Totel = totel,
                Data = query
            };
            return outputList;
        }

        [HttpGet("GetDoctorByID/{Id}")]
        public async Task<DoctorDto> GetDoctorByID(int Id)
        {
            var query = from doctors in _doContext.Doctors.ToList()
                        where doctors.Id== Id
                        join hospitals in _doContext.Hospitals.ToList()
                        on doctors.HospitalId equals hospitals.Id
                        join departments in _doContext.Departments.ToList()
                        on doctors.DepartmentId equals departments.Id
                        select new DoctorDto
                        {
                            Id = doctors.Id,
                            DoctorName = doctors.DoctorName,
                            DepartmentName = departments.DepartmentName,
                            HospitalName = hospitals.HospitalName,
                            PictureLink = doctors.PictureLink,
                            Resume = doctors.Resume
                        };
            return query.FirstOrDefault();
        }

        [Route("CreateOrEditDoctor")]
        [HttpPost]
        public async Task<DoctorDto> CreateOrEditDoctor([FromBody] CreateOrEditDoctorDto input)
        {
            var doctor = _mapper.Map<Doctors>(input);
            if (input.Id.HasValue && input.Id.Value != 0)
            {
                _doContext.Doctors.Update(doctor);
            }
            else
            {
                await _doContext.Doctors.AddAsync(doctor);
            }
            await _doContext.SaveChangesAsync();
            return _mapper.Map<DoctorDto>(doctor);
        }

        [HttpDelete("Delete/{Id}")]
        public async Task Delete(int Id)
        {
            var user = _doContext.Doctors.FirstOrDefault(x => x.Id == Id);
            _doContext.Doctors.Remove(user);
            await _doContext.SaveChangesAsync();
        }
    }
}