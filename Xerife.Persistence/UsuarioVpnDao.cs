using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xerife.Entities;
using Xerife.Entities.Enum;
using Xerife.Persistence.Util;

namespace Xerife.Persistence
{
    /// <summary>
    /// Data Access Object de UsuarioVpn
    /// </summary>
    public class UsuarioVpnDao
    {
        /// <summary>
        /// Database
        /// </summary>
        private Model1Container db = new Model1Container();

        /// <summary>
        /// Inclui um usuario vpn
        /// </summary>
        /// <param name="usuarioVpn"></param>
        /// <param name="responsavel"></param>
        /// <returns></returns>
        public bool IncluirUsuarioVpn(UsuarioVpn usuarioVpn, string justificativa, string responsavel)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var acessoVpn = db.UsuarioVpnSet.Add(usuarioVpn);

                    VpnHistorico vh = new VpnHistorico();
                    vh.Inicio = usuarioVpn.Inicio;
                    vh.Fim = usuarioVpn.Fim;
                    vh.Acao = (int)AcaoVpn.Inclusão;
                    vh.Responsavel = responsavel;
                    vh.DataAcao = DateTime.Now;
                    vh.Usuario = usuarioVpn.Login;
                    vh.Justificativa = justificativa;

                    db.VpnHistoricoSet.Add(vh);

                    ActiveDirectoryUtil.ConcederAcesso(usuarioVpn.Login);

                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// Exclui um usuário vpn
        /// </summary>
        /// <param name="id"></param>
        /// <param name="responsavel"></param>
        /// <returns></returns>
        public bool ExcluirUsuarioVpn(string login, string responsavel)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    UsuarioVpn usuarioVpn = db.UsuarioVpnSet.Where(x => x.Login.Equals(login)).FirstOrDefault();
                    if (usuarioVpn != null)
                    {
                        db.UsuarioVpnSet.Remove(usuarioVpn);
                    }

                    VpnHistorico vh = new VpnHistorico();
                    vh.Acao = (int)AcaoVpn.Exclusão;
                    vh.Responsavel = responsavel;
                    vh.DataAcao = DateTime.Now;
                    vh.Usuario = login;
                    vh.Justificativa = "Revogação manual";

                    db.VpnHistoricoSet.Add(vh);
                    ActiveDirectoryUtil.RevogarAcesso(login);

                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// Exclui um usuário vpn
        /// </summary>
        /// <param name="id"></param>
        /// <param name="responsavel"></param>
        /// <returns></returns>
        public bool ConcederAcessoVitalicio(string login, string responsavel)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    UsuarioVpn usuarioVpn = db.UsuarioVpnSet.Where(x => x.Login.Equals(login)).FirstOrDefault();
                    if (usuarioVpn != null)
                    {
                        db.UsuarioVpnSet.Remove(usuarioVpn);
                    }

                    VpnHistorico vh = new VpnHistorico();

                    vh.Acao = (int)AcaoVpn.Renovação;
                    vh.Responsavel = responsavel;
                    vh.DataAcao = DateTime.Now;
                    vh.Usuario = login;
                    vh.Justificativa = "Acesso Vitalício";

                    db.VpnHistoricoSet.Add(vh);

                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// Remove acessos temporarios vpn
        /// </summary>
        /// <returns></returns>
        public bool RemoverAcessosTemporariosVpn()
        {
            var usuariosTemporarios = db.UsuarioVpnSet.Where(x => x.Fim < DateTime.Today);

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var u in usuariosTemporarios)
                    {
                        VpnHistorico vh = new VpnHistorico();
                        vh.Acao = (int)AcaoVpn.Expiração;
                        vh.Responsavel = "Rotina Automática";
                        vh.DataAcao = DateTime.Now;
                        vh.Usuario = u.Login;
                        vh.Justificativa = "Expiração automática";

                        db.VpnHistoricoSet.Add(vh);

                        db.UsuarioVpnSet.Remove(u);
                        ActiveDirectoryUtil.RevogarAcesso(u.Login);
                    }

                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    foreach (var u in usuariosTemporarios)
                    {
                        ActiveDirectoryUtil.ConcederAcesso(u.Login);
                    }
                    return false;
                }
            }
        }

        /// <summary>
        /// Consulta lista de usuarios
        /// </summary>
        /// <returns></returns>
        public List<UsuarioVpn> ConsultarComboUsuarioAD()
        {
            return ActiveDirectoryUtil.ListarUsuarios().Select(x => new UsuarioVpn() { Login = x.SamAccountName, Nome = x.Name }).ToList();
        }

        /// <summary>
        /// Consulta lista de usuarios vpn
        /// </summary>
        /// <returns></returns>
        public List<UsuarioVpn> ConsultarListaUsuariosAD()
        {
            return ActiveDirectoryUtil.GetUsuarioVpns().Select(x => new UsuarioVpn() { Login = x.SamAccountName, Nome = x.Name }).ToList();
        }

        /// <summary>
        /// Consulta lista de usuarios vpn
        /// </summary>
        /// <returns></returns>
        public List<UsuarioVpn> ConsultarListaUsuarioVpn()
        {
            return db.UsuarioVpnSet.ToList();
        }

        /// <summary>
        /// Busca usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UsuarioVpn GetUsuario(int id)
        {
            return db.UsuarioVpnSet.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Verifica se o usuario está cadastrado na base de dados
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool IsUsuarioCadastrado(string usuario)
        {
            return db.UsuarioVpnSet.Any(x => x.Login.Equals(usuario));
        }


        /// <summary>
        /// Inclui um usuario vpn
        /// </summary>
        /// <param name="usuarioVpn"></param>
        /// <param name="responsavel"></param>
        /// <returns></returns>
        public bool ProlongarAcesso(UsuarioVpn usuarioVpn, string justificativa, string responsavel)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var usuarioCadastrado = db.UsuarioVpnSet.FirstOrDefault(x=>x.Id.Equals(usuarioVpn.Id));
                    if (usuarioCadastrado == null)
                    {
                        return false;
                    }
                    usuarioCadastrado.Fim = usuarioVpn.Fim;
                    db.Entry(usuarioCadastrado).State = EntityState.Modified;

                    VpnHistorico vh = new VpnHistorico();
                    vh.Inicio = usuarioVpn.Inicio;
                    vh.Fim = usuarioVpn.Fim;
                    vh.Acao = (int)AcaoVpn.Renovação;
                    vh.Responsavel = responsavel;
                    vh.DataAcao = DateTime.Now;
                    vh.Usuario = usuarioCadastrado.Login;
                    vh.Justificativa = justificativa;

                    db.VpnHistoricoSet.Add(vh);

                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }
        }
    }
}