using ApiUfoCasesNet8.Data;
using ApiUfoCasesNet8.Modelos;
using ApiUfoCasesNet8.Modelos.Dtos;
using ApiUfoCasesNet8.UfoRepository.InterfacesUfos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using XSystem.Security.Cryptography;
using System.Security.Claims;


namespace ApiUfoCasesNet8.UfoRepository
{
    public class UsuariosUfoRepository : IUsuariosUfoRepository
    {

        private readonly ApplicationDbContext _context;
        private string claveSecreta;



        public UsuariosUfoRepository(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
        }


        public ICollection<UsuariosUfos> GetAllUsuarios()
        {
           return _context.UsuariosUfos.OrderBy(u => u.Nombre).ToList();    
        }

        public UsuariosUfos GetUsuario(int UsuarioId)
        {
            return _context.UsuariosUfos.FirstOrDefault(u => u.Id == UsuarioId);
        }

        //public bool IsUniqueUser(string usuario)
        //{
        //    var usuarioBd = _context.UsuariosUfos.FirstOrDefault(u => u.NombreDeUsuario == usuario);
        //    if(usuario == null)
        //    {
        //        return true;
        //    }

        //    return false;
        //}


        public bool IsUniqueUser(string usuario)
        {
            var usuarioBd = _context.UsuariosUfos.FirstOrDefault(u => u.NombreDeUsuario == usuario);
            // Si usuarioBd es null, significa que no se encontró el usuario, y por lo tanto, es único
            return usuarioBd == null;
        }


        public async Task<UsuariosUfos> Registro(UsuarioRegistroUfosDto usuarioRegistroUfosDto)
        {
            var passwordEncriptada = obtenermd5(usuarioRegistroUfosDto.Password);

            UsuariosUfos usuario = new UsuariosUfos()
            {
                NombreDeUsuario = usuarioRegistroUfosDto.NombreDeUsuario,
                Password = passwordEncriptada,
                Nombre = usuarioRegistroUfosDto.Nombre,
                Role = usuarioRegistroUfosDto.Role
            };

            _context.Add(usuario);

            await _context.SaveChangesAsync();

            usuario.Password = passwordEncriptada;

            return usuario;

        }


        public async Task<UsuarioUfoRespuestaDto> Login(UsuarioLoginUfosDto usuarioLoginDto)
        {
            var passwordEncriptada = obtenermd5(usuarioLoginDto.Password);

            var usuario = _context.UsuariosUfos.FirstOrDefault(
                u => u.NombreDeUsuario.ToLower() == usuarioLoginDto.NombreDeUsuario.ToLower()
                && u.Password == passwordEncriptada
                );

                //validamos si el usuario existe con la combinancion de usuario y contraseña correcta

              if(usuario == null)
              {
                return new UsuarioUfoRespuestaDto()
                {
                    Token = "",
                    Usuario = null
                };
              }

            //aqui sí existe el usuario, por tanto podemos proceder al login

            var manejadorToken = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.NombreDeUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role),

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejadorToken.CreateToken(tokenDescriptor);

            UsuarioUfoRespuestaDto usuarioUfoRespuestaDto = new UsuarioUfoRespuestaDto()
            {
                Token = manejadorToken.WriteToken(token),
                Usuario = usuario
            };
            
            return usuarioUfoRespuestaDto;

        }



        //Método para encriptar contraseña con MD5 se usa tanto en el Acceso como en el Registro
        public static string obtenermd5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }
    }
}
