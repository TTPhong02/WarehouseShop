using AutoMapper;
using BE_WAREHOUSE.Core.DTOs;
using BE_WAREHOUSE.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, Users>();
            CreateMap<Orders, OrderDto>();
        }
    }
}
