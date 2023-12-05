using ApiUfoCasesNet8.Data;
using ApiUfoCasesNet8.Modelos;
using ApiUfoCasesNet8.UfoRepository.InterfacesUfos;

namespace ApiUfoCasesNet8.UfoRepository
{
    public class UfosRepository : IUfosRepository
    {
        private readonly ApplicationDbContext _context;

        public UfosRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public ICollection<Ufo> GetUfos()
        {
            return _context.Ufo.OrderBy(c => c.Nombre).ToList();
        }

        public Ufo GetUfos(int ufoid)
        {
            return _context.Ufo.FirstOrDefault(c => c.Id == ufoid);
        }


        public bool ActualizarUfo(Ufo ufo)
        {
           //ufo.FechaAvistamiento = DateTime.Now;

            _context.Ufo.Update(ufo);

            return Guardar();
        }

        public bool BorrarUfo(Ufo ufo)
        {
           _context.Ufo.Remove(ufo);

            return Guardar();
        }



        public ICollection<Ufo> BuscarUfo(string nombre)
        {
            IQueryable<Ufo> query = _context.Ufo; // Prepara una consulta para todas las películas.


            // Si se proporciona un nombre, filtra las películas que contienen ese nombre o descripción.
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.Nombre.Contains(nombre) || e.Descripcion.Contains(nombre));
            }

            return query.ToList(); // Ejecuta la consulta y devuelve una lista de películas.
        }

        public bool CrearNuevoUfo(Ufo ufo)
        {
            //ufo.FechaAvistamiento = DateTime.Now;

            _context.Ufo.Add(ufo);

            return Guardar();
        }

        public bool ExisteUfo(string nombre)
        {
            bool valor = _context.Ufo.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());

            return valor;
        }

        public bool ExisteUfo(int Id)
        {
            bool valor = _context.Ufo.Any(c => c.Id == Id);

            return valor;
        }

    

        public bool Guardar()
        {
           //return _context.SaveChanges() >= 0 ? true : false;

            return _context.SaveChanges() >= 0;


        }

       
    }
}
