using ApiUsuarios.Dtos;
using ApiUsuarios.Models;
using Dapper;
using Npgsql;

namespace ApiUsuarios.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration _config;

        public UsuarioRepository(IConfiguration config)
        {
            _config = config;
        }

        private NpgsqlConnection Connection =>
            new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));

        public async Task<int> CrearAsync(UsuarioCreateDto dto)
        {
            using var con = Connection;

            // 🔍 VALIDAR PAÍS
            var paisExiste = await con.ExecuteScalarAsync<bool>(
                "SELECT EXISTS(SELECT 1 FROM pais WHERE pais_id = @id)",
                new { id = dto.PaisId });

            if (!paisExiste)
                throw new Exception("El país seleccionado no existe.");

            // 🔍 VALIDAR DEPARTAMENTO
            var deptoExiste = await con.ExecuteScalarAsync<bool>(
                "SELECT EXISTS(SELECT 1 FROM departamento WHERE departamento_id = @id AND pais_id = @pais)",
                new { id = dto.DepartamentoId, pais = dto.PaisId });

            if (!deptoExiste)
                throw new Exception("El departamento no existe o no pertenece al país.");

            // 🔍 VALIDAR MUNICIPIO
            var muniExiste = await con.ExecuteScalarAsync<bool>(
                "SELECT EXISTS(SELECT 1 FROM municipio WHERE municipio_id = @id AND departamento_id = @depto)",
                new { id = dto.MunicipioId, depto = dto.DepartamentoId });

            if (!muniExiste)
                throw new Exception("El municipio no existe o no pertenece al departamento.");

            // ✔ Ejecutar SP
            var sql = "SELECT sp_crear_usuario(@Nombre, @Telefono, @Direccion, @PaisId, @DepartamentoId, @MunicipioId)";
            return await con.ExecuteScalarAsync<int>(sql, dto);
        }

        public async Task<UsuarioDto> ObtenerPorIdAsync(int id)
        {
            using var con = Connection;
            return await con.QueryFirstOrDefaultAsync<UsuarioDto>(
                "SELECT * FROM sp_obtener_usuario(@id)", new { id });
        }

        public async Task<IEnumerable<UsuarioDto>> ObtenerTodosAsync()
        {
            using var con = Connection;
            return await con.QueryAsync<UsuarioDto>("SELECT * FROM sp_obtener_todos_usuarios()");
        }

        public async Task<bool> ActualizarAsync(int id, UsuarioUpdateDto dto)
        {
            using var con = Connection;

            // mismas validaciones que crear

            var paisExiste = await con.ExecuteScalarAsync<bool>(
                "SELECT EXISTS(SELECT 1 FROM pais WHERE pais_id = @id)",
                new { id = dto.PaisId });

            if (!paisExiste)
                throw new Exception("El país seleccionado no existe.");

            var deptoExiste = await con.ExecuteScalarAsync<bool>(
                "SELECT EXISTS(SELECT 1 FROM departamento WHERE departamento_id = @id AND pais_id = @pais)",
                new { id = dto.DepartamentoId, pais = dto.PaisId });

            if (!deptoExiste)
                throw new Exception("El departamento no existe o no pertenece al país.");

            var muniExiste = await con.ExecuteScalarAsync<bool>(
                "SELECT EXISTS(SELECT 1 FROM municipio WHERE municipio_id = @id AND departamento_id = @depto)",
                new { id = dto.MunicipioId, depto = dto.DepartamentoId });

            if (!muniExiste)
                throw new Exception("El municipio no existe o no pertenece al departamento.");

            var sql = @"SELECT sp_actualizar_usuario(@Id, @Nombre, @Telefono, @Direccion, 
                        @PaisId, @DepartamentoId, @MunicipioId)";

            return await con.ExecuteScalarAsync<bool>(sql, new
            {
                Id = id,
                dto.Nombre,
                dto.Telefono,
                dto.Direccion,
                dto.PaisId,
                dto.DepartamentoId,
                dto.MunicipioId
            });
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var con = Connection;
            return await con.ExecuteScalarAsync<bool>("SELECT sp_eliminar_usuario(@id)", new { id });
        }

        public async Task<Usuario> ObtenerPorTelefonoAsync(string telefono)
        {
            using var con = Connection;
            return await con.QueryFirstOrDefaultAsync<Usuario>(
                "SELECT * FROM usuarios WHERE telefono = @telefono",
                new { telefono });
        }
    }
}
