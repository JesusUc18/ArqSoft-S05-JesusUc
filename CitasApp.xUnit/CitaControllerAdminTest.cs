using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Presentation.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CitasApp.Tests.Controllers
{
    // ---------------------------------------------------------------------
    // Adapters "fake" en memoria — Implementación exacta de las interfaces
    // ICitaRepository, IPacienteRepository e IMedicoRepository.
    // ---------------------------------------------------------------------
    public class CitaRepositoryFake : ICitaRepository
    {
        private readonly List<Cita> _citas;

        public CitaRepositoryFake(List<Cita> citas) => _citas = citas;

        public IEnumerable<Cita> ObtenerTodas() => _citas;

        public Cita ObtenerPorId(int id) => _citas.FirstOrDefault(c => c.Id == id)!;

        public void Agregar(Cita cita) => _citas.Add(cita);

        public void Actualizar(Cita cita)
        {
            var index = _citas.FindIndex(c => c.Id == cita.Id);
            if (index != -1) _citas[index] = cita;
        }

        public void Eliminar(int id) => _citas.RemoveAll(c => c.Id == id);
    }

    public class PacienteRepositoryFake : IPacienteRepository
    {
        private readonly List<Paciente> _pacientes;

        public PacienteRepositoryFake(List<Paciente> pacientes) => _pacientes = pacientes;

        public IEnumerable<Paciente> ObtenerTodos() => _pacientes;

        public Paciente ObtenerPorId(int id) => _pacientes.FirstOrDefault(p => p.Id == id)!;

        public void Agregar(Paciente paciente) => _pacientes.Add(paciente);

        public void Actualizar(Paciente paciente)
        {
            var index = _pacientes.FindIndex(p => p.Id == paciente.Id);
            if (index != -1) _pacientes[index] = paciente;
        }

        public void Eliminar(int id) => _pacientes.RemoveAll(p => p.Id == id);
    }

    public class MedicoRepositoryFake : IMedicoRepository
    {
        private readonly List<Medico> _medicos;

        public MedicoRepositoryFake(List<Medico> medicos) => _medicos = medicos;

        public IEnumerable<Medico> ObtenerTodos() => _medicos;

        public Medico ObtenerPorId(int id) => _medicos.FirstOrDefault(m => m.Id == id)!;

        public void Agregar(Medico medico) => _medicos.Add(medico);

        public void Actualizar(Medico medico)
        {
            var index = _medicos.FindIndex(m => m.Id == medico.Id);
            if (index != -1) _medicos[index] = medico;
        }

        public void Eliminar(int id) => _medicos.RemoveAll(m => m.Id == id);
    }

    // ---------------------------------------------------------------------
    // Pruebas — Solo el camino del administrador
    // ---------------------------------------------------------------------
    public class CitaControllerAdminTests
    {
        private CitaController CrearControllerConDatosDePrueba(out List<Cita> citasEsperadas)
        {
            // Arrange — datos de prueba en memoria
            citasEsperadas = new List<Cita>
            {
                new Cita { Id = 1, PacienteId = 10, Estado = "Pendiente" },
                new Cita { Id = 2, PacienteId = 20, Estado = "Confirmada" },
                new Cita { Id = 3, PacienteId = 10, Estado = "Pendiente" }
            };

            var pacientes = new List<Paciente>
            {
                new Paciente { Id = 10, Email = "paciente1@correo.com" },
                new Paciente { Id = 20, Email = "paciente2@correo.com" }
            };

            var medicos = new List<Medico>
            {
                new Medico { Id = 1, Name = "Dr. Pérez" }
            };

            // Inyectamos los repositorios fake directamente al controlador
            var citaRepoFake = new CitaRepositoryFake(citasEsperadas);
            var pacienteRepoFake = new PacienteRepositoryFake(pacientes);
            var medicoRepoFake = new MedicoRepositoryFake(medicos);

            var controller = new CitaController(citaRepoFake, medicoRepoFake, pacienteRepoFake);

            // Simular usuario admin logueado
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, "jorge@admin.com") };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            return controller;
        }

        [Fact]
        public void Index_ConCuentaAdmin_RegresaTodasLasCitasSinFiltrar()
        {
            // Arrange
            var controller = CrearControllerConDatosDePrueba(out var citasEsperadas);

            // Act
            var resultado = controller.Index() as ViewResult;
            var modelo = resultado?.Model as IEnumerable<Cita>;

            // Assert
            Assert.NotNull(modelo);
            Assert.Equal(citasEsperadas.Count, modelo.Count());
            Assert.Equal(citasEsperadas, modelo);
        }

        [Fact]
        public void Index_ConCuentaAdmin_IncluyeCitasDeMasDeUnPaciente()
        {
            // Arrange
            var controller = CrearControllerConDatosDePrueba(out _);

            // Act
            var resultado = controller.Index() as ViewResult;
            var modelo = resultado?.Model as IEnumerable<Cita>;

            // Assert
            Assert.NotNull(modelo);
            var pacientesDistintos = modelo.Select(c => c.PacienteId).Distinct().Count();
            Assert.True(pacientesDistintos > 1);
        }

        [Fact]
        public void Index_ConCuentaAdmin_CargaCatalogosDePacientesYMedicosEnViewBag()
        {
            // Arrange
            var controller = CrearControllerConDatosDePrueba(out _);

            // Act
            controller.Index();

            // Assert
            Assert.NotNull(controller.ViewBag.Pacientes);
            Assert.NotNull(controller.ViewBag.Medicos);
        }
    }
}