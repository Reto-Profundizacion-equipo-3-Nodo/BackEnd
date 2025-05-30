using AutoMapper;
using FundacionAntivirus.Dtos;
using FundacionAntivirus.Models;

namespace FundacionAntivirus.Mappers
{
    public class UserProfile: Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<User, UserRequestDto>().ReverseMap();

        }
    }
}