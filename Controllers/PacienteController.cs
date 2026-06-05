using Microsoft.AspNetCore.Mvc;
using CitasApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Citas_App.Controllers
{
    public class PacienteController : Controller
    {
        private readonly string _jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Pacientes.json");

        private List<Paciente> ObtenerPacientesDesdeJson()
        {
            if (!System.IO.File.Exists(_jsonPath)) return new List<Paciente>();
            string jsonString = System.IO.File.ReadAllText(_jsonPath);
            return JsonSerializer.Deserialize<List<Paciente>>(jsonString) ?? new List<Paciente>();
        }

        private void GuardarPacientesEnJson(List<Paciente> pacientes)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(pacientes, opciones);
            System.IO.File.WriteAllText(_jsonPath, jsonString);
        }

        public IActionResult Index()
        {
            var pacientes = ObtenerPacientesDesdeJson();
            return View(pacientes);
        }

        public IActionResult Detalle(int id)
        {
            var pacientes = ObtenerPacientesDesdeJson();
            var paciente = pacientes.FirstOrDefault(p => p.Id == id);
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

            var pacientes = ObtenerPacientesDesdeJson();
            paciente.Id = pacientes.Count > 0 ? pacientes.Max(p => p.Id) + 1 : 1;
            pacientes.Add(paciente);
            GuardarPacientesEnJson(pacientes);

            TempData["Mensaje"] = "Paciente registrado correctamente.";
            return RedirectToAction("Index");
        }
    }
}
