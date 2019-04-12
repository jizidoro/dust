using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using Xerife.Entities;

namespace Xerife.Persistence.Util
{
    public static class ActiveDirectoryUtil
    {
        private static string _adUser = ConfigurationManager.AppSettings["IntegrationUser"];
        private static string _adUserPassword = ConfigurationManager.AppSettings["IntegrationUserPassword"];
        private static string _grupoVpn = "GR-VPN-VIXTEAM";
        private static string _adServer = "JUREMA";

        public static void ConcederAcesso(string login)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, _adServer, _adUser, _adUserPassword))
            {
                var usuario = UserPrincipal.FindByIdentity(pc, login);
                if (usuario != null)
                {
                    var group = GroupPrincipal.FindByIdentity(pc, _grupoVpn);
                    if (!usuario.IsMemberOf(group))
                    {
                        group.Members.Add(pc, IdentityType.SamAccountName, login);
                        group.Save();
                    }
                }
            }
        }

        public static void RevogarAcesso(string login)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, _adServer, _adUser, _adUserPassword))
            {
                var usuario = UserPrincipal.FindByIdentity(pc, login);
                if (usuario != null)
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, _grupoVpn);
                    if (usuario.IsMemberOf(group))
                    {
                        group.Members.Remove(pc, IdentityType.SamAccountName, login);
                        group.Save();
                    }
                }
            }
        }

        public static IEnumerable<Principal> GetUsuarioVpns()
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, _adServer, _adUser, _adUserPassword))
            {
                GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, _grupoVpn);
                if (group != null)
                {
                    foreach (var groupMember in group.Members)
                    {
                        yield return groupMember;
                    }
                }
            }
        }

        public static IEnumerable<Principal> ListarUsuarios()
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, _adServer, _adUser, _adUserPassword))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(pc)))
                {
                    UserPrincipal searchTemplate = new UserPrincipal(pc);
                    searchTemplate.Enabled = true;
                    searcher.QueryFilter = searchTemplate;
                    foreach (var result in searcher.FindAll().Where(x => !x.DistinguishedName.Contains("Recursos") && x.DistinguishedName.Contains("Colaboradores")))
                    {
                        yield return result;
                    }
                }
            }
        }
    }
}
