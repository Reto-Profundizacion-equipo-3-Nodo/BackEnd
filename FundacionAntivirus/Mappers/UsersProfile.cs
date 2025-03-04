
using AutoMapper;
using FundacionAntivirus.Dto;
using FundacionAntivirus.Models;

namespace FundacionAntivirus.Mappers
{
    public class UsersProfile: Profile
    {
        public UsersProfile() 
        {
            CreateMap<Users, UsersResponseDto>().ReverseMap();
            CreateMap<Users, UsersRequestDto>().ReverseMap();

        }
    }
}