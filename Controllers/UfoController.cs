using ApiUfoCasesNet8.Modelos;
using ApiUfoCasesNet8.Modelos.Dtos;
using ApiUfoCasesNet8.UfoRepository.InterfacesUfos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiUfoCasesNet8.Controllers
{
    [ApiController]
    [Route("api/ufo")]
    public class UfoController : ControllerBase
    {

        private readonly IUfosRepository _ufosRepository;

        private readonly IMapper _mapper;

        public UfoController(IUfosRepository ufosRepository, IMapper mapper)
        {
            _mapper = mapper;
            _ufosRepository = ufosRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Ufo> GetUfos()
        {
            var listaDeUfos = _ufosRepository.GetUfos();

            var listaUfosDto = new List<Ufo>();

            foreach(var item in listaDeUfos)
            {
                listaUfosDto.Add(_mapper.Map<Ufo>(item));
            }

            return Ok(listaUfosDto);
        }

        [AllowAnonymous]
        [HttpGet("{ufoId:int}", Name = "GetUfo")]
        //asegurarnos que el int en "{ufoId:int}" va junto ya que si hay espacios entre ufoId: int no compilar
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public ActionResult<Ufo>GetUfo(int ufoId)
        {
            var itemUfo = _ufosRepository.GetUfos(ufoId);

            if(itemUfo == null)
            {
                return NotFound();
            }

            var itemUfoDto = _mapper.Map<UfoDto>(itemUfo);

            return Ok(itemUfoDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UfoDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult CrearUfo([FromBody] UfoDto ufoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(ufoDto == null)
            {
                return BadRequest(ModelState);
            }
            if(_ufosRepository.ExisteUfo(ufoDto.Nombre))
            {
                ModelState.AddModelError("", "El Ufo ya existe en el sistema");
                return StatusCode(404, ModelState);
            }

            var ufo = _mapper.Map<Ufo>(ufoDto);

            if (!_ufosRepository.CrearNuevoUfo(ufo))
            {
                ModelState.AddModelError("", $"Algo salio mal al crear {ufo.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetUfo", new {ufoId = ufo.Id}, ufo);
        }


        [Authorize(Roles = "admin")]
        [HttpPatch("{ufoId:int}", Name = "ActualizarPatchUfo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult ActualizarPatchUfo([FromBody] UfoDto ufoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ufo = _mapper.Map<Ufo>(ufoDto);

            if (!_ufosRepository.ActualizarUfo(ufo))
            {
                ModelState.AddModelError("", $"Algo salio mal al actualizar {ufo.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{ufoId:int}", Name = "BorrarUfo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult BorrarUfo(int ufoId)
        {
            if (!_ufosRepository.ExisteUfo(ufoId))
            {
                return NotFound();
            }

            var ufo = _ufosRepository.GetUfos(ufoId);

            if (!_ufosRepository.BorrarUfo(ufo))
            {
                ModelState.AddModelError("", $"Algo salio mal al borrar el registro {ufo.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("BuscarUfo")]
        public ActionResult Buscar(string nombre)
        {
            try
            {
                var resultado = _ufosRepository.BuscarUfo(nombre.Trim());

                if (resultado.Any())
                {
                    return Ok(resultado);
                }

                return NotFound();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos");
            }
            
        }
    }
}
