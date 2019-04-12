using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xerife.Web.ChannelProjetoWebService;
using Xerife.Business.Util;

namespace Xerife.Web.Controllers
{
    /// <summary>
    /// Controller do channel
    /// </summary>
    public class ChannelController : Controller
    {
        /// <summary>
        /// Busca lista de projetos pelo gerente
        /// </summary>
        /// <param name="gerente"></param>
        /// <returns></returns>
        public JsonResult GetProjetos()
        {
            return Json(ChannelUtil.ObterProjetosAtivos().OrderBy(x => x.nomeProjeto), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Busca lista de tarefas do projeto
        /// </summary>
        /// <param name="projetoId"></param>
        /// <returns></returns>
        public JsonResult GetTarefasProjeto(int projetoId)
        {
            return Json(ChannelUtil.GetTarefasProjeto(projetoId).OrderBy(x => x.dataInicio), JsonRequestBehavior.AllowGet);
        }
    }
}