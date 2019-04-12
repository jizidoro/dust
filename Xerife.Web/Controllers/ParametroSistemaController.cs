using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xerife.Business;
using Xerife.Entities;
using Xerife.Web.Models;

namespace Xerife.Web.Controllers
{
    public class ParametroSistemaController : Controller
    {
        private ParametroSistemaBus bus = new ParametroSistemaBus();
        // GET: ParametroSistema
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ConsultarParametroSistema()
        {
            return Json(bus.ConsultarParametroSistemaPorPerfil(User.Identity.Name.Split('\\')[1]), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AlterarParametroSistema(ParametroSistema parametro)
        {
            var sucesso = bus.AlterarParametroSistema(parametro);
            return Json(new { status = sucesso, redirectUrl = Url.Action("Index", "ParametroSistema") });
        }

        public JsonResult GetById(int id)
        {
            return Json(Mapper.Map<VmParametroSistema>(bus.GetById(id)), JsonRequestBehavior.AllowGet);
        }


    }
}