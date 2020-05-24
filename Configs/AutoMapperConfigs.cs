using AutoMapper;
using HospitalAPI.Applications.Answers;
using HospitalAPI.Applications.Departments.Dtos;
using HospitalAPI.Applications.DiseaseKinds;
using HospitalAPI.Applications.DiseaseKnowledge;
using HospitalAPI.Applications.Doctors.Dtos;
using HospitalAPI.Applications.HealthNews;
using HospitalAPI.Applications.Hospitals.Dtos;
using HospitalAPI.Applications.NewsKinds.Dtos;
using HospitalAPI.Applications.Problems;
using HospitalAPI.Applications.User.Dtos;
using HospitalAPI.Models;
using HospitalAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Configs
{
    public class AutoMapperConfigs:Profile
    {
        public AutoMapperConfigs()
        {
            CreateMap<CreateOrEditUserDto, Users>();
            CreateMap<UserDto, Users>();
            CreateMap<Users, UserDto>();
            CreateMap<Users, UserListDto>();

            CreateMap<RegisterInput, CreateOrEditUserDto>();
            CreateMap<CreateOrEditDoctorDto, Doctors>();
            CreateMap<Doctors, CreateOrEditDoctorDto>();           
             CreateMap<DoctorDto, Doctors>();
            CreateMap<Doctors, DoctorDto>();
            CreateMap<DoctorListDto, Doctors>();
            CreateMap<Doctors, DoctorListDto>();

            CreateMap<CreateOrEditHospitalDto, Hospitals>();
            CreateMap<Hospitals, CreateOrEditHospitalDto>();
            CreateMap<HospitalDto, Hospitals>();
            CreateMap<Hospitals, HospitalDto>();
            CreateMap<HospitalListDto, Hospitals>();
            CreateMap<Hospitals, HospitalListDto>();

            CreateMap<CreateOrEditDepartmentDto, Departments>();
            CreateMap<Departments, CreateOrEditDepartmentDto>();
            CreateMap<DepartmentDto, Departments>();
            CreateMap<Departments, DepartmentDto>();
            //CreateMap<DepartmentListDto, Departments>();
            CreateMap<Departments, DepartmentListDto>();

            CreateMap<CreateOrEditNewKindDto, NewsKinds>();
            CreateMap<NewsKinds, CreateOrEditNewKindDto>();
            CreateMap<NewsKindDto, NewsKinds>();
            CreateMap<NewsKinds, NewsKindDto>();
            CreateMap<NewsKindListDto, NewsKinds>();
            CreateMap<NewsKinds, NewsKindListDto>();

            CreateMap<CreateOrEditHealthNewDto, HealthNews>();
            CreateMap<HealthNews, CreateOrEditHealthNewDto>();
            CreateMap<HealthNewDto, HealthNews>();
            CreateMap<HealthNews, HealthNewDto>();
            CreateMap<HealthNewListDto, HealthNews>();
            CreateMap<HealthNews, HealthNewListDto>();

            CreateMap<CreateOrEditDiseaseKnowledgeDto, DiseaseKnowledge>();
            CreateMap<DiseaseKnowledge, CreateOrEditDiseaseKnowledgeDto>();
            CreateMap<DiseaseKnowledgeDto, DiseaseKnowledge>();
            CreateMap<DiseaseKnowledge, DiseaseKnowledgeDto>();
            CreateMap<DiseaseKnowledgeListDto, DiseaseKnowledge>();
            CreateMap<DiseaseKnowledge, DiseaseKnowledgeListDto>();

            CreateMap<CreateOrEditDiseaseKindsDto, DiseaseKinds>();
            CreateMap<DiseaseKinds, CreateOrEditDiseaseKindsDto>();
            CreateMap<DiseaseKindsDto, DiseaseKinds>();
            CreateMap<DiseaseKinds, DiseaseKindsDto>();
            CreateMap<DiseaseKindsListDto, DiseaseKinds>();
            CreateMap<DiseaseKinds, DiseaseKindsListDto>();

            CreateMap<CreateOrEditProblemDto, Problems>();
            CreateMap<Problems, CreateOrEditProblemDto>();
            CreateMap<ProblemDto, Problems>();
            CreateMap<Problems, ProblemDto>();
            CreateMap<ProblemListDto, Problems>();
            CreateMap<Problems, ProblemListDto>();

            CreateMap<CreateOrEditAnswerDto, Answers>();
            CreateMap<Answers, CreateOrEditAnswerDto>();
            CreateMap<AnswerDto, Answers>();
            CreateMap<Answers, AnswerDto>();
        }
    }
}
