using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xerife.Entities;
using Xerife.Entities.Enum;
using Xerife.Web.Models;

namespace Xerife.Web.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        /// <summary>
        /// Nome do Profile de configuração
        /// </summary>
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        /// <summary>
        /// Configura mapeamentos
        /// </summary>
        public DomainToViewModelMappingProfile()
        {
            CreateMap<VpnHistorico, VmHistoricoVpn>().AfterMap((c, vm) =>
            {
                vm.DataAcao = c.DataAcao.ToString("dd/MM/yyyy");
                vm.Inicio = c.Inicio.HasValue ? c.Inicio.Value.ToString("dd/MM/yyyy") : string.Empty;
                vm.Fim = c.Fim.HasValue ? c.Fim.Value.ToString("dd/MM/yyyy") : string.Empty;
                vm.Acao = ((AcaoVpn)c.Acao).ToString();
            }
            );
            CreateMap<LogIntegracao, VmLogIntegracao>().AfterMap((c, vm) =>
            {
                vm.Data = c.Data.ToString("dd/MM/yyyy");
                vm.Registro = c.Registro;
                vm.Projeto = c.Projeto;
                vm.Status = c.Status ? "Erro" : "Info";
            }
            );
            CreateMap<ParametroSistema, VmParametroSistema>().AfterMap((c, vm) =>
            {
                vm.NomePerfil = c.Perfil.Nome;
            }
            );
            CreateMap<Usuario, VmUsuario>().AfterMap((c, vm) =>
            {
                vm.IdPerfil = c.Perfil.Id;
                vm.NomePerfil = c.Perfil.Nome;
            }
            );
            CreateMap<UsuarioVpn, VmUsuarioVpn>().AfterMap((c, vm) =>
            {
                vm.Id = c.Id;
                vm.Login = c.Login;
                vm.DataFim = c.Fim.ToString("dd/mM/yyyy");
            }
            );
        }
    }
}