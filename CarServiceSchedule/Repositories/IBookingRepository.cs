using System.Collections.Generic;
using System.Threading.Tasks;
using CarServiceSchedule.Model;

namespace CarServiceSchedule.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> Get();
        Task<Booking> Get(int id);
        Task<Booking> Create(Booking user);
        Task Update(Booking user);
        Task Delete(int id);

    }
}