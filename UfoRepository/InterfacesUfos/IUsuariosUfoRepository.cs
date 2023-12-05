using ApiUfoCasesNet8.Modelos;
using ApiUfoCasesNet8.Modelos.Dtos;

namespace ApiUfoCasesNet8.UfoRepository.InterfacesUfos
{
    public interface IUsuariosUfoRepository
    {
        ICollection<UsuariosUfos> GetAllUsuarios();

        UsuariosUfos GetUsuario(int UsuarioId);

        bool IsUniqueUser(string usuario);

        Task<UsuarioUfoRespuestaDto> Login(UsuarioLoginUfosDto usuarioLoginDto);

        Task<UsuariosUfos> Registro(UsuarioRegistroUfosDto usuarioRegistro);

      
    }
}
