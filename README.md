A continuación se presenta una versión ampliada y detallada del README para el backend del proyecto "Antivirus Backend", considerando tanto la descripción original como los lineamientos y requisitos técnicos establecidos en el reto. Este documento está orientado a desarrolladores que se encargarán de la implementación y mantenimiento de la lógica de negocio, la integración de la base de datos y la seguridad de la API.

---

# Antivirus Backend

Antivirus Backend es un conjunto de APIs RESTful desarrollado en **.NET 9** que forma parte del desafío "Antivirus para la Deserción". Este proyecto se encarga de gestionar la lógica de negocio, el acceso a datos y la seguridad, proporcionando un servicio robusto y escalable. La aplicación utiliza **Entity Framework Core** para interactuar con una base de datos **PostgreSQL**, implementa autenticación basada en **JWT** para proteger endpoints sensibles y se documenta de forma interactiva a través de **Swagger/OpenAPI**.

## Índice

- [Arquitectura y Principios de Diseño](#arquitectura-y-principios-de-diseño)
- [Tecnologías Utilizadas](#tecnologías-utilizadas)
- [Requisitos Previos](#requisitos-previos)
- [Instalación y Configuración](#instalación-y-configuración)
- [Uso y Ejecución](#uso-y-ejecución)
- [Autenticación y Seguridad](#autenticación-y-seguridad)
- [Despliegue Automatizado](#despliegue-automatizado)
- [Estrategia de Desarrollo y Buenas Prácticas](#estrategia-de-desarrollo-y-buenas-prácticas)
- [Contribución](#contribución)
- [Licencia](#licencia)
- [Repositorio](#repositorio)

## Arquitectura y Principios de Diseño

El proyecto sigue los lineamientos del reto empresarial, haciendo énfasis en:
- **Separación de responsabilidades:**  
  - El **backend** se encarga exclusivamente de la lógica de negocio, acceso a datos y seguridad.
  - Se utiliza una arquitectura modular y escalable, basada en los principios **SOLID** para facilitar el mantenimiento y futuras ampliaciones.
- **Inyección de dependencias:**  
  - Se aprovechan las capacidades de .NET para la inyección de dependencias, permitiendo una mayor flexibilidad y facilidad en la realización de pruebas unitarias e integración.
- **Control de versiones y ramas:**  
  - El desarrollo se organiza mediante ramas individuales para cada integrante, con la integración final consolidada en la rama `master`.

Esta arquitectura permite que el frontend y el backend operen de forma desacoplada, garantizando que la interfaz de usuario se enfoque en la presentación y la interacción, mientras el backend maneja el procesamiento de datos y la lógica del negocio.

## Tecnologías Utilizadas

El proyecto se apoya en las siguientes tecnologías y herramientas:

- **.NET 9**  
  Framework de desarrollo para aplicaciones robustas y de alto rendimiento.
- **Entity Framework Core**  
  ORM que facilita la interacción con la base de datos PostgreSQL, permitiendo el uso de migraciones para la gestión del esquema.
- **PostgreSQL**  
  Sistema de gestión de bases de datos relacional que almacena de forma segura la información del sistema.
- **JWT (JSON Web Tokens)**  
  Protocolo de autenticación y autorización, usado para proteger endpoints sensibles y gestionar el acceso de usuarios.
- **Swagger/OpenAPI**  
  Herramienta que genera documentación interactiva, permitiendo la visualización y prueba de los endpoints de manera sencilla.

## Requisitos Previos

Antes de comenzar la instalación y ejecución del proyecto, asegúrate de contar con los siguientes elementos:

- **.NET 9 SDK:**  
  Debe estar instalado para compilar y ejecutar la aplicación.
- **PostgreSQL:**  
  Una instancia de PostgreSQL configurada para alojar la base de datos del proyecto.
- **Editor de Código:**  
  Se recomienda utilizar Visual Studio, Visual Studio Code o cualquier otro editor compatible con .NET.
- **Git:**  
  Para clonar y gestionar el repositorio del proyecto.

## Instalación y Configuración

1. **Clonar el repositorio:**

   ```bash
   git clone https://github.com/Reto-Profundizacion-equipo-3-Nodo/BackEnd.git
   cd BackEnd
   ```

2. **Configuración de la cadena de conexión:**  
   Modifica el archivo `appsettings.json` para configurar la cadena de conexión a la base de datos. Ejemplo:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Database=antivirus_db;Username=usuario;Password=contraseña"
   }
   ```

3. **Aplicar migraciones:**  
   Una vez configurada la cadena de conexión, ejecuta el siguiente comando para actualizar el esquema de la base de datos:

   ```bash
   dotnet ef database update
   ```

4. **Instalación de dependencias:**  
   Si se requiere, restaura los paquetes NuGet ejecutando:

   ```bash
   dotnet restore
   ```

## Uso y Ejecución

Para iniciar el servidor y comenzar a consumir los endpoints de la API:

1. **Ejecutar el proyecto:**

   ```bash
   dotnet run
   ```

2. **Acceso a la API:**  
   Una vez en ejecución, la API estará disponible en las siguientes direcciones:
   - **HTTP:** [http://localhost:5000](http://localhost:5000)
   - **HTTPS:** [https://localhost:5001](https://localhost:5001)

3. **Documentación interactiva con Swagger:**  
   Puedes acceder a la documentación de la API a través de Swagger en [http://localhost:5000/swagger](http://localhost:5000/swagger), donde se pueden probar y visualizar todos los endpoints disponibles.

## Autenticación y Seguridad

El sistema implementa **JWT** para gestionar la autenticación y autorización:

- **Obtención de Token:**  
  Para acceder a los endpoints protegidos, se debe realizar una solicitud **POST** al endpoint `/api/login` con credenciales válidas.
- **Protección de Endpoints:**  
  Los endpoints sensibles están configurados para requerir un token JWT válido, asegurando así que solo usuarios autenticados (especialmente con rol Admin para operaciones críticas) puedan interactuar con ellos.

## Despliegue Automatizado

El proyecto cuenta con integración continua y despliegue automatizado mediante **GitHub Actions** en **Azure**. Para configurar el despliegue:

1. **Configurar el archivo de flujo de trabajo:**  
   Utiliza el archivo `.github/workflows/deploy.yml` o `azure-pipelines.yml` según corresponda.
2. **Definir secretos en GitHub:**  
   Se deben establecer los siguientes secretos:
   - `AZURE_WEBAPP_NAME`
   - `AZURE_CREDENTIALS`
3. **Automatización del Despliegue:**  
   Una vez configurado, el despliegue se realizará automáticamente tras la fusión en la rama `master`.

## Estrategia de Desarrollo y Buenas Prácticas

El desarrollo del proyecto se rige por las siguientes buenas prácticas, alineadas con los requerimientos del reto empresarial:

- **Código Limpio y Documentado:**  
  Se sigue un estilo de codificación consistente y se incluye documentación inline para facilitar el mantenimiento.
- **Pruebas Unitarias e Integración:**  
  Se recomienda implementar pruebas que aseguren el correcto funcionamiento de cada endpoint y la integración entre componentes.
- **Uso Adecuado del Control de Versiones:**  
  Cada integrante desarrolla en ramas individuales y se realiza una integración final mediante pull requests para consolidar los cambios en la rama `master`.
- **Manejo de Errores y Validaciones:**  
  Se implementa un manejo robusto de errores tanto en el lado del servidor como en la validación de entradas para garantizar la estabilidad y seguridad de la aplicación.

## Contribución

Si deseas contribuir a este proyecto, sigue los siguientes pasos:

1. **Haz un fork del repositorio.**
2. **Crea una rama para tu funcionalidad o corrección:**

   ```bash
   git checkout -b feature-nueva
   ```

3. **Realiza los cambios y haz commit con un mensaje descriptivo:**

   ```bash
   git commit -m "Descripción del cambio"
   ```

4. **Envía tus cambios a tu fork:**

   ```bash
   git push origin feature-nueva
   ```

5. **Crea un Pull Request** para que los cambios sean revisados e integrados en la rama `master`.

## Licencia

Este proyecto se distribuye bajo la **Licencia MIT**, lo que permite su uso, modificación y distribución bajo los términos establecidos en dicha licencia.

