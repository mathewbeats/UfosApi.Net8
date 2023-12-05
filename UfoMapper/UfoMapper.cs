using ApiUfoCasesNet8.Modelos;
using ApiUfoCasesNet8.Modelos.Dtos;
using AutoMapper;

namespace ApiUfoCasesNet8.UfoMapper
{
    public class UfoMapper: Profile
    {
        public UfoMapper()
        {

            // Definir mapeos entre Ufo y UfoDTO
            CreateMap<Ufo, UfoDto>().ReverseMap();
            CreateMap<Testigo, TestigoDto>().ReverseMap();
            CreateMap<UsuariosUfos, UsuariosUfosDto>().ReverseMap();
        }
    }
}
