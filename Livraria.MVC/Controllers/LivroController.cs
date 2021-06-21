using Livraria.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Livraria.MVC.Controllers
{
    public class LivroController : BaseController
    {
        // GET: LivroController
        public ActionResult Index()
        {
            LivroViewModel model = new LivroViewModel();
            List<LivroViewModel> listaObj = RequestList(model, "v1/livros/ObterTodos");
            model.ListaLivros = listaObj;
            return View(model.ListaLivros);
        }

        // GET: LivroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LivroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LivroViewModel model)
        {
            try
            {
                LivroViewModel modelAux = RequestItem(model, "v1/livros/" + model.ISBN);

                if (modelAux == null)
                    RequestAction(model, "v1/livros", "POST");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LivroController/Edit/5
        public ActionResult Edit(int id)
        {
            LivroViewModel model = new LivroViewModel();
            model = RequestItem(model, "v1/livros/" + id);
            return View(model);
        }

        // POST: LivroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LivroViewModel model)
        {
            try
            {
                RequestAction(model, "v1/livros", "PUT");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LivroController/Delete/5
        public ActionResult Delete(int id)
        {
            LivroViewModel model = new LivroViewModel();
            model = RequestItem(model, "v1/livros/" + id);
            return View(model);
        }

        // POST: LivroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(LivroViewModel model)
        {
            try
            {
                RequestAction(model, "v1/livros/" + model.ISBN, "DELETE");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
