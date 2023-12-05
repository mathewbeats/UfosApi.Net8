using ApiUfoCasesNet8.Modelos;
using ApiUfoCasesNet8.Modelos.Dtos;
using ApiUfoCasesNet8.UfoRepository.InterfacesUfos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiUfoCasesNet8.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController: ControllerBase
    {
        private readonly IUsuariosUfoRepository _ufosRepository;

        private readonly IMapper _mapper;

        protected RespuestAPI _respuestaAPI;

        public UsuariosController(IUsuariosUfoRepository ufosRepository, IMapper mapper)
        {
            _ufosRepository = ufosRepository;
            _mapper = mapper;
            this._respuestaAPI = new();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public ActionResult GetUsuario()
        {
            var listadDeUsuarios = _ufosRepository.GetAllUsuarios();

            var listaDeUsuariosDto = new List<UsuariosUfosDto>();

            foreach(var lista in listadDeUsuarios)
            {
                listaDeUsuariosDto.Add(_mapper.Map<UsuariosUfosDto>(lista));
            }

            return Ok(listadDeUsuarios);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("UsuarioId:int", Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public IActionResult GetUsuario(int usuarioId)
        {
            var itemUsuario = _ufosRepository.GetUsuario(usuarioId);

            if (itemUsuario == null)
            {
                return NotFound();
            }

            var itemUsuarioDto = _mapper.Map<UsuariosUfosDto>(itemUsuario);

            return Ok (itemUsuarioDto);
        }

        [AllowAnonymous]
        [HttpPost("Registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public async Task<ActionResult> Registro([FromBody] UsuarioRegistroUfosDto usuarioRegistroUfosDto)
        {
            bool validarNombreUsuario = _ufosRepository.IsUniqueUser(usuarioRegistroUfosDto.NombreDeUsuario);

            if(!validarNombreUsuario)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.IsSucces = false;
                _respuestaAPI.ErrorMessages.Add("El nombre de Usuario ya existe");
                return BadRequest();

            }

            var usuario = await _ufosRepository.Registro(usuarioRegistroUfosDto);

            if (usuario == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.IsSucces = false;
                _respuestaAPI.ErrorMessages.Add("error en el registro");
                return BadRequest(_respuestaAPI);
            }

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.IsSucces = true;
            return Ok(_respuestaAPI);

        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> Login([FromBody] UsuarioLoginUfosDto usuarioLoginUfosDto)
        {
            var respuestaLogin = await _ufosRepository.Login(usuarioLoginUfosDto);

            if (respuestaLogin.Usuario == null || string.IsNullOrEmpty(respuestaLogin.Token))
            {
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.IsSucces = false;
                _respuestaAPI.ErrorMessages.Add("El nombre de usuario o password son incorrectos");
                return BadRequest(_respuestaAPI);
            }


            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.IsSucces = true;
            _respuestaAPI.Resoult = respuestaLogin;
            return Ok(_respuestaAPI);
        }
    }


}
