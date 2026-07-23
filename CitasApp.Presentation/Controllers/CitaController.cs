using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CitasApp.Domain.Models;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Presentation.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPacienteRepository _pacienteRepository;

        public CitaController(
            ICitaRepository citaRepository,
            IMedicoRepository medicoRepository,
            IPacienteRepository pacienteRepository)
        {
            _citaRepository = citaRepository;
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
        }

        public IActionResult Index()
        {
            var citas = _citaRepository.ObtenerTodas();
            ViewBag.Medicos = _medicoRepository.ObtenerTodos();
            ViewBag.Pacientes = _pacienteRepository.ObtenerTodos();
            return View(citas);
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            var citas = _citaRepository.ObtenerTodas()
                .Where(c => c.PacienteId == pacienteId)
                .ToList();
            return View(citas);
        }

        // GET: Cita/Crear
        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.Medicos = _medicoRepository.ObtenerTodos();
            ViewBag.Pacientes = _pacienteRepository.ObtenerTodos();
            return View();
        }

        // POST: Cita/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Cita cita)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Medicos = _medicoRepository.ObtenerTodos();
                ViewBag.Pacientes = _pacienteRepository.ObtenerTodos();
                return View(cita);
            }

            _citaRepository.Agregar(cita);

            TempData["Mensaje"] = "Cita agendada correctamente.";
            return RedirectToAction("Index");
        }
    }
}