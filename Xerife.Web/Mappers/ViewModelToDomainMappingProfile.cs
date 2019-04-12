using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xerife.Entities;
using Xerife.Web.Models;

namespace Xerife.Web.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<VmProjeto, Projeto>();
            });
        }
    }
}