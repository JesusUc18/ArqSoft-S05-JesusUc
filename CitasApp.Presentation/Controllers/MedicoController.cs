using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CitasApp.Domain.Models;
using CitasApp.Domain.Interfaces;

namespace Citas_App.Controllers
{
    public class MedicoController : Controller
    {
        private readonly string _jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Medico.json");

        private List<Medico> ObtenerMedicosDesdeJson()
        {
            if (!System.IO.File.Exists(_jsonPath)) return new List<Medico>();
            string jsonString = System.IO.File.ReadAllText(_jsonPath);
            return JsonSerializer.Deserialize<List<Medico>>(jsonString) ?? new List<Medico>();
        }

        private void GuardarMedicosEnJson(List<Medico> medicos)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(medicos, opciones);
            System.IO.File.WriteAllText(_jsonPath, jsonString);
        }

        public IActionResult Index()
        {
            var medicos = ObtenerMedicosDesdeJson();
            return View(medicos);
        }

        public IActionResult Detalle(int id)
        {
            var medicos = ObtenerMedicosDesdeJson();
            var medico = medicos.FirstOrDefault(m => m.Id == id);
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

            var medicos = ObtenerMedicosDesdeJson();
            medico.Id = medicos.Count > 0 ? medicos.Max(m => m.Id) + 1 : 1;
            medicos.Add(medico);
            GuardarMedicosEnJson(medicos);

            TempData["Mensaje"] = "Médico agregado correctamente.";
            return RedirectToAction("Index");
        }
    }
}
