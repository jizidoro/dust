using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Xerife.Business;
using Xerife.Business.Util;
using Xerife.Entities;
using Xerife.Entities.Util;
using Xerife.Web.Controllers;
using Xerife.Web.Util;

namespace Xerife.Web
{
    /// <summary>
    /// Controller das telas de projetos
    /// </summary>
    [CustomAuthorizeAttribute(Roles = "Administrador, Gerente, PMO")]
    public class ProjetoController : Controller
    {
        private ProjetoBus bus = new ProjetoBus();
        private TfsController tfsController = new TfsController();

        /// <summary>
        /// Página principal
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Lista os projetos na grid
        /// </summary>
        /// <returns></returns>
        public JsonResult ListarProjetos()
        {
            return Json(bus.ConsultarProjetos(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Detalhes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            Projeto projeto = bus.GetProjeto(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }

        /// <summary>
        /// Busca o projeto pelo seu id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetProjeto(int id)
        {
            return Json(bus.GetProjeto(id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// View de criação de projeto
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult AlterarStatus(int id)
        {
            bus.AlterarStatusProjeto(id);
            return RedirectToAction("Index");
        }

        public ActionResult ExecutarIntegracaoChannelTfs(int idProjeto = 0)
        {
            var resultadoExecucao = bus.ExecutarIntegracaoChannelTfs(Entities.Enum.ModoExecucaoIntegracao.Manual, idProjeto);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Inclui um novo projeto ou altera um existente
        /// </summary>
        /// <param name="projeto"></param>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Incluir(Projeto projeto)
        {
            bool sucesso = false;
            projeto.UsuarioGerente = User.Identity.Name.Split('\\')[1];

            if (!projeto.Id.Equals(0))
            {
                sucesso = bus.Alterar(projeto);
            }
            else
            {
                sucesso = bus.Incluir(projeto);
            }

            return Json(new { status = sucesso,  redirectUrl = Url.Action("Index", "Projeto") });
        }

        /// <summary>
        /// Carrega os detalhes do projeto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Projeto projeto = bus.GetProjeto(id.Value);

            if (projeto == null)
            {
                return HttpNotFound();
            }

            var listaCollections = TfsUtil.GetProjectCollection(projeto.TfsUrl);

            if (listaCollections != null && listaCollections.value != null)
            {
                listaCollections.value = listaCollections.value.OrderBy(x => x.name).ToArray();

                var lista = listaCollections.value.Select(x => x.name).ToList();

                ViewBag.tfsCollections = PopularDropDown(lista);
            }

            var listaProjects = TfsUtil.GetProjects(projeto.TfsUrl, projeto.TfsCollection);

            if (listaProjects != null && listaProjects.value != null)
            {
                listaProjects.value = listaProjects.value.OrderBy(x => x.name).ToArray();

                var lista = listaProjects.value.Select(x => x.name).ToList();

                ViewBag.tfsProjects = PopularDropDown(lista);
            }

            var listaTeams = TfsUtil.GetTeams(projeto.TfsUrl, projeto.TfsCollection, projeto.TfsProject);

            if (listaTeams != null && listaTeams.value != null)
            {
                listaTeams.value = listaTeams.value.OrderBy(x => x.name).ToArray();

                var lista = listaTeams.value.Select(x => x.name).ToList();

                ViewBag.listaTeams = PopularDropDown(lista);
            }

            var listaAreaPath = (IEnumerable<AreaPathTreeViewModel>)tfsController.GetAreaPathTree(projeto.TfsUrl, projeto.TfsCollection, projeto.TfsProject).Data; //TfsUtil.GetAreaPathTree(projeto.TfsUrl, projeto.TfsCollection, projeto.TfsProject);

            if (listaAreaPath != null && listaAreaPath != null)
            {
                listaAreaPath = listaAreaPath.OrderBy(x => x.name).ToList();

                var lista = listaAreaPath.Select(x => x.name).ToList();

                ViewBag.areaPath = PopularDropDown(lista);
            }

            ViewBag.idProjeto = id.Value;

            projeto.TarefaChannel = bus.ListarTarefasDoProjeto(projeto.Id);

            var listaTarefasSelecionadas = ViewBag.listaTarefasSelecionadas = projeto.TarefaChannel.Count.Equals(0) ? null : projeto.TarefaChannel.Select(x => x.IdTarefaChannel).ToList();

            var listaIteracoesSelecionadas = new List<object>();

            for (int i = 0; i < projeto.TarefaChannel.Count; i++)
            {
                var tar = projeto.TarefaChannel.ToList()[i];

                listaIteracoesSelecionadas.Add(new { IdIterationTfs = tar.IdIterationTfs, IdTarefaChannel = tar.IdTarefaChannel });
            }

            ViewBag.listaIteracoesSelecionadas = listaIteracoesSelecionadas;

            return View(projeto);
        }

        /// <summary>
        /// Popula dropdowns
        /// </summary>
        /// <param name="opcoes"></param>
        /// <returns></returns>
        private string PopularDropDown(List<string> opcoes)
        {
            var retorno = string.Empty;

            foreach (var item in opcoes)
            {
                retorno += "<option value='" + item + "'>" + item + "</option>";
            }

            return retorno;
        }

        /// <summary>
        /// Edita o projeto
        /// </summary>
        /// <param name="projeto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,TfsUrl,TfsCollection,TfsProject,TfsTeam")] Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(projeto);
        }

        /// <summary>
        /// Deleta o projeto e suas tarefas associadas a ele
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Projeto projeto = bus.GetProjeto(id.Value);

            if (projeto == null)
            {
                return HttpNotFound();
            }

            bool sucesso = bus.ExcluirProjeto(projeto);

            return Json(new { status = sucesso, redirectUrl = Url.Action("Index", "Projeto") }, JsonRequestBehavior.AllowGet);
        }
    }
}