using AgendaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaWeb.Controllers
{
    public class ContatoController : Controller
    {
        private ContatoRepositorio repo = new ContatoRepositorio();
        // GET: Contato
        [Authorize]
        public ActionResult Index()
        {
            var contato = repo.GetByName(User.Identity.Name.ToString());
            //var contato = repo.GetAll();

            if (contato == null)
            {
                return HttpNotFound();
            }

            return View(contato);
        }

        // GET: Contato/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contato/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repo.SaveByName(contato, User.Identity.Name.ToString());
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(contato);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Contato/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var contato = repo.GetById(id);

            if (contato == null)
            {
                return HttpNotFound();
            }

            return View(contato);
        }

        // POST: Contato/Edit/5
        [HttpPost]
        [Authorize]
        public ActionResult Edit(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repo.Update(contato);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(contato);
                }
            }
            catch
            {
                return View();
            }
        }

        // POST: Contato/Delete/5
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                repo.DeleteById(id);
                return Json(repo.GetAll());
            }
            catch
            {
                return View();
            }
        }
    }
}
