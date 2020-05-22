using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Applications.commons;
using HospitalAPI.Models;
using HospitalAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{

    //如果访问的路径为404则需要配置路由
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        private readonly DoContext _doContext;
        private readonly IMapper _mapper;
        public SearchController(DoContext doContext, IMapper mapper)
        {
            _doContext = doContext;
            _mapper = mapper;
        }

        [Route("GetSearchs")]
        [HttpPost]
        public SearchDto Searchs(string input)
        {
            SearchDto result=new SearchDto() {Id=-1,Flag=-1 };
            var query = from doctor in _doContext.Doctors.ToList()                          
                            join department in _doContext.Departments.ToList()
                            on doctor.DepartmentId equals department.Id
                            select new 
                            {
                                Id = doctor.Id,
                                DoctorName=doctor.DoctorName,
                                Resume=doctor.Resume,
                                DepartmentName= department.DepartmentName,
                        };

            var searchDto = query.WhereIf(!string.IsNullOrEmpty(input), x => x.DoctorName.Contains(input) || x.Resume.Contains(input) || x.DepartmentName.Contains(input)).FirstOrDefault();

            if (searchDto!=null)
            {
                result =new SearchDto() 
                {
                    Id = searchDto.Id,
                    Flag = 0
                };
            }
            else
            {
                var hospital = _doContext.Hospitals.ToList().WhereIf(!string.IsNullOrEmpty(input), x => x.HospitalName.Contains(input) || x.HospitalAddress.Contains(input) || x.Resume.Contains(input)).FirstOrDefault();

                if (hospital != null)
                {
                    result = new SearchDto()
                    {
                        Id = hospital.Id,
                        Flag = 1
                    };
                }
                else
                {
                    var querys = from healthNews in _doContext.HealthNews.ToList()                                    
                                   join newsKinds in _doContext.NewsKinds.ToList()
                                   on healthNews.NewsKindId equals newsKinds.Id
                                   select new
                                   {
                                       Id = healthNews.Id,
                                       Title=healthNews.Title,
                                       Content=healthNews.Content,
                                       Name= newsKinds.Name,
                                   };

                    var healthNew = querys.WhereIf(!string.IsNullOrEmpty(input), x => x.Title.Contains(input) || x.Content.Contains(input) || x.Name.Contains(input)).FirstOrDefault();

                    if (healthNew!=null)
                    {
                        result = new SearchDto()
                        {
                            Id = healthNew.Id,
                            Flag = 2
                        };
                    }
                }
            }
            return result;
        }
    }
}