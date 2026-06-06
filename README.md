# Arquitectura de Software - Actividad #14 - Práctica .NET: Implementar MVC con ASP.NET Core

## 👨‍💻 Información del Estudiante

* **Nombre:** Jesús Omar Uc Domínguez
* **Matrícula:** SW2509031
* **Grupo:** 3B
* **Cuatrimestre:** 3er Cuatrimestre
* **Carrera:** TSU en Desarrollo e Innovación de Software
* **Profesor:** Jorge Javier Pedrozo Romero

---

# 🏥 Sistema de Gestión de Citas Médicas

Este proyecto es una **aplicación web MVC desarrollada en .NET** que permite administrar información relacionada con pacientes, médicos y citas médicas.

El sistema facilita el registro y visualización de pacientes, médicos y citas, organizando la información mediante el patrón de arquitectura **Modelo-Vista-Controlador (MVC)**. Esto permite mantener una estructura ordenada, escalable y fácil de mantener.

---

## 📌 Características

* Registro y gestión de pacientes.
* Registro y gestión de médicos.
* Administración de citas médicas.
* Organización mediante arquitectura MVC.
* Interfaz web desarrollada con Razor Views.
* Separación clara entre modelos, vistas y controladores.
* Código estructurado para futuras ampliaciones.

---

## 🩺 Cómo funciona el sistema

1. **Inicio de la aplicación:** El usuario accede al sistema desde el navegador.
2. **Gestión de pacientes:** Se pueden visualizar y administrar los datos de los pacientes.
3. **Gestión de médicos:** Se registran y consultan médicos con su especialidad y número de licencia.
4. **Gestión de citas:** Se programan citas asociando pacientes y médicos.
5. **Controladores MVC:** Procesan las solicitudes y retornan las vistas correspondientes.
6. **Modelos:** Representan las entidades principales del sistema.
7. **Vistas:** Presentan la información de manera visual e interactiva.

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
├── Areas/
│   └── Identity/
│
├── Controllers/
│   ├── HomeController.cs
│   ├── PacienteController.cs
│   ├── MedicoController.cs
│   └── CitaController.cs
│
├── Data/
│   ├── ApplicationDbContext.cs
│   ├── Pacientes.json
│   ├── Medico.json
│   └── Cita.json
│
├── Migrations/
│
├── Models/
│   ├── Paciente.cs
│   ├── Medico.cs
│   ├── Cita.cs
│   └── ErrorViewModel.cs
│
├── Properties/
│
├── Views/
│   ├── Cita/
│   │   ├── Crear.cshtml
│   │   ├── Index.cshtml
│   │   └── PorPaciente.cshtml
│   │
│   ├── Home/
│   │   ├── Index.cshtml
│   │   └── Privacy.cshtml
│   │
│   ├── Medico/
│   │   ├── Crear.cshtml
│   │   ├── Detalle.cshtml
│   │   └── Index.cshtml
│   │
│   ├── Paciente/
│   │   ├── Crear.cshtml
│   │   ├── Detalle.cshtml
│   │   └── Index.cshtml
│   │
│   └── Shared/
│       ├── _Layout.cshtml
│       ├── _LoginPartial.cshtml
│       ├── _ValidationScriptsPartial.cshtml
│       └── Error.cshtml
│
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── lib/
│
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
├── CitasApp.csproj
├── CitasApp.slnx
├── README.md
├── .gitignore
└── .gitattributes
```

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

* **.NET 8 MVC**
* **C#**
* **ASP.NET Core**
* **Razor Views (CSHTML)**
* **HTML5**
* **CSS3**
* **Bootstrap**
* **Entity Framework Core**
* **Visual Studio 2022**
* **MVC Pattern (Model-View-Controller)**

---

# 📚 Archivos principales

* **Program.cs** – Configuración principal y arranque de la aplicación.
* **Controllers/** – Controladores encargados de gestionar las solicitudes.
* **Models/** – Entidades principales del sistema.
* **Views/** – Interfaz visual para los usuarios.
* **Data/** – Configuración y acceso a datos.
* **wwwroot/** – Recursos estáticos como CSS, JavaScript e imágenes.
* **appsettings.json** – Configuración general del proyecto.

---

---

## 🤖 Cláusula de Uso de Inteligencia Artificial

Durante el desarrollo de este proyecto se utilizó apoyo de herramientas de Inteligencia Artificial únicamente como recurso complementario de aprendizaje y consulta.

La IA fue empleada específicamente en:

* **Diseño y personalización de estilos CSS** para mejorar la apariencia visual de la aplicación.
* **Implementación de la lógica para el registro de pacientes.**
* **Implementación de la lógica para el registro de médicos.**
* **Implementación de la lógica para el registro de citas.**

Estas funcionalidades representaban una lógica más avanzada respecto a los conocimientos que poseía al momento de desarrollar la práctica, por lo que la Inteligencia Artificial fue utilizada como apoyo para comprender conceptos, resolver dudas y orientar la implementación.

El resto de la estructura del proyecto, organización de archivos, adaptación del código, pruebas, comprensión de la funcionalidad y documentación fueron realizadas de manera personal como parte del proceso de aprendizaje académico.

---


## 🎯 Objetivo de la práctica

Aplicar los principios de la arquitectura MVC mediante el desarrollo de una aplicación web capaz de gestionar pacientes, médicos y citas médicas, fortaleciendo el uso de .NET, C# y la organización del código en capas.

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
