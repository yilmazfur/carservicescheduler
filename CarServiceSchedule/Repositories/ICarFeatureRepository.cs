using System.Collections.Generic;
using System.Threading.Tasks;
using CarServiceSchedule.Model;

namespace CarServiceSchedule.Repositories
{
    public interface ICarFeatureRepository
    {
        Task<IEnumerable<CarFeature>> Get();
        Task<CarFeature> Get(int id);
        Task<CarFeature> Create(CarFeature book);
        Task Update(CarFeature book);
        Task Delete(int id);
    }
}