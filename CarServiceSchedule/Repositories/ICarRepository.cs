using System.Collections.Generic;
using System.Threading.Tasks;
using CarServiceSchedule.Model;

namespace CarServiceSchedule.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> Get();
        Task<Car> Get(int id);
        Task<Car> Create(Car book);
        Task Update(Car book);
        Task Delete(int id);
    }
}