using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiUfoCasesNet8.Modelos
{
    public class Ufo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        // Entidad avistada (OVNI, entidad extraterrestre, etc.)

        [Required]
        public string Entity { get; set; }

        // Edad estimada de la entidad, si es aplicable
        public int? Edad { get; set; }

        [Required]
        public string Descripcion { get; set; }

        // Lugar de origen o avistamiento
        [Required]
        public string LugarDeOrigen { get; set; }

        // Fecha y hora del avistamiento
        [Required]
        public DateTime FechaAvistamiento { get; set; }

        // Descripción detallada del avistamiento
        [Required]
        public string DetallesAvistamiento { get; set; }

        // URL de la imagen (se puede almacenar en una base de datos o referenciar a un servicio de almacenamiento de imágenes)
        [Required]
        public string UrlImagen { get; set; }

        // Nivel de credibilidad del avistamiento (escala del 1 al 10)

        public int NivelCredibilidad { get; set; }

        // Testigos presentes durante el avistamiento
        [NotMapped] // No se mapea a la base de datos si es un campo complejo
        public Testigo[] Testigos { get; set; }

        // Posibles explicaciones alternativas al avistamiento
        [Required]
        public string? ExplicacionesAlternativas { get; set; }

        // Si el avistamiento fue investigado por alguna organización
        public bool? Investigado { get; set; }

        // Resultados de la investigación, si la hay
        public string? ResultadosInvestigacion { get; set; }
    }
}
