using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Applications.commons;
using HospitalAPI.Applications.Hospitals.Dtos;
using HospitalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    //如果访问的路径为404则需要配置路由
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : Controller
    {
        private readonly DoContext _doContext;
        private readonly IMapper _mapper;
        public HospitalController(DoContext doContext, IMapper mapper)
        {
            _doContext = doContext;
            _mapper = mapper;
        }

        [Route("GetHospitals")]
        [HttpPost]
        public  OutputList GetHospitals([FromBody] Filters input)
        {
             var hospitals=  _doContext.Hospitals.ToList().WhereIf(!string.IsNullOrEmpty(input.Query), x => x.HospitalName.Contains(input.Query) || x.HospitalAddress.Contains(input.Query));

            var totel = hospitals.Count();

            if (input.PageSize == 0 || input.PageNumber == 0)
            {

            }
            else
            {
                hospitals = hospitals.Skip((input.PageNumber - 1) * input.PageSize)
                         .Take(input.PageSize);
            }

            var result = _mapper.Map<List<HospitalListDto>>(hospitals);

            OutputList outputList = new OutputList
            {
                Totel = totel,
                Data = result
            };

            return outputList;

        }

        [HttpGet("GetHospitalByID/{Id}")]
        public async Task<HospitalDto> GetHospitalByID(int Id)
        {
            var hospital = _doContext.Hospitals.FirstOrDefault(x => x.Id == Id);
            return _mapper.Map<HospitalDto>(hospital);
        }

        [Route("CreateOrEditHospital")]
        [HttpPost]
        public async Task<HospitalDto> CreateOrEditHospital([FromBody] CreateOrEditHospitalDto input)
        {
            var hospital = _mapper.Map<Hospitals>(input);
            if (input.Id.HasValue && input.Id.Value != 0)
            {
                _doContext.Hospitals.Update(hospital);
            }
            else
            {
                await _doContext.Hospitals.AddAsync(hospital);
            }
            await _doContext.SaveChangesAsync();
            return _mapper.Map<HospitalDto>(hospital);
        }

        [HttpDelete("Delete/{Id}")]
        public async Task Delete(int Id)
        {
            var hospital = _doContext.Hospitals.SingleOrDefault(x => x.Id == Id);
            if (hospital == null)
            {

            }
            else
            {
                _doContext.Hospitals.Remove(hospital);
                await _doContext.SaveChangesAsync();
            }
        }
    }
}