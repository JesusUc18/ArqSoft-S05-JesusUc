using Microsoft.AspNetCore.Mvc;
using CitasApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Citas_App.Controllers
{
    public class CitaController : Controller
    {
        private readonly string _jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Cita.json");
        private readonly string _medicosPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Medico.json");
        private readonly string _pacientesPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Pacientes.json");

        private List<Cita> ObtenerCitasDesdeJson()
        {
            if (!System.IO.File.Exists(_jsonPath)) return new List<Cita>();
            string jsonString = System.IO.File.ReadAllText(_jsonPath);
            return JsonSerializer.Deserialize<List<Cita>>(jsonString) ?? new List<Cita>();
        }

        private void GuardarCitasEnJson(List<Cita> citas)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(citas, opciones);
            System.IO.File.WriteAllText(_jsonPath, jsonString);
        }

        private List<Medico> ObtenerMedicos()
        {
            if (!System.IO.File.Exists(_medicosPath)) return new List<Medico>();
            string jsonString = System.IO.File.ReadAllText(_medicosPath);
            return JsonSerializer.Deserialize<List<Medico>>(jsonString) ?? new List<Medico>();
        }

        private List<Paciente> ObtenerPacientes()
        {
            if (!System.IO.File.Exists(_pacientesPath)) return new List<Paciente>();
            string jsonString = System.IO.File.ReadAllText(_pacientesPath);
            return JsonSerializer.Deserialize<List<Paciente>>(jsonString) ?? new List<Paciente>();
        }

        public IActionResult Index()
        {
            var citas = ObtenerCitasDesdeJson();
            return View(citas);
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            var citas = ObtenerCitasDesdeJson();
            var citasPaciente = citas.Where(c => c.PacienteId == pacienteId).ToList();
            return View(citasPaciente);
        }

        // GET: Cita/Crear
        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.Medicos = ObtenerMedicos();
            ViewBag.Pacientes = ObtenerPacientes();
            return View();
        }

        // POST: Cita/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Cita cita)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Medicos = ObtenerMedicos();
                ViewBag.Pacientes = ObtenerPacientes();
                return View(cita);
            }

            var citas = ObtenerCitasDesdeJson();
            cita.Id = citas.Count > 0 ? citas.Max(c => c.Id) + 1 : 1;
            citas.Add(cita);
            GuardarCitasEnJson(citas);

            TempData["Mensaje"] = "Cita agendada correctamente.";
            return RedirectToAction("Index");
        }
    }
}
