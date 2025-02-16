using BootcampApi.Models;

namespace BootcampApi.Services
{
    public class BootcampService : IBootcampService
    {
        private static List<Bootcamp> _bootcamps = new List<Bootcamp>
        {
            new Bootcamp { Id = 1, Nombre = "Academia A", Descripcion = "Bootcamp de desarrollo web", FechaInicio = new DateTime(2025, 1, 10), FechaFin = new DateTime(2025, 4, 10) },
            new Bootcamp { Id = 2, Nombre = "Academia B", Descripcion = "Bootcamp de ciberseguridad", FechaInicio = new DateTime(2025, 2, 15), FechaFin = new DateTime(2025, 5, 15) }
        };

        public IEnumerable<Bootcamp> GetAll() => _bootcamps;

        public Bootcamp GetById(int id) => _bootcamps.FirstOrDefault(b => b.Id == id);

        public void Create(Bootcamp bootcamp) => _bootcamps.Add(bootcamp);

        public void Update(int id, Bootcamp bootcamp)
        {
            var existingBootcamp = _bootcamps.FirstOrDefault(b => b.Id == id);
            if (existingBootcamp != null)
            {
                existingBootcamp.Nombre = bootcamp.Nombre;
                existingBootcamp.Descripcion = bootcamp.Descripcion;
                existingBootcamp.FechaInicio = bootcamp.FechaInicio;
                existingBootcamp.FechaFin = bootcamp.FechaFin;
            }
        }

        public void Delete(int id) => _bootcamps.RemoveAll(b => b.Id == id);
    }
}