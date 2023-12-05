using System.ComponentModel.DataAnnotations;

namespace ApiUfoCasesNet8.Modelos
{
    public class UsuariosUfos
    {
        [Key]
        public int Id { get; set; }

        public string NombreDeUsuario { get; set; }

        public string Nombre {  get; set; }

        public string Password { get; set; }

        public string Role {  get; set; }

    }
}
