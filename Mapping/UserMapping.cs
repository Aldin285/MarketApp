using System;
using MarketApp.Models;
using MarketApp.DTO;
using Microsoft.VisualBasic;
using AutoMapper;
using WebApplication1.DTO.User;

namespace MarketApp.Mapping;

public class UserMappingProfile : Profile
{
  public UserMappingProfile()
    {
        // CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }

    
}
