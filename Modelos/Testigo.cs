using System.ComponentModel.DataAnnotations;

namespace ApiUfoCasesNet8.Modelos
{
    public class Testigo
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Relacion { get; set; }
        public string Testimonio { get; set; }

    }
}
