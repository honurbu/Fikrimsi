using AutoMapper;
using Fikrimsi.Core.DTOs;
using Fikrimsi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Service.Mapping
{
    public class MapProfile : Profile
    {

        public MapProfile()
        {
            CreateMap<UserAppDto, UserApp>().ReverseMap(); 
            CreateMap<CreateUserDto, UserApp>().ReverseMap(); 
            CreateMap<CommentDto, Comment>().ReverseMap(); 
            CreateMap<TitleDto, Title>().ReverseMap(); 
        }
    }
}
