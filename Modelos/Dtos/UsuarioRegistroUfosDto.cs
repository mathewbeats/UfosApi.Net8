using System.ComponentModel.DataAnnotations;

namespace ApiUfoCasesNet8.Modelos.Dtos
{
    public class UsuarioRegistroUfosDto
    {

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string NombreDeUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Password es obligatoria")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
