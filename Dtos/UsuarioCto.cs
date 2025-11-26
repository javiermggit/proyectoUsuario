namespace ApiUsuarios.Models
{
    public class UsuarioDto
    {
        public int usuario_id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Pais { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
    }
}
