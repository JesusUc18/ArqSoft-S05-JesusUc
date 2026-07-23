using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CitasApp.Domain.Models;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Presentation.Controllers
{
    public class MedicoController : Controller
    {
        private readonly IMedicoRepository _medicoRepository;

        public MedicoController(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public IActionResult Index()
        {
            var medicos = _medicoRepository.ObtenerTodos();
            return View(medicos);
        }

        public IActionResult Detalle(int id)
        {
            var medico = _medicoRepository.ObtenerPorId(id);
            if (medico == null) return NotFound();
            return View(medico);
        }

        // GET: Medico/Crear
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Medico/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Medico medico)
        {
            if (!ModelState.IsValid) return View(medico);

            _medicoRepository.Agregar(medico);

            TempData["Mensaje"] = "Médico agregado correctamente.";
            return RedirectToAction("Index");
        }
    }
}