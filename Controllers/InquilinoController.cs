using InmobiliariaAlvarez.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaAlvarez.Controllers
{
    public class InquilinoController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly RepositorioInquilino repositorio;

        public InquilinoController(IConfiguration configuration)
        {
            this.repositorio = new RepositorioInquilino(configuration);
            this.configuration = configuration;
        }

        // GET: InquilinoController
        public ActionResult Index()
        {
           
                IList <Inquilino>  lista = repositorio.ObtenerTodos();
                return View(lista);
          
        }

        // GET: InquilinoController/Details/5
        public ActionResult Details(int IdInquilino)
        {
            var e = repositorio.Obtener(IdInquilino);
            return View(e);
        }

        // GET: InquilinoController/Create

        public ActionResult Crear()
        { 
           return View();               
        }


        // POST: InquilinoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Inquilino e)
        {
            try
            {
                int res = repositorio.Alta(e);
                    TempData["Mensaje"] = $"Inquilino ingresado con exito. Id: {res}";
                    return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                TempData["Mensaje"] = ex.StackTrace;
                return View();
            }
        }

        // GET: InquilinoController/Edit/5
        public ActionResult Edit(int IdInquilino)
        {
            var e = repositorio.Obtener(IdInquilino);
            return View(e);
        }

        // POST: InquilinoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int IdInquilino, Inquilino e)
        {
            try
            {
                repositorio.Modificacion(e);
                TempData["Mensaje"] = "Inquilino modificado con exito";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InquilinoController/Delete/5
        public ActionResult Delete(int IdInquilino)
        {
            var e = repositorio.Obtener(IdInquilino);
            return View(e);
        }

        // POST: InquilinoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int IdInquilino, Inquilino e)
        {
            try
            {
                repositorio.Baja(IdInquilino);
                TempData["Mensaje"] = "Inquilino eliminado con exito";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
