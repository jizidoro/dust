using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Xerife.Entities;
using Xerife.Business;
using Xerife.Web.Models;
using Xerife.Entities.Util;
using System.Linq.Expressions;
using AutoMapper;

namespace Xerife.Web.Controllers
{
    /// <summary>
    /// Controller dos hitoricos de vpn
    /// </summary>
    public class VpnHistoricoController : Controller
    {
        private VpnHistoricoBus bus = new VpnHistoricoBus();

        /// <summary>
        /// View Principal
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Consulta o histórico da vpn de acordo com o filtro
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public JsonResult ConsultarHistoricoVpn(HistoricoVpnFiltro filtro)
        {
            var vmHistoricoVpn = bus.ConsultarVpnHistorico(filtro).OrderByDescending(x => x.DataAcao).ToList()
                    .Select(p => Mapper.Map<VmHistoricoVpn>(p));
            return Json(vmHistoricoVpn, JsonRequestBehavior.AllowGet);
        }
        
    }
}
