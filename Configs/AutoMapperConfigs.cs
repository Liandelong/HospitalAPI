using AutoMapper;
using HospitalAPI.Applications.Departments.Dtos;
using HospitalAPI.Applications.Doctors.Dtos;
using HospitalAPI.Applications.HealthNews;
using HospitalAPI.Applications.Hospitals.Dtos;
using HospitalAPI.Applications.NewsKinds.Dtos;
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
        }
    }
}
