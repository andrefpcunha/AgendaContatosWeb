using AgendaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AgendaWeb.Controllers
{
    public class SegurancaController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(String returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = UsuarioModel.ValidarUsuario(model.Email, model.Senha);
            if (usuario != null)
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);
               
                return RedirectToAction("Index", "Contato");

            }
            else
            {
                ModelState.AddModelError("", "Login inválido.");
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Seguranca");
        }
    }
}