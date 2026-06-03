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
        // Apunta exactamente a Cita.json en tu carpeta Data
        private readonly string _jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Cita.json");

        private List<Cita> ObtenerCitasDesdeJson()
        {
            if (!System.IO.File.Exists(_jsonPath)) return new List<Cita>();

            string jsonString = System.IO.File.ReadAllText(_jsonPath);
            return JsonSerializer.Deserialize<List<Cita>>(jsonString) ?? new List<Cita>();
        }

        // Muestra la agenda completa de citas
        public IActionResult Index()
        {
            var citas = ObtenerCitasDesdeJson();
            return View(citas);
        }

        // Filtra las citas de un paciente específico
        public IActionResult PorPaciente(int pacienteId)
        {
            var citas = ObtenerCitasDesdeJson();
            var citasPaciente = citas.Where(c => c.PacienteId == pacienteId).ToList();

            return View(citasPaciente);
        }
    }
}