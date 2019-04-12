using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xerife.Business.Util;
using Xerife.Entities;
using Xerife.Entities.Enum;
using Xerife.Entities.Util;
using Xerife.Persistence;

namespace Xerife.Business
{
    public class ParametroSistemaBus
    {
        /// <summary>
        /// Data Access Object de Projeto
        /// </summary>
        private ParametroSistemaDao parametroSistemaDao = new ParametroSistemaDao();
        private UsuarioDao usuarioDao = new UsuarioDao();

        /// <summary>
        /// Altera um projeto
        /// </summary>
        /// <param name="projeto"></param>
        /// <returns></returns>
        public bool AlterarParametroSistema(ParametroSistema parametro)
        {
            return parametroSistemaDao.AlterarParametroSistema(parametro);
        }
        /// <summary>
        /// Consulta os projetos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParametroSistema> ConsultarParametroSistemaPorPerfil(string usuario)
        {
            string perfilUsuario = usuarioDao.GetPerfilUsuario(usuario);
            if (perfilUsuario.Equals("Administrador"))
            {
                return parametroSistemaDao.ConsultarParametroSistema();
            }
            else
            {
                return parametroSistemaDao.ConsultarParametroSistemaPorPerfil(perfilUsuario);
            }
        }

        public ParametroSistema GetById(int id)
        {
            return parametroSistemaDao.GetById(id);
        }

    }
}
