using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Applications.Answers;
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
    public class AnswersController : Controller
    {
        private readonly DoContext _doContext;
        private readonly IMapper _mapper;
        public AnswersController(DoContext doContext, IMapper mapper)
        {
            _doContext = doContext;
            _mapper = mapper;
        }

        //获取所有回答
        [Route("GetDoctorAndPatientAnswers")]
        [HttpPost]
        public List<ChatMessagesDto> GetDoctorAndPatientAnswers(AnswerInputDto input)
        {
            List<ChatMessagesDto> Resultlist = new List<ChatMessagesDto>();

            var problemName = _doContext.Problems.FirstOrDefault(x => x.Id == input.ProblemId);
            if (problemName != null)
            {
                Resultlist.Add(new ChatMessagesDto()
                {
                    Id = problemName == null ? 0 : problemName.Id,
                    Content = problemName == null ? "" : problemName.Title,
                    Type = 1,
                    Pic = "/static/hm/images/6.jpg"
                });
            }

            var querys = from answers in _doContext.Answers.ToList()
                                       .Where(x => x.ProblemId == input.ProblemId)
                         join problems in _doContext.Problems.ToList()
                        on answers.ProblemId equals problems.Id
                       into JoinedEmpProblem
                         from problem in JoinedEmpProblem.DefaultIfEmpty()
                         select new ChatMessagesDto
                         {
                             Id = answers.Id,
                             Content = answers.Content,
                             Type = answers.Type,
                             Pic = answers.Type == 1? "/static/hm/images/6.jpg" : "/static/hm/images/yishen.jpg"
                         };

            var list = querys.OrderBy(x => x.Id).ToList();
            Resultlist.AddRange(list);

            return Resultlist;
        }


        //获取所有回答
        [Route("GetAnswers")]
        [HttpPost]
        public OutputList GetAnswers([FromBody] AnswerInput input)
        {
            var querys = from answers in _doContext.Answers.ToList()
                         join users in _doContext.Users.ToList() on answers.UserId equals users.Id
                         into JoinedEmpUser
                         from user in JoinedEmpUser.DefaultIfEmpty()
                         join problems in _doContext.Problems.ToList()
                         .Where(x=>x.Id==input.ProbliemId)
                         on answers.ProblemId equals problems.Id
                         into JoinedEmpProblem
                         from problem in JoinedEmpProblem.DefaultIfEmpty()
                         select new ProblemListDto
                         {
                             Id= answers.Id,
                             Title = problem == null ? "" : problem.Title,
                             Info = answers.Content,
                             Another = user == null ? "" : user.UserName,
                             UserId = user == null ? 0 : user.Id,
                             ProblemId= problem == null ? 0 : problem.Id,
                         };


            var totel = querys.Count();

            querys = querys.Where(x => x.ProblemId == input.ProbliemId);

            var result = _mapper.Map<List<ProblemListDto>>(querys);

            OutputList outputList = new OutputList
            {
                Totel = totel,
                Data = result
            };
            return outputList;
        }


        [Route("CreateOrEditAnswer")]
        [HttpPost]
        public async Task<AnswerDto> CreateOrEditAnswer([FromBody] CreateOrEditAnswerDto input)
        {
            var answer = _mapper.Map<Answers>(input);
            answer.Type = 0;
            if (input.Id.HasValue && input.Id.Value != 0)
            {
                _doContext.Answers.Update(answer);
            }
            else
            {
                answer.CreateTime = DateTime.Now;
                await _doContext.Answers.AddAsync(answer);
            }
            await _doContext.SaveChangesAsync();
            return _mapper.Map<AnswerDto>(answer);
        }

        [Route("UserAnswer")]
        [HttpPost]
        public async Task<AnswerDto> UserAnswer([FromBody] CreateOrEditAnswerDto input)
        {
            var answer = _mapper.Map<Answers>(input);
            answer.Type= 1;
            if (input.Id.HasValue && input.Id.Value != 0)
            {
                _doContext.Answers.Update(answer);
            }
            else
            {
                answer.CreateTime = DateTime.Now;
                await _doContext.Answers.AddAsync(answer);
            }
            await _doContext.SaveChangesAsync();
            return _mapper.Map<AnswerDto>(answer);
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