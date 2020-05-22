using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Applications.commons;
using HospitalAPI.Applications.HealthNews;
using HospitalAPI.Models;
using HospitalAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers
{
    //如果访问的路径为404则需要配置路由
    [Route("api/[controller]")]
    [ApiController]
    public class HealthNewsController : Controller
    {
        private readonly DoContext _doContext;
        private readonly IMapper _mapper;
        public HealthNewsController(DoContext doContext, IMapper mapper)
        {
            _doContext = doContext;
            _mapper = mapper;
        }

        [Route("GetHealthNewsAPI")]
        [HttpPost]
        public OutputList GetHealthNews( int newsKindId)
        {
            var query = from healthNews in _doContext.HealthNews.ToList()
                        .WhereIf(newsKindId > 0, x => x.NewsKindId == newsKindId)
                        join newKinds in _doContext.NewsKinds.ToList()
                        on healthNews.NewsKindId equals newKinds.Id
                        select new HealthNewListDto
                        {
                            DateTime = healthNews.DateTime == null ? null : healthNews.DateTime.ToString("yyyy-MM-dd hh:mm"),
                            Image_url = healthNews.Image_url,
                            Title = healthNews.Title,
                            Article_Type = healthNews.Article_Type,
                            Source = healthNews.Source,
                            VideoSrc = healthNews.VideoSrc,
                            NewsKindName = newKinds.Name,
                            Id = healthNews.Id,
                            Content= healthNews.Content
                        };

            foreach (var healthNewListDto in query)
            {
                healthNewListDto.Article_Type += 1;
            }

            var totel = query.Count();

            var result = _mapper.Map<List<HealthNewListDto>>(query);

            OutputList outputList = new OutputList
            {
                Totel = totel,
                Data = result
            };
            return outputList;
        }

        [Route("GetHealthNews")]
        [HttpPost]
        public OutputList GetHealthNews([FromBody] HealthNewsInput input)
        {
            var query = from healthNews in _doContext.HealthNews.ToList()
                        .WhereIf(!string.IsNullOrEmpty(input.Query), x => x.Title.Contains(input.Query))
                        .WhereIf(input.NewsKindId>0,x=>x.NewsKindId==input.NewsKindId)
                        join newKinds in _doContext.NewsKinds.ToList()
                        on healthNews.NewsKindId equals newKinds.Id
                        select new HealthNewListDto
                        {
                            DateTime = healthNews.DateTime==null? null: healthNews.DateTime.ToString("yyyy-MM-dd hh:mm"),
                            Image_url = healthNews.Image_url,
                            Title = healthNews.Title,
                            Article_Type = healthNews.Article_Type,
                            Source = healthNews.Source,
                            VideoSrc = healthNews.VideoSrc,
                            NewsKindName= newKinds.Name,
                            Id= healthNews.Id,
                            Content = healthNews.Content,
                            NewsKindId= newKinds.Id
                        };

            foreach (var healthNewListDto in query)
            {
                healthNewListDto. Article_Type += 1;
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

            var result = _mapper.Map<List<HealthNewListDto>>(query);

            OutputList outputList = new OutputList
            {
                Totel = totel,
                Data = result
            };
            return outputList;
        }

        [HttpGet("GetHealthNewByID/{Id}")]
        public OutputList GetHealthNewByID(int Id)
        {
            var healthNew = _doContext.HealthNews.FirstOrDefault(x => x.Id == Id);
            var result= _mapper.Map<HealthNewDto>(healthNew);
            OutputList outputList = new OutputList
            {
                Totel = 1,
                Data = result
            };
            return outputList;
        }

        [Route("CreateOrEditHealthNew")]
        [HttpPost]
        public async Task<HealthNewDto> CreateOrEditHealthNew([FromBody] CreateOrEditHealthNewDto input)
        {
            var healthNews = _mapper.Map<HealthNews>(input);
            healthNews.DateTime = DateTime.Now;
            if (input.Id.HasValue && input.Id.Value != 0)
            {
                _doContext.HealthNews.Update(healthNews);
            }
            else
            {
                await _doContext.HealthNews.AddAsync(healthNews);
            }
            await _doContext.SaveChangesAsync();
            return _mapper.Map<HealthNewDto>(healthNews);
        }

        [HttpDelete("Delete/{Id}")]
        public async Task Delete(int Id)
        {
            var healthNews = _doContext.HealthNews.SingleOrDefault(x => x.Id == Id);
            if (healthNews == null)
            {

            }
            else
            {
                _doContext.HealthNews.Remove(healthNews);
                await _doContext.SaveChangesAsync();
            }
        }
    }
}