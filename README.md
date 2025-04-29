# Quesería Sin Ti

[![.NET 9.0](https://img.shields.io/badge/.NET-9.0-blue)](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
[![Visual Studio](https://img.shields.io/badge/Visual%20Studio-2022-blueviolet)](https://visualstudio.microsoft.com/es/vs/community/)

**Desarrollo evolutivo grupal del trabajo de campo - Ingeniería II**

---

## Documentación adjunta

Se puede consultar el informe del proyecto descargandolo desde aqui:  
[📄 Ver Informe (PDF)](https://github.com/user-attachments/files/19951032/TrabajoDeCampoQueseriaSoftware-2025.pdf)

---

## Índice

- [Requisitos de instalación](#requisitos-de-instalación)
- [Instrucciones de instalación](#instrucciones-de-instalación)
- [Datos de prueba](#datos-de-prueba)
- [Notas adicionales](#notas-adicionales)

---

## Requisitos de instalación

- [Visual Studio Community 2022](https://visualstudio.microsoft.com/es/vs/community/) (recomendado) o cualquier editor/compilador compatible con .NET.
- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0).
- [SQL Server Management Studio (SSMS 20)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms).
- [Git](https://git-scm.com/).

---

## Instrucciones de instalación

1. **Clonar el repositorio**

   ```bash
   git clone https://github.com/tu_usuario/queseria_sin_ti.git


2. **Abrir la solución**

   - Abre el archivo `QueseriaSoftware.sln` en Visual Studio 2022 (o en tu editor preferido).

3. **Verificar la rama**

   - Asegúrate de estar en la rama `master` y de haber descargado los últimos cambios.

4. **Configurar la cadena de conexión**

   - Abre el archivo `appsettings.json`.
   - Modifica la sección `ConnectionStrings` para configurar la ruta de conexión al servidor SQL donde se creará la base de datos.

5. **Crear la base de datos**

   - Abre una terminal sobre la solución.
   - Ejecuta el siguiente comando:

     ```bash
     dotnet ef database update
     ```

   Esto creará la base de datos local del proyecto, incluyendo todos los cambios disponibles.

---

## Datos de prueba

- **Correo de administrador:** `admin@queseria.com`
- **Contraseña:** `admin123`
- Se cargan también **dos productos de prueba** en la base de datos.

---

## Notas adicionales

- La base de datos se crea automáticamente con los cambios de esquema definidos.
- Puedes ajustar otros parámetros de entorno (como nombre de base de datos o configuración de logs) desde el archivo `appsettings.json`.
- Para ejecutar el comando `dotnet ef database update`, asegúrate de tener instalado [Entity Framework Core Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet).

---

> Proyecto desarrollado con ❤️ por el grupo 18, equipo de **Quesería Sin Ti**.
- [@Benites, Matias Maximiliano](https://github.com/Matiasbenites)
- [@Fernández, Erika ](https://github.com/erika00f)
- [@Fernandez Gotusso, Maria Daniela ](https://github.com/yoquienmas)
- [@Frias, Javier](https://github.com/spuk0)
- [@Garcia, Brenda](https://github.com/BrendaGarcia3)
