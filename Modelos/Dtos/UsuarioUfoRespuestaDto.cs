namespace ApiUfoCasesNet8.Modelos.Dtos
{
    public class UsuarioUfoRespuestaDto
    {
        //esto es para obtener cuando me autentique correctamente el usuario y el token
        //esto nos lo debe dar el servidor en la respuesta
        public UsuariosUfos Usuario { get; set; }

        public string Token { get; set; }
    }
}
