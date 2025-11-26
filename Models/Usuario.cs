namespace ApiUsuarios.Models
{
    public class Usuario
    {
        public int usuario_id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int pais_id { get; set; }
        public int departamento_id { get; set; }
        public int municipio_id { get; set; }
    }
}
