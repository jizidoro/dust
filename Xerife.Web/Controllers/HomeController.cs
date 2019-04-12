using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xerife.Web.Util;

namespace Xerife.Web.Controllers
{
    /// <summary>
    /// Controller da pagina inicial
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// View principal
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Message = TempData["ErroMessage"];
            return View();
        }

        /// <summary>
        /// Sobre
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Contato
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}