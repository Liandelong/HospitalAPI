using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Applications.commons;
using HospitalAPI.Applications.DiseaseKnowledge;
using HospitalAPI.Models;
using HospitalAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    //如果访问的路径为404则需要配置路由
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseKnowledgeController : Controller
    {
        private readonly DoContext _doContext;
        private readonly IMapper _mapper;
        public DiseaseKnowledgeController(DoContext doContext, IMapper mapper)
        {
            _doContext = doContext;
            _mapper = mapper;
        }

        [Route("GetDiseaseKnowledgesAPI")]
        [HttpPost]
        public OutputList GetDiseaseKnowledges(int diseaseKindId)
        {
            var querys = from diseaseKnowledges in _doContext.DiseaseKnowledges.ToList()
                        .WhereIf(diseaseKindId > 0, x => x.DiseaseKindId == diseaseKindId)
                        join diseaseKinds in _doContext.DiseaseKinds.ToList()
                        on diseaseKnowledges.DiseaseKindId equals diseaseKinds.Id
                        select new DiseaseKnowledgeListDto
                        {
                            DateTime = diseaseKnowledges.DateTime == null ? null : diseaseKnowledges.DateTime.ToString("yyyy-MM-dd hh:mm"),
                            Image_url = diseaseKnowledges.Image_url,
                            Title = diseaseKnowledges.Title,
                            Article_Type = diseaseKnowledges.Article_Type,
                            Source = diseaseKnowledges.Source,
                            VideoSrc = diseaseKnowledges.VideoSrc,
                            DiseaseKindName = diseaseKinds.Name,
                            Id = diseaseKnowledges.Id,
                            Content = diseaseKnowledges.Content
                        };

            foreach (var query in querys)
            {
                query.Article_Type += 1;
            }

            var totel = querys.Count();

            var result = _mapper.Map<List<DiseaseKnowledgeListDto>>(querys);

            OutputList outputList = new OutputList
            {
                Totel = totel,
                Data = result
            };
            return outputList;
        }

        [Route("GetDiseaseKnowledges")]
        [HttpPost]
        public OutputList GetdiseaseKnowledges([FromBody] DiseaseKnowledgeInput input)
        {
            var query = from diseaseKnowledges in _doContext.DiseaseKnowledges.ToList()
                        .WhereIf(!string.IsNullOrEmpty(input.Query), x => x.Title.Contains(input.Query))
                        .WhereIf(input.DiseaseKindId > 0, x => x.DiseaseKindId == input.DiseaseKindId)
                        join diseaseKinds in _doContext.DiseaseKinds.ToList()
                        on diseaseKnowledges.DiseaseKindId equals diseaseKinds.Id
                        select new DiseaseKnowledgeListDto
                        {
                            DateTime = diseaseKnowledges.DateTime == null ? null : diseaseKnowledges.DateTime.ToString("yyyy-MM-dd hh:mm"),
                            Image_url = diseaseKnowledges.Image_url,
                            Title = diseaseKnowledges.Title,
                            Article_Type = diseaseKnowledges.Article_Type,
                            Source = diseaseKnowledges.Source,
                            VideoSrc = diseaseKnowledges.VideoSrc,
                            DiseaseKindName = diseaseKinds.Name,
                            Id = diseaseKnowledges.Id,
                            Content = diseaseKnowledges.Content,
                            DiseaseKindId = diseaseKinds.Id
                        };

            foreach (var DiseaseKnowledgeListDto in query)
            {
                DiseaseKnowledgeListDto.Article_Type += 1;
            }

            var totel = query.Count();

            if (input.PageSize == 0 || input.PageNumber == 0)
            {

            }
            else
            {
                query = query.Skip((input.PageNumber - 1) * input.PageSize)
                         .Take(input.PageSize);
            }

            var result = _mapper.Map<List<DiseaseKnowledgeListDto>>(query);

            OutputList outputList = new OutputList
            {
                Totel = totel,
                Data = result
            };
            return outputList;
        }

        [HttpGet("GetDiseaseKnowledgeByID/{Id}")]
        public OutputList GetDiseaseKnowledgeByID(int Id)
        {
            var DiseaseKnowledge = _doContext.DiseaseKnowledges.FirstOrDefault(x => x.Id == Id);
            var result = _mapper.Map<DiseaseKnowledgeDto>(DiseaseKnowledge);
            OutputList outputList = new OutputList
            {
                Totel = 1,
                Data = result
            };
            return outputList;
        }

        [Route("CreateOrEditDiseaseKnowledge")]
        [HttpPost]
        public async Task<DiseaseKnowledgeDto> CreateOrEditDiseaseKnowledge([FromBody] CreateOrEditDiseaseKnowledgeDto input)
        {
            var diseaseKnowledges = _mapper.Map<DiseaseKnowledge>(input);
            diseaseKnowledges.DateTime = DateTime.Now;
            if (input.Id.HasValue && input.Id.Value != 0)
            {
                _doContext.DiseaseKnowledges.Update(diseaseKnowledges);
            }
            else
            {
                await _doContext.DiseaseKnowledges.AddAsync(diseaseKnowledges);
            }
            await _doContext.SaveChangesAsync();
            return _mapper.Map<DiseaseKnowledgeDto>(diseaseKnowledges);
        }

        [HttpDelete("Delete/{Id}")]
        public async Task Delete(int Id)
        {
            var diseaseKnowledges = _doContext.DiseaseKnowledges.SingleOrDefault(x => x.Id == Id);
            if (diseaseKnowledges == null)
            {

            }
            else
            {
                _doContext.DiseaseKnowledges.Remove(diseaseKnowledges);
                await _doContext.SaveChangesAsync();
            }
        }
    }
}