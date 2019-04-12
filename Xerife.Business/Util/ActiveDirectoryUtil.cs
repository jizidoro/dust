using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices.AccountManagement;
using Xerife.Entities.Util;
using System.Configuration;

namespace Xerife.Business.Util
{
    public static class ActiveDirectoryUtil
    {
        private static string _adUser = "vixintegracao";// ConfigurationManager.AppSettings["IntegrationUser"];
        private static string _adUserPassword = "vixteam*2015";// ConfigurationManager.AppSettings["IntegrationUserPassword"];
        private static string _grupoVpn = "GR-VPN-VIXTEAM";
        private static string _adServer = "JUREMA";

        public static List<UsuarioVpnViewModel> GetUsuarioVpns()
        {
            List<UsuarioVpnViewModel> usuariosVpn = new List<UsuarioVpnViewModel>();
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, _adServer, _adUser, _adUserPassword))
            {
                GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, _grupoVpn);
                foreach (var u in group.Members)
                {
                    var tempUsuarioVpn = new UsuarioVpnViewModel()
                    {
                        Login = u.SamAccountName,
                        Nome = u.Name,
                        Inicio = string.Empty,
                        Fim = string.Empty
                    };
                    usuariosVpn.Add(tempUsuarioVpn);
                }
            }
            return usuariosVpn;
        }
    }
}