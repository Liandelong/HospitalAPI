using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Applications.commons;
using HospitalAPI.Applications.NewsKinds.Dtos;
using HospitalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    //如果访问的路径为404则需要配置路由
    [Route("api/[controller]")]
    [ApiController]
    public class NewsKindsController : Controller
    {
        private readonly DoContext _doContext;
        private readonly IMapper _mapper;
        public NewsKindsController(DoContext doContext, IMapper mapper)
        {
            _doContext = doContext;
            _mapper = mapper;
        }

        [Route("GetNewsKinds")]
        [HttpPost]
        public  OutputList GetNewsKinds([FromBody] Filters input)
        {
            var newsKind = _doContext.NewsKinds.ToList().WhereIf(!string.IsNullOrEmpty(input.Query), x => x.Name.Contains(input.Query));

            var totel = newsKind.Count();

            if (input.PageSize == 0 || input.PageNumber == 0)
            {

            }
            else
            {
                         newsKind = newsKind.Skip((input.PageNumber - 1) * input.PageSize)
                         .Take(input.PageSize);
            }

            var result = _mapper.Map<List<NewsKindListDto>>(newsKind);

            OutputList outputList = new OutputList
            {
                Totel = totel,
                Data = result
            };
            return outputList;
        }

        [HttpGet("GetNewsKindByID/{Id}")]
        public async Task<NewsKindDto> GetNewsKindByID(int Id)
        {
            var newsKind = _doContext.NewsKinds.FirstOrDefault(x => x.Id == Id);
            return _mapper.Map<NewsKindDto>(newsKind);
        }

        [Route("CreateOrEditNewsKind")]
        [HttpPost]
        public async Task<NewsKindDto> CreateOrEditNewsKind([FromBody] CreateOrEditNewKindDto input)
        {
            var newsKind = _mapper.Map<NewsKinds>(input);
            if (input.Id.HasValue && input.Id.Value != 0)
            {
                _doContext.NewsKinds.Update(newsKind);
            }
            else
            {
                await _doContext.NewsKinds.AddAsync(newsKind);
            }
            await _doContext.SaveChangesAsync();
            return _mapper.Map<NewsKindDto>(newsKind);
        }

        [HttpDelete("Delete/{Id}")]
        public async Task Delete(int Id)
        {
            var newsKind = _doContext.NewsKinds.SingleOrDefault(x => x.Id == Id);
            if (newsKind == null)
            {

            }
            else
            {
                _doContext.NewsKinds.Remove(newsKind);
                await _doContext.SaveChangesAsync();
            }
        }
    }
}