using System.Collections.Generic;
using System.Threading.Tasks;
using CarServiceSchedule.Model;
using Microsoft.EntityFrameworkCore;

namespace CarServiceSchedule.Repositories
{
    public class BookingRepository:IBookingRepository
    {

          private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> Create(Booking demand)
        {
            _context.Bookings.Add(demand);
            await _context.SaveChangesAsync();

            return demand;
        }

        public async Task Delete(int id)
        {
            var demandToDelete = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(demandToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> Get()
        {
            return await _context.Bookings.ToListAsync();

        }

        public async Task<Booking> Get(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task Update(Booking booking)
        {
            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        
    }
}