using ApiUfoCasesNet8.Modelos;

namespace ApiUfoCasesNet8.UfoRepository.InterfacesUfos
{
    public interface IUfosRepository
    {
        ICollection<Ufo> GetUfos();

        Ufo GetUfos(int  ufoid);

        bool ExisteUfo(string nombre);

        bool ExisteUfo(int Id);

        bool CrearNuevoUfo(Ufo ufo);

        bool ActualizarUfo(Ufo ufo);

        bool Guardar();

        bool BorrarUfo(Ufo ufo);

        ICollection<Ufo> BuscarUfo(string nombre);


    }
}
