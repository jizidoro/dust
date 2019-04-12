using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xerife.Entities;
using Xerife.Persistence;

namespace Xerife.Business
{
    /// <summary>
    /// Usuario de vpn
    /// </summary>
    public class UsuarioVpnBus
    {
        /// <summary>
        /// Data Access Object de UsuarioVpn
        /// </summary>
        private UsuarioVpnDao usuarioVpnDao = new UsuarioVpnDao();
        private ParametroSistemaDao parametroDao = new ParametroSistemaDao();
        private const string SILGA_PARAMETRO_LIMITE = "DURACAO_MAXIMA_VPN";

        /// <summary>
        /// Popula combo de usuarios
        /// </summary>
        /// <returns></returns>
        public List<UsuarioVpn> ConsultarComboUsuarioAD()
        {
            return usuarioVpnDao.ConsultarComboUsuarioAD();
        }

        /// <summary>
        /// Lista os usuarios
        /// </summary>
        /// <returns></returns>
        public List<UsuarioVpn> ConsultarListaUsuariosAD()
        {
            return usuarioVpnDao.ConsultarListaUsuariosAD();
        }

        /// <summary>
        /// Lista os usuarios Vpn
        /// </summary>
        /// <returns></returns>
        public List<UsuarioVpn> ConsultarListaUsuarioVpn()
        {
            return usuarioVpnDao.ConsultarListaUsuarioVpn();
        }

        /// <summary>
        /// Inclui um usuario Vpn
        /// </summary>
        /// <param name="usuarioVpn"></param>
        /// <param name="responsavel"></param>
        /// <returns></returns>
        public string IncluirUsuarioVpn(UsuarioVpn usuarioVpn, string justificativa, string responsavel)
        {
            string msg = ValidaRegrasNegocio(true, usuarioVpn, justificativa);
            if (!string.IsNullOrEmpty(msg))
            {
                return msg;
            }
            var resultado = usuarioVpnDao.IncluirUsuarioVpn(usuarioVpn, justificativa, responsavel);
            if (resultado)
            {
                return string.Empty;
            }
            else
            {
                return "Falha ao conceder acesso usuário.";
            }
        }

        private string ValidaRegrasNegocio(bool inclusao, UsuarioVpn usuarioVpn, string justificativa)
        {
            if (!inclusao)
            {
                var parametro = parametroDao.GetBySigla(SILGA_PARAMETRO_LIMITE);
                if (parametro != null)
                {
                    var limite = Convert.ToInt32(parametro.Valor);
                    if (DateTime.Today.AddDays(limite) <= usuarioVpn.Fim)
                    {
                        return "Período máximo é de " + limite.ToString() + " dias.";
                    }
                }
                if (usuarioVpn.Fim.Date < DateTime.Now.Date)
                {
                    return "Data fim menor do que a data de hoje.";
                }
                if (string.IsNullOrEmpty(justificativa))
                {
                    return "Justificativa obrigatória.";
                }
                if (!usuarioVpnDao.IsUsuarioCadastrado(usuarioVpn.Login))
                {
                    return "Usuário com acesso vitalício.";
                }
            }
            else
            {
                if (usuarioVpnDao.IsUsuarioCadastrado(usuarioVpn.Login))
                {
                    return "Usuário já possui permissão";
                }
            }
            return "";
        }

        /// <summary>
        /// Exclui um usuário vpn
        /// </summary>
        /// <param name="id"></param>
        /// <param name="responsavel"></param>
        /// <returns></returns>
        public bool ExcluirUsuarioVpn(string login, string responsavel)
        {
            return usuarioVpnDao.ExcluirUsuarioVpn(login, responsavel);
        }

        /// <summary>
        /// Exclui um usuário vpn
        /// </summary>
        /// <param name="id"></param>
        /// <param name="responsavel"></param>
        /// <returns></returns>
        public bool ConcederAcessoVitalicio(string login, string responsavel)
        {
            return usuarioVpnDao.ConcederAcessoVitalicio(login, responsavel);
        }

        /// <summary>
        /// Remover acessos temporarios vpn
        /// </summary>
        public void RemoverAcessosTemporariosVpn()
        {
            usuarioVpnDao.RemoverAcessosTemporariosVpn();
        }

        /// <summary>
        /// Busca usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UsuarioVpn GetUsuario(int id)
        {
            return usuarioVpnDao.GetUsuario(id);
        }


        /// <summary>
        /// Inclui um usuario Vpn
        /// </summary>
        /// <param name="usuarioVpn"></param>
        /// <param name="responsavel"></param>
        /// <returns></returns>
        public string ProlongarAcesso(UsuarioVpn usuarioVpn, string justificativa, string responsavel)
        {
            string msg = ValidaRegrasNegocio(true, usuarioVpn, justificativa);
            if (!string.IsNullOrEmpty(msg))
            {
                return msg;
            }
            var resultado = usuarioVpnDao.ProlongarAcesso(usuarioVpn, justificativa, responsavel);
            if (resultado)
            {
                return string.Empty;
            }
            else
            {
                return "Falha ao prolongar acesso.";
            }
        }

    }
}