using System.Collections.Generic;
using System.Threading.Tasks;
using CarServiceSchedule.Model;
using CarServiceSchedule.Repositories;

namespace CarServiceSchedule.Business
{
    public interface ICarFeatureBusiness
    {
        public Task<IEnumerable<CarFeature>> GetCarFeatures();
    }
}