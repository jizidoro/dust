using Antlr.Runtime.Misc;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Xerife.Entities;
using Xerife.Business.Util;
using Xerife.Entities.Util;

namespace Xerife.Web.Controllers
{
    /// <summary>
    /// Controller do tfs
    /// </summary>
    public class TfsController : Controller
    {
        /// <summary>
        /// Modelo do database
        /// </summary>
        private Model1Container db = new Model1Container();

        /// <summary>
        /// Busca as collections do tfs
        /// </summary>
        /// <param name="tfs"></param>
        /// <returns></returns>
        public JsonResult GetCollections(string tfs)
        {
            return Json(TfsUtil.GetProjectCollection(tfs).value.OrderBy(x => x.name), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Busca os projetos tfs
        /// </summary>
        /// <param name="tfs"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public JsonResult GetProjects(string tfs, string collection)
        {
            return Json(TfsUtil.GetProjects(tfs, collection).value.OrderBy(x => x.name), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Busca as equipes
        /// </summary>
        /// <param name="tfs"></param>
        /// <param name="collection"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public JsonResult GetTeams(string tfs, string collection, string project)
        {
            return Json(TfsUtil.GetTeams(tfs, collection, project).value.OrderBy(x => x.name), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Busca o diretorio do projeto
        /// </summary>
        /// <param name="tfs"></param>
        /// <param name="collection"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public JsonResult GetAreaPathTree(string tfs, string collection, string project)
        {
            return Json(Flatten(TfsUtil.GetAreaPathTree(tfs, collection, project)).Where(x => !x.hasChildren).OrderBy(x => x.name), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Busca as iterações da equipe
        /// </summary>
        /// <param name="tfs"></param>
        /// <param name="collection"></param>
        /// <param name="project"></param>
        /// <param name="team"></param>
        /// <returns></returns>
        public JsonResult GetTeamIterations(string tfs, string collection, string project, string team)
        {
            return Json(TfsUtil.GetTeamIterations(tfs, collection, project, team).value.OrderBy(x => x.attributes.startDate), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Busca as iterações pelo projeto
        /// </summary>
        /// <param name="projetoId"></param>
        /// <returns></returns>
        public JsonResult GetTeamIterationsByProject(int projetoId)
        {
            Projeto projeto = db.ProjetoSet.Find(projetoId);
            if (projeto != null)
            {
                return Json(TfsUtil.GetTeamIterations(projeto.TfsUrl, projeto.TfsCollection, projeto.TfsProject, projeto.TfsTeam).value, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        /// <summary>
        /// Busca a WIQL pela iteração
        /// </summary>
        /// <param name="tfs"></param>
        /// <param name="collection"></param>
        /// <param name="project"></param>
        /// <param name="iteration"></param>
        /// <returns></returns>
        public JsonResult GetWIQLbyIteration(string tfs, string collection, string project, string iteration)
        {
            return Json(TfsUtil.GetWIQLbyIteration(tfs, collection, project, iteration).workItems, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Busca a capacidade da equipe
        /// </summary>
        /// <param name="tfs"></param>
        /// <param name="collection"></param>
        /// <param name="project"></param>
        /// <param name="team"></param>
        /// <param name="iteration"></param>
        /// <returns></returns>
        public JsonResult GetTeamCapacity(string tfs, string collection, string project, string team, string iteration)
        {
            return Json(TfsUtil.GetTeamCapacity(tfs, collection, project, team, iteration), JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetTfsRootFolder(string tfs, string collection, string project)
        //{
        //    return Json(TfsUtil.GetTfsRootFolder(tfs, collection, project), JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// Busca o diretorio do tfs
        /// </summary>
        /// <param name="projetoId"></param>
        /// <returns></returns>
        public JsonResult GetTfsRootFolder(int projetoId)
        {
            Projeto projeto = db.ProjetoSet.Find(projetoId);
            if (projeto != null)
            {
                return Json(TfsUtil.GetTfsFolderFromPath(projeto.TfsUrl, projeto.TfsCollection, projeto.TfsProject), JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        /// <summary>
        /// Busca o diretorio do projeto tfs
        /// </summary>
        /// <param name="projetoId"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public JsonResult GetTfsProjectFolder(int projetoId, string folderPath)
        {
            Projeto projeto = db.ProjetoSet.Find(projetoId);
            if (projeto != null)
            {
                return Json(TfsUtil.GetTfsFolderFromPath(projeto.TfsUrl, projeto.TfsCollection, (folderPath).Replace("$/", "")), JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public JsonResult GetBranchFromPath(int projetoId, string folderPath)
        {
            Projeto projeto = db.ProjetoSet.Find(projetoId);
            if (projeto != null)
            {
                return Json(TfsUtil.GetBranchByPath(projeto.TfsUrl, projeto.TfsCollection, folderPath), JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        /// <summary>
        /// Busca a branch
        /// </summary>
        /// <param name="projetoId"></param>
        /// <returns></returns>
        public JsonResult GetBranchByRoot(int projetoId)
        {
            Projeto projeto = db.ProjetoSet.Find(projetoId);
            if (projeto != null)
            {
                return Json(TfsUtil.GetBranchByRoot(projeto.TfsUrl, projeto.TfsCollection), JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        /// <summary>
        /// Lista de ViewModel do diretorio
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static List<AreaPathTreeViewModel> Flatten(AreaPathTreeViewModel root)
        {
            var flattened = new List<AreaPathTreeViewModel> { root };
            var children = root.children;

            if (children != null)
            {
                foreach (var child in children)
                {
                    flattened.AddRange(Flatten(child));
                }
            }

            return flattened;
        }

        public JsonResult GetIssuesFromProject(string tfs, string collection, string project, string iteration)
        {
            return Json(TfsUtil.GetIssuesFromProject(tfs, collection, project, iteration).workItems, JsonRequestBehavior.AllowGet);
        }
    }
}