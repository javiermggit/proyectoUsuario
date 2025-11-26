using ApiUsuarios.Dtos;
using ApiUsuarios.Models;
using ApiUsuarios.Repositories;

namespace ApiUsuarios.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> CrearAsync(UsuarioCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new Exception("Nombre es obligatorio");

            if (await _repo.ObtenerPorTelefonoAsync(dto.Telefono) != null)
                throw new Exception("El teléfono ya está registrado");

            return await _repo.CrearAsync(dto);
        }

        public async Task<UsuarioDto> ObtenerPorIdAsync(int id)
            => await _repo.ObtenerPorIdAsync(id);

        public async Task<IEnumerable<UsuarioDto>> ObtenerTodosAsync()
            => await _repo.ObtenerTodosAsync();

        public async Task<bool> ActualizarAsync(int id, UsuarioUpdateDto dto)
            => await _repo.ActualizarAsync(id, dto);

        public async Task<bool> EliminarAsync(int id)
            => await _repo.EliminarAsync(id);
    }
}
