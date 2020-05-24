using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Applications.commons;
using HospitalAPI.Applications.Problems;
using HospitalAPI.Models;
using HospitalAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    //如果访问的路径为404则需要配置路由
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemsController : Controller
    {
        private readonly DoContext _doContext;
        private readonly IMapper _mapper;
        public ProblemsController(DoContext doContext, IMapper mapper)
        {
            _doContext = doContext;
            _mapper = mapper;
        }

        [Route("GetAllProblems")]
        [HttpPost]
        public OutputList GetAllProblems()
        {
            var querys = from problems in _doContext.Problems.ToList()
                         join answers in _doContext.Answers.ToList()
                         on problems.Id equals answers.ProblemId into JoinedEmpAnswer
                         from answer in JoinedEmpAnswer.DefaultIfEmpty()
                         join users in _doContext.Users.ToList()
                         on problems.UserId equals users.Id into JoinedEmpUser
                         from user in JoinedEmpUser.DefaultIfEmpty()

                         select new ProblemListDto
                         {
                             Id = problems.Id,
                             Title= problems.Title,
                             Info= answer==null?"": answer.Content,
                             Another = "医生",//user ==null?"":user.UserName,
                             UserId = user == null ? 0 : user.Id,
                             Img= "../../static/hen/问.jpg"
                         };

            var totel = querys.Count();

             querys = querys.GroupBy(x => x.Id).Select(a => new ProblemListDto {
                Id = a.Key,
                Title = a.FirstOrDefault().Title,
                Another = a.FirstOrDefault().Another,
                Info = a.FirstOrDefault().Info,
                UserId = a.FirstOrDefault().UserId,
                Img = a.FirstOrDefault().Img
            }).ToList();

            var result = _mapper.Map<List<ProblemListDto>>(querys);

            OutputList outputList = new OutputList
            {
                Totel = totel,
                Data = result
            };
            return outputList;
        }

        [Route("GetProblemList")]
        [HttpPost]
        public OutputList GetProblemList()
        {
            var querys = from problems in _doContext.Problems.ToList()
                         join answers in _doContext.Answers.ToList()
                         on problems.Id equals answers.ProblemId into JoinedEmpAnswer
                         from answer in JoinedEmpAnswer.DefaultIfEmpty()
                         join users in _doContext.Users.ToList()
                         on answer.UserId equals users.Id into JoinedEmpUser
                         from user in JoinedEmpUser.DefaultIfEmpty()

                         select new ProblemListDto
                         {
                             Id = problems.Id,
                             Title = problems.Title,
                             Info = answer == null ? "" : answer.Content,
                             Another = user ==null?"匿名":user.UserName,
                             UserId = user == null ? 0 : user.Id,
                             Img = "../../static/hen/问.jpg"
                         };

            var totel = querys.Count();

            querys = querys.GroupBy(x => x.Id).Select(a => new ProblemListDto
            {
                Id = a.Key,
                Title = a.FirstOrDefault().Title,
                Another = a.FirstOrDefault().Another,
                Info = a.FirstOrDefault().Info,
                UserId = a.FirstOrDefault().UserId,
                Img = a.FirstOrDefault().Img
            }).ToList();

            var result = _mapper.Map<List<ProblemListDto>>(querys);

            OutputList outputList = new OutputList
            {
                Totel = totel,
                Data = result
            };
            return outputList;
        }

        [Route("GetUserProblems")]
        [HttpPost]
        public OutputList GetUserProblems([FromBody] ProblemInput input)
        {
            var querys = from problems in _doContext.Problems.ToList()
                         join answers in _doContext.Answers.ToList()
                         on problems.Id equals answers.ProblemId into JoinedEmpAnswer
                         from answer in JoinedEmpAnswer.DefaultIfEmpty()
                         join users in _doContext.Users.ToList()
                          on answer.UserId equals users.Id into JoinedEmpUser
                         from user in JoinedEmpUser.DefaultIfEmpty()
                         select new ProblemListDto
                         {
                             Id = problems.Id,
                             Title = problems.Title,
                             Info = answer == null ? "" : answer.Content,
                             Another =user == null ? "匿名" : user.UserName,
                             UserId = user == null ? 0 : user.Id,
                             Img = "../../static/hen/问.jpg'"
                         };
            querys = querys.Where(x => x.UserId == input.UserId);

            querys = querys.GroupBy(x => x.Id).Select(a => new ProblemListDto
            {
                Id = a.Key,
                Title = a.FirstOrDefault().Title,
                Another = a.FirstOrDefault().Another,
                Info = a.FirstOrDefault().Info,
                UserId = a.FirstOrDefault().UserId,
                Img = a.FirstOrDefault().Img
            }).ToList();

            var totel = querys.Count();

            var result = _mapper.Map<List<ProblemListDto>>(querys);

            OutputList outputList = new OutputList
            {
                Totel = totel,
                Data = result
            };
            return outputList;
        }

        //[Route("GetProblemDetails")]
        //[HttpPost]
        //public OutputList GetProblemDetails([FromBody] Filters input)
        //{
        //    var querys = from problems in _doContext.Problems.ToList()
        //                                .WhereIf(!string.IsNullOrEmpty(input.Query), x => x.Title.Contains(input.Query))
        //                 join users in _doContext.Users.ToList() on problems.UserId equals users.Id
        //                 select new ProblemListDto
        //                 {
        //                     Id = problems.Id,
        //                     UserId = users.Id,
        //                     UserName = users.UserName,
        //                     Title = problems.Title,
        //                     CreateTime = problems.CreateTime
        //                 };

        //    var totel = querys.Count();

        //    if (input.PageSize == 0 || input.PageNumber == 0)
        //    {

        //    }
        //    else
        //    {
        //        querys = querys.Skip((input.PageNumber - 1) * input.PageSize)
        //        .Take(input.PageSize);
        //    }

        //    var result = _mapper.Map<List<ProblemListDto>>(querys);

        //    OutputList outputList = new OutputList
        //    {
        //        Totel = totel,
        //        Data = result
        //    };
        //    return outputList;
        //}

        //[Route("GetUserProblems")]
        //[HttpPost]
        //public OutputList GetUserProblems([FromBody] ProblemInput input)
        //{
        //    var querys = from problems in _doContext.Problems.ToList()
        //                                .WhereIf(!string.IsNullOrEmpty(input.Query), x => x.Title.Contains(input.Query))
        //                 join users in _doContext.Users.ToList()
        //                     .WhereIf(input.UserId != 0, x => x.Id == input.UserId)
        //               on problems.UserId equals users.Id
        //                 select new ProblemListDto
        //                 {
        //                     Id = problems.Id,
        //                     UserId = users.Id,
        //                     UserName = users.UserName,
        //                     Title = problems.Title,
        //                     CreateTime = problems.CreateTime
        //                 };

        //    var totel = querys.Count();

        //    if (input.PageSize == 0 || input.PageNumber == 0)
        //    {

        //    }
        //    else
        //    {
        //        querys = querys.Skip((input.PageNumber - 1) * input.PageSize)
        //        .Take(input.PageSize);
        //    }

        //    var result = _mapper.Map<List<ProblemListDto>>(querys);

        //    OutputList outputList = new OutputList
        //    {
        //        Totel = totel,
        //        Data = result
        //    };
        //    return outputList;
        //}


        [HttpGet("GetProblemByID/{Id}")]
        public async Task<ProblemDto> GetProblemByID(int Id)
        {
            var Problem = _doContext.Problems.FirstOrDefault(x => x.Id == Id);
            return _mapper.Map<ProblemDto>(Problem);
        }

        [Route("CreateOrEditProblem")]
        [HttpPost]
        public async Task<ProblemDto> CreateOrEditProblem([FromBody] CreateOrEditProblemDto input)
        {
            var problem = _mapper.Map<Problems>(input);
            if (input.Id.HasValue && input.Id.Value != 0)
            {                
                _doContext.Problems.Update(problem);
            }
            else
            {
                problem.CreateTime = DateTime.Now;
                problem.Status = 0;
                await _doContext.Problems.AddAsync(problem);
            }
            await _doContext.SaveChangesAsync();
            return _mapper.Map<ProblemDto>(problem);
        }

        [HttpDelete("Delete/{Id}")]
        public async Task Delete(int Id)
        {
            var Problem = _doContext.Problems.SingleOrDefault(x => x.Id == Id);
            if (Problem == null)
            {

            }
            else
            {
                _doContext.Problems.Remove(Problem);
                await _doContext.SaveChangesAsync();
            }
        }
    }
}