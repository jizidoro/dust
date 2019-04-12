using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Xerife.Business;
using Xerife.Entities;
using Xerife.Web.Models;
using Xerife.Business.Util;
using Xerife.Web.Util;
using Xerife.Entities.Util;
using AutoMapper;

namespace Xerife.Web.Controllers
{
    /// <summary>
    /// Ações de usuario vpn
    /// </summary>
    [CustomAuthorizeAttribute(Roles = "Administrador, Infra, Gerente")]
    public class UsuarioVpnsController : Controller
    {
        private UsuarioVpnBus usuarioVpnBus = new UsuarioVpnBus();

        /// <summary>
        /// View principal
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Busca a lista de usuarios vpn
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUsuarioVpns()
        {
            var usuariosVpn = ActiveDirectoryUtil.GetUsuarioVpns();
            var usuariosComplementoAcesso = usuarioVpnBus.ConsultarListaUsuarioVpn();

            foreach (var u in usuariosVpn)
            {
                var user = usuariosComplementoAcesso.Where(x => x.Login.Equals(u.Login)).FirstOrDefault();
                if (user != null)
                {
                    usuariosVpn.Where(x => x.Login.Equals(u.Login)).FirstOrDefault().Id = user.Id;
                    usuariosVpn.Where(x => x.Login.Equals(u.Login)).FirstOrDefault().Inicio = user.Inicio.Date.ToString("dd/MM/yyyy");
                    usuariosVpn.Where(x => x.Login.Equals(u.Login)).FirstOrDefault().Fim = user.Fim.Date.ToString("dd/MM/yyyy");
                }
            }

            return Json(new { dado = usuariosVpn.OrderBy(x => x.Nome), acesso = (this.User.IsInRole("Administrador") || this.User.IsInRole("Infra")) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Busca lista de 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetListaUsuariosAD()
        {
            return Json(usuarioVpnBus.ConsultarComboUsuarioAD().OrderBy(x => x.Nome), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Concede acesso ao usuario
        /// </summary>
        /// <param name="usuarioViewModel"></param>
        /// <returns></returns>
        public ActionResult ConcederAcesso(UsuarioVpnViewModel usuarioViewModel, string justificativa)
        {
            if (usuarioViewModel != null)
            {
                var usuarioDb = new UsuarioVpn()
                {
                    Login = usuarioViewModel.Login,
                    Inicio = DateTime.Now,
                    Fim = Convert.ToDateTime(usuarioViewModel.Fim)
                };
                var mensagem = usuarioVpnBus.IncluirUsuarioVpn(usuarioDb, justificativa, User.Identity.Name.Split('\\')[1]);

                return Json(new { redirectUrl = Url.Action("Index", "UsuarioVpns"), mensagem = mensagem });
            }

            return Json(new { redirectUrl = Url.Action("Index", "UsuarioVpns") });
        }


        /// <summary>
        /// Revoga o acesso do usuario e o exclui
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ConcederAcessoVitalicio(string login)
        {
            usuarioVpnBus.ConcederAcessoVitalicio(login, User.Identity.Name.Split('\\')[1]);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Revoga o acesso do usuario e o exclui
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RevogarAcesso(string login)
        {
            usuarioVpnBus.ExcluirUsuarioVpn(login, User.Identity.Name.Split('\\')[1]);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Busca os detalhes do usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetUsuarioVpnDetail(int id)
        {
            var usuarioVpn = Mapper.Map<VmUsuarioVpn>(usuarioVpnBus.GetUsuario(id));
            var mensagem = usuarioVpn != null ? "" : "Usuário já possui acesso vitalício à VPN.";
            return Json(new { data = Mapper.Map<VmUsuarioVpn>(usuarioVpnBus.GetUsuario(id)), mensagem = mensagem }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// View de criação de usuario
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Concede acesso ao usuario
        /// </summary>
        /// <param name="usuarioViewModel"></param>
        /// <returns></returns>
        public ActionResult ProlongarAcesso(UsuarioVpnViewModel usuarioViewModel, string justificativa)
        {
            if (usuarioViewModel != null)
            {
                var usuarioDb = new UsuarioVpn()
                {
                    Id = usuarioViewModel.Id,
                    Login = usuarioViewModel.Login,
                    Inicio = DateTime.Now,
                    Fim = Convert.ToDateTime(usuarioViewModel.Fim)
                };
                var mensagem = usuarioVpnBus.ProlongarAcesso(usuarioDb, justificativa, User.Identity.Name.Split('\\')[1]);

                return Json(new { redirectUrl = Url.Action("Index", "UsuarioVpns"), mensagem = mensagem });
            }

            return Json(new { redirectUrl = Url.Action("Index", "UsuarioVpns") });
        }

    }
}
