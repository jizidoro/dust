using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xerife.Entities;
using Xerife.Web.Models;
using Xerife.Business.Util;
using Xerife.Entities.Util;

namespace Xerife.Web.Controllers
{
    /// <summary>
    /// Controller de consultas
    /// </summary>
    public class ConsultasController : Controller
    {
        /// <summary>
        /// Modelo de database
        /// </summary>
        private Model1Container db = new Model1Container();
        
        /// <summary>
        /// Detalhes da capacidade do colaborador
        /// </summary>
        /// <returns></returns>
        public ActionResult CapacidadeColaborador()
        {
            return View("CapacidadeColaborador");
        }

        /// <summary>
        /// Detalhes das próximas entregas
        /// </summary>
        /// <returns></returns>
        public ActionResult ProximasEntregas()
        {
            return View("ProximasEntregas");
        }

        /// <summary>
        /// Busca a capacidade do colaborador
        /// </summary>
        /// <returns></returns>
        public JsonResult GetColaboradorCapacity()
        {
            List<ColaboradorViewModel> colaboradores = new List<ColaboradorViewModel>();

            var projetos = db.ProjetoSet.ToList();
            List<TfsTeamCapacityListViewModel> capacities = new List<TfsTeamCapacityListViewModel>();
            foreach (var p in projetos)
            {
                var capacity = TfsUtil.GetCapacityFromCurrentIteration(p);
                capacities.Add(capacity);
                if (capacity.value != null)
                {
                    foreach (var c in capacity.value)
                    {
                        //var totalDayOff = c.daysOff.Select(x => x.end - x.start).FirstOrDefault();
                        //var totalDayOffDate = Convert.ToDateTime(totalDayOff);

                        var tempColab = new ColaboradorViewModel()
                        {
                            Nome = c.teamMember.displayName,
                            UrlFoto = c.teamMember.imageUrl,
                            Capacidade = new List<CapacidadeViewModel>()
                        };
                        var tempCapacidade = new List<CapacidadeViewModel>();
                        foreach (var d in c.activities)
                        {
                            tempCapacidade.Add(new CapacidadeViewModel()
                            {
                                Disciplina = d.name,
                                Horas = d.capacityPerDay,
                                Projeto = p.TfsTeam
                            });
                        }
                        if (colaboradores.Any(x => x.Nome.Equals(tempColab.Nome)))
                        {
                            colaboradores.Where(x => x.Nome.Equals(tempColab.Nome)).FirstOrDefault().Capacidade.AddRange(tempCapacidade);
                        }
                        else
                        {
                            tempColab.Capacidade.AddRange(tempCapacidade);
                            colaboradores.Add(tempColab);
                        }
                    }
                }
            }

            return Json(colaboradores.OrderBy(x => x.Nome), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Busca as próximas entregas
        /// </summary>
        /// <returns></returns>
        public JsonResult GetProximasEntregas()
        {
            List<TfsTeamIterationViewModel> currentIterations = new List<TfsTeamIterationViewModel>();
            foreach (var p in db.ProjetoSet.ToList())
            {
                var it = TfsUtil.GetProjectIterations(p);
                it = it.Where(x => x.name.Contains("Iteração")).ToList();
                currentIterations.AddRange(it);
            }

            var eventList = from item in currentIterations
                            select new
                            {
                                id = item.id,
                                title = item.path.Split('\\').Skip(1).Aggregate((i, j) => i + "\\" + j),
                                //start = Convert.ToDateTime(item.attributes.startDate).ToString("s"),
                                //end = Convert.ToDateTime(item.attributes.finishDate).ToString("s"),
                                start = Convert.ToDateTime(item.attributes.finishDate).ToString("s"),
                                allDay = true,
                                //url = item.url
                            };

            return Json(eventList.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}