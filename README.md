# Arquitectura de Software - Actividad #18 - Práctica .NET: Arquitectura Hexagonal en C#

## 👨‍💻 Información del Estudiante

* **Nombre:** Jesús Omar Uc Domínguez
* **Matrícula:** SW2509031
* **Grupo:** 3B
* **Cuatrimestre:** 3er Cuatrimestre
* **Carrera:** TSU en Desarrollo e Innovación de Software
* **Profesor:** Jorge Javier Pedrozo Romero

---

# 🏥 Sistema de Gestión de Citas Médicas

Este proyecto es una **aplicación web desarrollada en .NET** que permite administrar información relacionada con pacientes, médicos y citas médicas.

El sistema facilita el registro y visualización de pacientes, médicos y citas, organizando la información mediante los principios de la **Arquitectura Hexagonal (Clean Architecture)**. Esto permite desacoplar la lógica de negocio central de los detalles de infraestructura y de la interfaz de usuario, manteniendo una estructura altamente ordenada, escalable y fácil de mantener.

---

## 📌 Características

* Registro y gestión de pacientes.
* Registro y gestión de médicos.
* Administración de citas médicas.
* **Organización mediante Arquitectura Hexagonal dividida en 4 proyectos desacoplados.**
* Interfaz web desarrollada con Razor Views en la capa de presentación.
* Inversión de dependencias para asegurar que las reglas de negocio no dependan de la base de datos o frameworks.
* Código estructurado y modular para futuras ampliaciones o cambios de infraestructura.
* **3 adapters de persistencia intercambiables:** JSON, CSV y SQLite — se activan desde `Program.cs` sin tocar el Dominio ni la Aplicación.

---

## 🩺 Cómo funciona el sistema

1. **Inicio de la aplicación:** El usuario accede al sistema desde el navegador web (Capa de Presentación).
2. **Gestión de pacientes:** Se pueden visualizar y administrar los datos de los pacientes a través de casos de uso (Capa de Aplicación).
3. **Gestión de médicos:** Se registran y consultan médicos con su especialidad y número de licencia.
4. **Gestión de citas:** Se programan citas asociando pacientes y médicos mediante reglas de negocio del dominio.
5. **Capa de Dominio (Domain):** Contiene las entidades principales y las interfaces de los repositorios sin dependencias externas.
6. **Capa de Aplicación (Application):** Orquesta los flujos de trabajo e implementa los casos de uso del sistema.
7. **Capa de Infraestructura (Infrastructure):** Contiene tres adapters de persistencia intercambiables para cada entidad — JSON, CSV y SQLite — seleccionables desde `Program.cs` sin modificar el Dominio.
8. **Capa de Presentación (Presentation):** Procesa las solicitudes mediante controladores MVC y retorna las vistas interactivas (Razor).

---

## 🧩 Diagrama de arquitectura (UML)

El diagrama de arquitectura que refleja el estado real del proyecto está en
[`docs/uml-arquitectura.md`](docs/uml-arquitectura.md). 

---

## 📸 Capturas de Pantalla

| Inicio/Privacidad | Citas |
|-----------|-----------|
| <img width="1919" height="951" alt="image" src="https://github.com/user-attachments/assets/2d932093-07df-4712-8de0-eae3ce99b283" /> | <img width="1919" height="944" alt="image" src="https://github.com/user-attachments/assets/6c472b5d-038e-4baa-8b8a-86de501554f1" /> |
| <img width="1919" height="946" alt="image" src="https://github.com/user-attachments/assets/5986622c-d2ca-42eb-af6e-83761aef9964" /> | <img width="1919" height="947" alt="image" src="https://github.com/user-attachments/assets/0c8487b4-e840-478a-9afc-9b8dade50f30" /> |
| <img width="1919" height="946" alt="image" src="https://github.com/user-attachments/assets/978c8164-090a-404a-884c-9296a88123dd" /> | <img width="1919" height="949" alt="image" src="https://github.com/user-attachments/assets/4738871e-049a-4318-8994-2e4818b0de51" /> |



