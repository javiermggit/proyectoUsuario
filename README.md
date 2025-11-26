API Usuarios – .NET 8 + PostgreSQL

API REST para administrar usuarios y su localización (país, departamento, municipio), usando stored procedures en PostgreSQL.
Incluye CRUD completo, validaciones y estructura modular con controladores, servicios y repositorios.

📦 Tecnologías Utilizadas

.NET 8 Web API

C#

PostgreSQL

Stored Procedures (PL/pgSQL)

Dapper

Postman (pruebas)

⚙️ Instalación y Configuración
✨ 1. Clonar el repositorio
git clone https://github.com/javiermggit/proyectoUsuario.git
cd TU-REPO

🛢 2. Configurar la base de datos
📁 Ubicación del archivo SQL

El archivo completo para inicializar la base de datos está en:

/Database/userDb.sql

🧩 Contiene:

Creación de la base UsuariosDB

Tablas:

pais

departamento

municipio

usuarios

Inserts iniciales

Stored Procedures:

sp_crear_usuario

sp_obtener_usuario

sp_obtener_todos_usuarios

sp_actualizar_usuario

sp_eliminar_usuario

▶️ Ejecutar script
🔹 Opción A – pgAdmin

Abrir pgAdmin

Crear conexión

Abrir Query Tool

Cargar userDb.sql

Ejecutar (▶️)

🔹 Opción B – Línea de comandos (psql)
psql -U postgres -f Database/userDb.sql

🔧 3. Configurar cadena de conexión

Editar appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Username=postgres;Password=12345;Database=UsuariosDB;"
  }
}


▶️ 4. Ejecutar la API
dotnet run


La API se iniciará en:

https://localhost:7128
http://localhost:5033

🧪 Endpoints (CRUD Usuarios)
📌 Crear Usuario

POST /api/usuario

{
  "nombre": "Javier Martínez",
  "telefono": "3004567891",
  "paisId": 1,
  "departamentoId": 5,
  "municipioId": 18,
  "direccion": "Calle 123 # 45 - 67"
}

📌 Obtener Usuario por ID

GET /api/usuario/{id}

📌 Obtener Todos

GET /api/usuario

Este endpoint retorna:

Información del usuario

Nombres de país, departamento y municipio

📌 Actualizar

PUT /api/usuario/{id}

📌 Eliminar

DELETE /api/usuario/{id}

🗂 Estructura del Proyecto
ApiUsuarios/
│
├── Controllers/
│   └── UsuarioController.cs
│
├── Services/
│   ├── IUsuarioService.cs
│   └── UsuarioService.cs
│
├── Repositories/
│   ├── IUsuarioRepository.cs
│   └── UsuarioRepository.cs
│
├── Models/
│   └── Usuario.cs
│
├── Dtos/
│   ├── UsuarioCreateDto.cs
│   ├── UsuarioUpdateDto.cs
│   └── UsuarioDto.cs
│
├── Database/
│   └── userDb.sql
│
└── appsettings.json

🛡 Validaciones Implementadas

✔ Verificación de que exista el país, departamento y municipio
✔ Manejo de errores controlado
✔ Validación de teléfono único
✔ Respuestas claras en formato JSON

📄 Licencia

Este proyecto es de uso libre para fines educativos o personales.
