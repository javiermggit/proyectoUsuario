namespace ApiUsuarios.Dtos
{
    public class UsuarioUpdateDto
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int PaisId { get; set; }
        public int DepartamentoId { get; set; }
        public int MunicipioId { get; set; }
    }
}
