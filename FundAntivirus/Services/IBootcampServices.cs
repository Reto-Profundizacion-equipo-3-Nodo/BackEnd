using BootcampApi.Models;

namespace BootcampApi.Services
{
    public interface IBootcampService
    {
        IEnumerable<Bootcamp> GetAll();
        Bootcamp GetById(int id);
        void Create(Bootcamp bootcamp);
        void Update(int id, Bootcamp bootcamp);
        void Delete(int id);
    }
}

