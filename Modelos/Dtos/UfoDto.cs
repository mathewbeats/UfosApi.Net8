using System.ComponentModel.DataAnnotations;

namespace ApiUfoCasesNet8.Modelos.Dtos
{
    public class UfoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La entidad es obligatorio")]
        public string Entity { get; set; }
        [Required(ErrorMessage = "La descripcion es obligatorio")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El Lugar de origen es obligatorio")]
        public string LugarDeOrigen { get; set; }
        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime FechaAvistamiento { get; set; }
        [Required(ErrorMessage = "La imagen es obligatoria")]
        public string UrlImagen { get; set; }
        [Required(ErrorMessage = "Los detalles de avsitamientos son obligatorios")]
        public string DetallesAvistamiento { get; set; }

        [Required(ErrorMessage = "Las explicaciones alternativas son obligatorias")]
        public string? ExplicacionesAlternativas { get; set; }
    }
}
