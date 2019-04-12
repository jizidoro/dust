using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Xerife.Entities;

namespace Xerife.Persistence
{
    /// <summary>
    /// Data Access Object de Tarefa
    /// </summary>
    public class TarefaChannelDao : BaseDao
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public TarefaChannelDao() : base()
        {

        }

        /// <summary>
        /// Inclui tarefa
        /// </summary>
        /// <param name="tarefa"></param>
        /// <returns></returns>
        public TarefaChannel IncluirTarefaChannel(TarefaChannel tarefa)
        {
            try
            {
                var novaTarefa = db.TarefaChannelSet.Add(tarefa);
                db.SaveChanges();
                return novaTarefa;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Lista as tarefas associadas ao projeto
        /// </summary>
        /// <param name="idProjeto"></param>
        /// <returns></returns>
        public List<TarefaChannel> ListarTarefasProjeto(int idProjeto)
        {
            db.Configuration.ProxyCreationEnabled = true;
            return db.TarefaChannelSet.Include(x=> x.Projeto).Where(x=> x.Projeto != null && x.Projeto.Id.Equals(idProjeto)).ToList();
        }

        /// <summary>
        /// Busca a tarefa
        /// </summary>
        /// <param name="idTarefa"></param>
        /// <returns></returns>
        public TarefaChannel GetTarefa(int idTarefa)
        {
            return db.TarefaChannelSet.Find(idTarefa);
        }

        /// <summary>
        /// Desfaz a associação de projeto e tarefas
        /// </summary>
        /// <param name="projeto"></param>
        /// <returns></returns>
        public bool DesassociarTarefasProjeto(Projeto projeto)
        {
            try
            {
                var listaTarefas = ListarTarefasProjeto(projeto.Id);

                foreach (var tar in listaTarefas)
                {
                    db.TarefaChannelSet.Remove(tar);
                }

                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}