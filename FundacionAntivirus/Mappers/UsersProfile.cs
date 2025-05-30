
using AutoMapper;
using FundacionAntivirus.Dto;
using FundacionAntivirus.Models;

namespace FundacionAntivirus.Mappers
{
    public class UsersProfile: Profile
    {
        public UsersProfile() 
        {
            CreateMap<users, UsersResponseDto>().ReverseMap();
            CreateMap<users, UsersRequestDto>().ReverseMap();

        }
    }
}