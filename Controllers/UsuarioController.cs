using ApiUsuarios.Dtos;
using ApiUsuarios.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        // ------------------------------
        // POST: Crear usuario
        // ------------------------------
        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioCreateDto dto)
        {
            try
            {
                var id = await _service.CrearAsync(dto);
                return Ok(new { UsuarioId = id, Mensaje = "Usuario creado." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ------------------------------
        // GET: Obtener por ID
        // ------------------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var usuario = await _service.ObtenerPorIdAsync(id);
            return usuario == null ? NotFound() : Ok(usuario);
        }

        // ------------------------------
        // GET: Obtener todos
        // ------------------------------
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
            => Ok(await _service.ObtenerTodosAsync());

        // ------------------------------
        // PUT: Actualizar usuario
        // ------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, UsuarioUpdateDto dto)
        {
            try
            {
                bool result = await _service.ActualizarAsync(id, dto);
                return result ? Ok("Actualizado.") : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ------------------------------
        // DELETE: Eliminar usuario
        // ------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                bool result = await _service.EliminarAsync(id);
                return result ? Ok("Eliminado.") : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
