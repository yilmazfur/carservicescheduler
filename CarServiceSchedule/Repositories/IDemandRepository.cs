using System.Collections.Generic;
using System.Threading.Tasks;
using CarServiceSchedule.Model;

namespace CarServiceSchedule.Repositories
{
    public interface IDemandRepository
    {
        Task<IEnumerable<Demand>> Get();
        Task<Demand> Get(int id);
        Task<Demand> Create(Demand demand);
        Task Update(Demand demand);
        Task Delete(int id);

    }
}