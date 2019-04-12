using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xerife.Entities;

namespace Xerife.Persistence
{
    /// <summary>
    /// Data Access Object de Usuario
    /// </summary>
    public class ParametroSistemaDao : BaseDao
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public ParametroSistemaDao() : base()
        {

        }
        public IEnumerable<ParametroSistema> ConsultarParametroSistemaPorPerfil(string perfil)
        {
            return db.ParametroSistemaSet.Where(x=>x.Perfil.Nome.Equals(perfil));
        }


        public IEnumerable<ParametroSistema> ConsultarParametroSistema()
        {
            return db.ParametroSistemaSet;
        }

        public bool AlterarParametroSistema(ParametroSistema parametro)
        {
            try
            {
                var parametroCadastrado = db.ParametroSistemaSet.FirstOrDefault(x => x.Id.Equals(parametro.Id));

                if (parametroCadastrado == null)
                {
                    return false;
                }

                parametroCadastrado.Nome = parametro.Nome;
                parametroCadastrado.Valor = parametro.Valor;
                parametroCadastrado.Descricao = parametro.Descricao;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public ParametroSistema GetById(int id)
        {
            return db.ParametroSistemaSet.Include("Perfil").Where(x => x.Id == id).FirstOrDefault();
        }

        public ParametroSistema GetBySigla(string sigla)
        {
            return db.ParametroSistemaSet.Where(x => x.Sigla == sigla).FirstOrDefault();
        }

    }
}