| Médicos | Pacientes |
|-----------|-----------|
| <img width="1919" height="944" alt="image" src="https://github.com/user-attachments/assets/21d2cbde-6175-4111-b84e-56eebe6c0286" /> | <img width="1919" height="951" alt="image" src="https://github.com/user-attachments/assets/df6ee7dc-b5c0-4c85-a4f5-051fffb11db8" />|
| <img width="1919" height="951" alt="image" src="https://github.com/user-attachments/assets/6acc84f6-b2ce-40d3-9688-3f3ee4ead958" /> | <img width="1919" height="946" alt="image" src="https://github.com/user-attachments/assets/187ec1a3-c324-4748-b1dc-88f6ac329995" /> |
| <img width="1919" height="945" alt="image" src="https://github.com/user-attachments/assets/4aaaf302-7d1e-4633-883c-5df14deca760" /> | <img width="1919" height="949" alt="image" src="https://github.com/user-attachments/assets/1938e1e4-b603-4a28-8690-84430607ae37" /> |




---

## 📁 Estructura del proyecto

```text

CitasApp/
├── CitasApp.Domain/
│   ├── Models/
│   │   ├── Paciente.cs
│   │   ├── Medico.cs
│   │   ├── Cita.cs
│   │   └── ErrorViewModel.cs
│   └── Interfaces/
│       ├── IPacienteRepository.cs
│       ├── IMedicoRepository.cs
│       └── ICitaRepository.cs
│
├── CitasApp.Application/
│   └── service/
│
├── CitasApp.Infrastructure/
│   └── Repositories/
│       ├── JsonPacienteRepository.cs   ← Adapter A
│       ├── JsonMedicoRepository.cs
│       ├── JsonCitaRepository.cs
│       ├── CsvPacienteRepository.cs    ← Adapter B (activo)
│       ├── CsvMedicoRepository.cs
│       ├── CsvCitaRepository.cs
│       ├── SqlitePacienteRepository.cs ← Adapter C
│       ├── SqliteMedicoRepository.cs
│       └── SqliteCitaRepository.cs
│
├── CitasApp.Presentation/
│   ├── Areas/
│   │   └── Identity/
│   ├── Controllers/
│   │   ├── HomeController.cs
│   │   ├── PacienteController.cs
│   │   ├── MedicoController.cs
│   │   └── CitaController.cs
│   ├── Data/
│   │   ├── ApplicationDbContext.cs
│   │   └── Migrations/
│   ├── Properties/
│   ├── Views/
│   │   ├── Cita/
│   │   ├── Home/
│   │   ├── Medico/
│   │   ├── Paciente/
│   │   └── Shared/
│   ├── wwwroot/
│   ├── Program.cs
│   ├── appsettings.json
│   └── appsettings.Development.json
│
├── CitasApp.slnx
├── README.md
├── .gitignore
└── .gitattributes

```

---

## 🔌 Adapters de persistencia intercambiables

Uno de los beneficios clave de la Arquitectura Hexagonal es que el **Dominio nunca sabe cómo se guardan los datos**. Los tres adapters implementan los mismos Ports (`IPacienteRepository`, `IMedicoRepository`, `ICitaRepository`) y se enchufan desde `Program.cs` sin modificar ninguna otra capa.

| Bloque | Adapter | Descripción |
|--------|---------|-------------|
| **A** | JSON | Lectura/escritura en archivos `.json` dentro de `/Data`. Era el comportamiento original. |
| **B** ✅ | CSV | Lectura/escritura en archivos `.csv` dentro de `/Data`. **Activo actualmente.** |
| **C** | SQLite | Base de datos local (`citasapp.db`) usando `Microsoft.Data.Sqlite`. Sin servidor requerido. |

Para cambiar de adapter basta con comentar el bloque activo y descomentar otro en `Program.cs`:

```csharp
// ▶ Bloque A — JSON
// builder.Services.AddSingleton<IPacienteRepository, JsonPacienteRepository>();

// ▶ Bloque B — CSV  ← activo
builder.Services.AddSingleton<IPacienteRepository>(_ => new CsvPacienteRepository(csvPacientes));

// ▶ Bloque C — SQLite
// builder.Services.AddSingleton<IPacienteRepository>(_ => new SqlitePacienteRepository(sqlitePath));
```

