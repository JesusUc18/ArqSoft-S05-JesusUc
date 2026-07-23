# Arquitectura de Software - Actividad #32 - Detectar Code-Smell y Refactorizar

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
(test)

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
