using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xerife.Business;
using Xerife.Web.Models;

namespace Xerife.Web.Controllers
{
    public class PerfilController : Controller
    {
        private PerfilBus perfilBus = new PerfilBus();
        public JsonResult GetPerfis()
        {
            return Json(perfilBus.GetPerfis().OrderBy(x => x.Nome), JsonRequestBehavior.AllowGet);
        }
    }
}