> **Nota:** El adapter SQLite requiere el paquete `Microsoft.Data.Sqlite` en el proyecto `CitasApp.Infrastructure`.

---

## 🗂️ Modelos principales

### 👤 Paciente

Contiene información básica de los pacientes:

* Id
* Nombre
* Apellido
* Correo electrónico
* Teléfono

### 👨‍⚕️ Médico

Contiene información profesional de los médicos:

* Id
* Nombre
* Apellido
* Especialidad
* Número de licencia

### 📅 Cita

Representa las citas médicas programadas:

* Id
* PacienteId
* MedicoId
* Fecha
* Hora
* Motivo
* Estado

---

## 🎨 Tecnologías utilizadas

* **.NET 10 MVC**
* **C#**
* **ASP.NET Core**
* **Razor Views (CSHTML)**
* **HTML5**
* **CSS3**
* **Bootstrap**
* **Entity Framework Core** (Identity)
* **Microsoft.Data.Sqlite** (adapter SQLite)
* **Visual Studio 2022**
* **Hexagonal Architecture Pattern (Clean Architecture)**

---

# 📚 Archivos principales

* **CitasApp.Domain** – El núcleo de la aplicación; contiene las entidades de negocio puras e interfaces de repositorio separadas por entidad (`IPacienteRepository`, `IMedicoRepository`, `ICitaRepository`).
* **CitasApp.Application** – Contiene la lógica de la aplicación y el flujo de los casos de uso implementados.
* **CitasApp.Infrastructure** – Tres adapters de persistencia intercambiables (JSON, CSV, SQLite) para cada entidad; sin dependencia del Dominio ni la Presentación.
* **CitasApp.Presentation** – Interfaz de usuario basada en el patrón MVC, controladores web, recursos estáticos (`wwwroot`) y el archivo de arranque central (`Program.cs`) donde se selecciona el adapter activo.

---

---

## 🤖 Cláusula de Uso de Inteligencia Artificial

Durante el desarrollo y posterior evolución de este proyecto se utilizó apoyo de herramientas de Inteligencia Artificial únicamente como recurso complementario de aprendizaje, consulta y automatización de tareas estructurales.

La IA fue empleada específicamente en:

* **Diseño y personalización de estilos CSS** para mejorar la apariencia visual de la aplicación.
* **Implementación de la lógica inicial para el registro** de pacientes, médicos y citas.
* **Orientación técnica para la refactorización arquitectónica**, guiando la separación del monolito original hacia una estructura limpia desacoplada en 4 capas (Domain, Application, Infrastructure, Presentation) mediante comandos de la CLI de .NET y Git.

Estas funcionalidades y cambios arquitectónicos representaban una lógica avanzada respecto a los conocimientos previos, por lo que la Inteligencia Artificial fue utilizada como apoyo para comprender conceptos de diseño de software modular, resolver dudas de dependencias y orientar la implementación.

El resto de la lógica de negocio, pruebas de compilación, adaptación final del código, resolución de rutas y documentación fueron realizadas de manera personal como parte del proceso de aprendizaje académico.

---


## 🎯 Objetivo de la práctica

Aplicar los principios de la arquitectura de software avanzada mediante la transición de un sistema monolítico tradicional hacia una arquitectura hexagonal, aislando de manera eficiente las reglas de negocio de los detalles técnicos y de presentación en capas independientes en .NET 10.

---

## 🤝 Agradecimientos

* **Profesor Jorge Javier Pedrozo Romero** por la guía y acompañamiento durante la práctica.
* **Tecnológico de Software** por brindar la formación académica y tecnológica necesaria para el desarrollo del proyecto.

---

## 📧 Contacto

* **Email Institucional:** [jesus.uc@tecdesoftware.edu.mx](mailto:jesus.uc@tecdesoftware.edu.mx)
* **GitHub:** https://github.com/JesusUc18

---

## 📄 Licencia

Este proyecto forma parte de las actividades académicas del **Tecnológico de Software** y se distribuye bajo la licencia MIT.

---

<div align="center">

**⭐ Si te gustó este proyecto, dale una estrella ⭐**

</div>
