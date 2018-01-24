using AgendaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AgendaWeb.Models.UsuarioModel;

namespace AgendaWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioRepositorio rep = new UsuarioRepositorio();

        // GET: Usuario
        [Authorize]
        public ActionResult Index()
        {
            //return View(rep.GetAll());
            return View(rep.GetByName(User.Identity.Name));
        }

        // GET: Usuario/Novo
        [AllowAnonymous]
        public ActionResult Novo()
        {
            return View();
        }

        // POST: Usuario/Novo
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Novo(UsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                rep.Save(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        
    }
}
