using System.ComponentModel.DataAnnotations;

namespace ApiUfoCasesNet8.Modelos.Dtos
{
    public class UsuarioLoginUfosDto
    {
         
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string NombreDeUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }

    }
}
