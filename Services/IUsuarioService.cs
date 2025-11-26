using ApiUsuarios.Dtos;
using ApiUsuarios.Models;

namespace ApiUsuarios.Services
{
    public interface IUsuarioService
    {
        Task<int> CrearAsync(UsuarioCreateDto dto);
        Task<UsuarioDto> ObtenerPorIdAsync(int id);
        Task<IEnumerable<UsuarioDto>> ObtenerTodosAsync();
        Task<bool> ActualizarAsync(int id, UsuarioUpdateDto dto);
        Task<bool> EliminarAsync(int id);
    }
}
