using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using sIo = System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xerife.Business.Util;
using Xerife.Entities;
using Xerife.Web.Models;
using NPOI.XSSF.UserModel;

namespace Xerife.Web.Controllers
{
    /// <summary>
    /// Constroller de baseline
    /// </summary>
    public class BaselineController : Controller
    {
        /// <summary>
        /// Modelo de Database
        /// </summary>
        private Model1Container db = new Model1Container();

        /// <summary>
        /// View principal
        /// </summary>
        /// <returns></returns>
        public ActionResult GerarBaseline()
        {
            return View();
        }

        /// <summary>
        /// Gerar planejamento de baseline
        /// </summary>
        /// <returns></returns>
        public ActionResult GerarBaselinePlanejamento()
        {
            return View();
        }

        /// <summary>
        /// Gerar Baseline de projeto
        /// </summary>
        /// <param name="baselineGeracao"></param>
        /// <returns></returns>
        public JsonResult GerarBaselineProjeto(BaselineGeracaoViewModel baselineGeracao)
        {
            Projeto projeto = db.ProjetoSet.Find(baselineGeracao.ProjetoId);
            if (projeto != null)
            {
                var workItemResolved = TfsUtil.IterationWorkItemsUnclosed(projeto.TfsUrl, projeto.TfsCollection, projeto.TfsProject, baselineGeracao.Iteracao);
                baselineGeracao.WorkItemUnclosedAssert = !workItemResolved.Any();
                baselineGeracao.WorkItemUnclosedComentario = workItemResolved.Count.ToString() + " WorkItems não fechados.";

                var changesets = TfsUtil.ProjetoIterationChangesets(projeto.TfsUrl, projeto.TfsCollection, projeto.TfsProject, projeto.TfsTeam, baselineGeracao.Iteracao, baselineGeracao.FolderPathTfs);

                baselineGeracao.ChangesetArtefatoAssert = changesets.Any();
                baselineGeracao.ChangesetArtefatoComentario = changesets.Any() ? string.Empty : "Nenhum check-in encontrado.";

                var changesetsWorkitems = TfsUtil.GetChangesetWorkItems(projeto.TfsUrl, projeto.TfsCollection, changesets);
                double percentualChangeSetWorkItem = changesets.Count > 0 ? ((double)changesetsWorkitems.Count / (double)changesets.Count) * 100 : 0;
                baselineGeracao.ArtefatoWorkItemAssert = percentualChangeSetWorkItem >= 80 ? true : false; //ADERENCIA_CHANGESET_WORKITEM
                baselineGeracao.ArtefatoWorkItemComentario = percentualChangeSetWorkItem.ToString() + "% de aderência.";

                baselineGeracao.CommitBranchAssert = baselineGeracao.BranchPathtfs.Equals("N/A") ? true : baselineGeracao.ArtefatoWorkItemAssert;
                baselineGeracao.CommitBranchComentario = baselineGeracao.BranchPathtfs.Equals("N/A") ? "Projeto não utiliza branch." : string.Empty;
            }
            var arq = ManageExcel(baselineGeracao);
            return Json(new { FileGuid = arq, FileName = "TestReportOutput.xlsx" }, JsonRequestBehavior.AllowGet);

        }
        public virtual ActionResult Download(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                return File(data, "application/vnd.ms-excel", fileName);
            }
            else
            {
                return new EmptyResult();
            }
        }

        /// <summary>
        /// Cria arquivo excel baseado na view model
        /// </summary>
        /// <param name="bgVm"></param>
        private string ManageExcel(BaselineGeracaoViewModel bgVm)
        {
            string
                rootPath = HttpContext.Request.MapPath("~"),
                templateFile = rootPath + "DocumentsTemplate\\auditoriabaselineentrega.xlsx";
            
            var wb = new XSSFWorkbook(templateFile);

            ISheet sheet = wb.GetSheetAt(0);
            IRow row = sheet.GetRow(5);
            ICell cell = row.GetCell(3);
            cell.SetCellValue(bgVm.AnalistaConfiguracao);

            row = sheet.GetRow(6);
            cell = row.GetCell(3);
            cell.SetCellValue(DateTime.Now.Date.ToString("dd/MM/yyyy"));

            row = sheet.GetRow(7);
            cell = row.GetCell(3);
            cell.SetCellValue(bgVm.Identificacao);

            row = sheet.GetRow(15);
            cell = row.GetCell(7);
            cell.SetCellValue(bgVm.WorkItemUnclosedAssert ? "OK" : "NC");
            cell = row.GetCell(9);
            cell.SetCellValue(bgVm.WorkItemUnclosedComentario);

            row = sheet.GetRow(16);
            cell = row.GetCell(7);
            cell.SetCellValue(bgVm.ChangesetArtefatoAssert ? "OK" : "NC");
            cell = row.GetCell(9);
            cell.SetCellValue(bgVm.ChangesetArtefatoComentario);

            row = sheet.GetRow(17);
            cell = row.GetCell(7);
            cell.SetCellValue(bgVm.ArtefatoWorkItemAssert ? "OK" : "NC");
            cell = row.GetCell(9);
            cell.SetCellValue(bgVm.ArtefatoWorkItemComentario);

            string handle = Guid.NewGuid().ToString();

            using (var ms = new sIo.MemoryStream())
            {
                wb.Write(ms);
                TempData[handle] = ms.ToArray();
            }

            return handle;
        }
    }
}