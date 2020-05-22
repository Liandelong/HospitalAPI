using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Applications.commons;
using HospitalAPI.Applications.DiseaseKinds;
using HospitalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    //如果访问的路径为404则需要配置路由
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseKindsController : Controller
    {
        private readonly DoContext _doContext;
        private readonly IMapper _mapper;
        public DiseaseKindsController(DoContext doContext, IMapper mapper)
        {
            _doContext = doContext;
            _mapper = mapper;
        }

        [Route("GetDiseaseKinds")]
        [HttpPost]
        public OutputList GetDiseaseKinds([FromBody] Filters input)
        {
            var diseaseKind = _doContext.DiseaseKinds.ToList().WhereIf(!string.IsNullOrEmpty(input.Query), x => x.Name.Contains(input.Query));

            var totel = diseaseKind.Count();

            if (input.PageSize == 0 || input.PageNumber == 0)
            {

            }
            else
            {
                diseaseKind = diseaseKind.Skip((input.PageNumber - 1) * input.PageSize)
                .Take(input.PageSize);
            }

            var result = _mapper.Map<List<DiseaseKindsListDto>>(diseaseKind);

            OutputList outputList = new OutputList
            {
                Totel = totel,
                Data = result
            };
            return outputList;
        }

        [HttpGet("GetDiseaseKindByID/{Id}")]
        public async Task<DiseaseKindsDto> GetDiseaseKindByID(int Id)
        {
            var diseaseKind = _doContext.DiseaseKinds.FirstOrDefault(x => x.Id == Id);
            return _mapper.Map<DiseaseKindsDto>(diseaseKind);
        }

        [Route("CreateOrEditDiseaseKind")]
        [HttpPost]
        public async Task<DiseaseKindsDto> CreateOrEditDiseaseKind([FromBody] CreateOrEditDiseaseKindsDto input)
        {
            var diseaseKind = _mapper.Map<DiseaseKinds>(input);
            if (input.Id.HasValue && input.Id.Value != 0)
            {
                _doContext.DiseaseKinds.Update(diseaseKind);
            }
            else
            {
                await _doContext.DiseaseKinds.AddAsync(diseaseKind);
            }
            await _doContext.SaveChangesAsync();
            return _mapper.Map<DiseaseKindsDto>(diseaseKind);
        }

        [HttpDelete("Delete/{Id}")]
        public async Task Delete(int Id)
        {
            var diseaseKind = _doContext.DiseaseKinds.SingleOrDefault(x => x.Id == Id);
            if (diseaseKind == null)
            {

            }
            else
            {
                _doContext.DiseaseKinds.Remove(diseaseKind);
                await _doContext.SaveChangesAsync();
            }
        }
    }
}