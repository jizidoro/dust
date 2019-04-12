using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xerife.Entities;

namespace Xerife.Persistence
{
    /// <summary>
    /// Data Access Object de Usuario
    /// </summary>
    public class UsuarioDao : BaseDao
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public UsuarioDao() : base()
        {

        }

        /// <summary>
        /// Consulta os usuários
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Usuario> ConsultarUsuarios()
        {
            return db.UsuarioSet.Include("Perfil");
        }

        /// <summary>
        /// Consulta os usuários
        /// </summary>
        /// <returns></returns>
        public Usuario GetUsuario(int id)
        {
            return db.UsuarioSet.Include("Perfil").FirstOrDefault(x => x.Id == id);
        }

        public bool AlterarPerfilUsuario(int idUsuario, int idPerfil)
        {
            try
            {
                var usuarioCadsatrado = db.UsuarioSet.FirstOrDefault(x => x.Id.Equals(idUsuario));

                if (usuarioCadsatrado == null)
                {
                    return false;
                }
                var novoPerfil = db.PerfilSet.FirstOrDefault(x => x.Id.Equals(idPerfil));

                usuarioCadsatrado.Perfil = novoPerfil;
                db.Entry(usuarioCadsatrado).State = EntityState.Modified;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IncluirUsuario(Usuario usuario)
        {
            try
            {
                var perfil = db.PerfilSet.FirstOrDefault(x => x.Id == usuario.Perfil.Id);
                usuario.Perfil = perfil;
                db.UsuarioSet.Add(usuario);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ExcluirUsuario(int id)
        {
            try
            {
                var usuarioExcluir = db.UsuarioSet.FirstOrDefault(x => x.Id == id);
                if (usuarioExcluir == null)
                {
                    return true;
                }
                db.UsuarioSet.Remove(usuarioExcluir);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica se o usuario está registrado ou não
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool IsRegistrado(string login)
        {
            return db.UsuarioSet.Any(x => x.Login.Equals(login));
        }

        /// <summary>
        /// Busca o perfil pelo login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public string GetPerfilUsuario(string login)
        {
            var user = db.UsuarioSet.Include("Perfil").Where(x => x.Login.Equals(login)).FirstOrDefault();
            if (user != null) { return user.Perfil.Nome; }

            return string.Empty;
        }

    }
}