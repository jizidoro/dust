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
    public class UsuarioController : Controller
    {
        private UsuarioBus usuarioBus = new UsuarioBus();
        private PerfilBus perfilBus = new PerfilBus();

        public ActionResult Index()
        {
            return View();
        }
        // GET: Usuario
        public JsonResult GetUsuarios()
        {
            return Json(usuarioBus.ConsultarUsuarios().OrderBy(y => y.Login).ToList()
                    .Select(p => Mapper.Map<VmUsuario>(p)), JsonRequestBehavior.AllowGet);
        }
        // GET: Usuario
        public JsonResult ConsultarUsuarios()
        {
            return Json(usuarioBus.ConsultarUsuarios().OrderBy(x => x.Perfil.Nome).ThenBy(y => y.Login).ToList()
                    .Select(p => Mapper.Map<VmUsuario>(p)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsuario(int id)
        {
            return Json(Mapper.Map<VmUsuario>(usuarioBus.GetUsuario(id)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IncluirUsuario(Usuario usuario)
        {
            var sucesso = usuarioBus.IncluirUsuario(usuario);
            return Json(new { status = sucesso, redirectUrl = Url.Action("Index", "Usuario") });
        }

        public JsonResult AlterarPerfilUsuario(int idUsuario, int idPerfil)
        {
            var sucesso = usuarioBus.AlterarPerfilUsuario(idUsuario, idPerfil);
            return Json(new { status = sucesso, redirectUrl = Url.Action("Index", "Usuario") });
        }

        public JsonResult ExcluirUsuario(int id)
        {
            var sucesso = usuarioBus.ExcluirUsuario(id);
            return Json(new { status = sucesso, redirectUrl = Url.Action("Index", "Usuario") });
        }
    }
}