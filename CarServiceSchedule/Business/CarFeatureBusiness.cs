using System.Collections.Generic;
using CarServiceSchedule.Model;
using CarServiceSchedule.Repositories;
using System.Linq;
using System.Threading.Tasks;
namespace CarServiceSchedule.Business
{
    public class CarFeatureBusiness : ICarFeatureBusiness
    {
        private readonly ICarFeatureRepository _carFeatureRepository;


        public CarFeatureBusiness(ICarFeatureRepository carFeatureRepository)
        {
            _carFeatureRepository = carFeatureRepository;
        }
        public Task<IEnumerable<CarFeature>>  GetCarFeatures() //async degil
        {
             var result = _carFeatureRepository.Get();

            return result;
 
        }

    }
}