using Microsoft.AspNetCore.Mvc;
using CitasApp.Domain.Models;
using CitasApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Citas_App.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteController(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public IActionResult Index()
        {
            var pacientes = _pacienteRepository.ObtenerTodos();
            return View(pacientes);
        }

        public IActionResult Detalle(int id)
        {
            var paciente = _pacienteRepository.ObtenerPorId(id);
            if (paciente == null) return NotFound();
            return View(paciente);
        }

        // GET: Paciente/Crear
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Paciente/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Paciente paciente)
        {
            if (!ModelState.IsValid) return View(paciente);

            _pacienteRepository.Agregar(paciente);

            TempData["Mensaje"] = "Paciente registrado correctamente.";
            return RedirectToAction("Index");
        }
    }
}