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
    /// Ações do usuário
    /// </summary>
    public class UsuarioBus
    {
        /// <summary>
        /// Data Access Object do usuario
        /// </summary>
        private UsuarioDao usuarioDao = new UsuarioDao();

        /// <summary>
        /// Consulta usuários
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Usuario> ConsultarUsuarios()
        {
            return usuarioDao.ConsultarUsuarios();
        }
        /// <summary>
        /// Consulta usuários
        /// </summary>
        /// <returns></returns>
        public Usuario GetUsuario(int id)
        {
            return usuarioDao.GetUsuario(id);
        }
        public bool AlterarPerfilUsuario(int idUsuario, int idPerfil)
        {
            return usuarioDao.AlterarPerfilUsuario(idUsuario, idPerfil);
        }
        public bool IncluirUsuario(Usuario usuario)
        {
            return usuarioDao.IncluirUsuario(usuario);
        }
        public bool ExcluirUsuario(int id)
        {
            return usuarioDao.ExcluirUsuario(id);
        }

        /// <summary>
        /// Verifica se o usuário está registrado
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool IsRegistrado(string login)
        {
            return usuarioDao.IsRegistrado(login);
        }

        /// <summary>
        /// Busca o perfil do usuário
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public string GetPerfilUsuario(string login)
        {
            return usuarioDao.GetPerfilUsuario(login);
        }

    }
}