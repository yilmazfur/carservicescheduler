using System.Collections.Generic;
using System.Threading.Tasks;
using CarServiceSchedule.Model;

namespace CarServiceSchedule.Business
{
    public interface IDemandBusiness
    {

        public Task<IEnumerable<Demand>> GetDemands();

        public Task<Demand> CreateDemand(Demand demand);
    }
}