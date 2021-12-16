using System.Collections.Generic;
using System.Threading.Tasks;
using CarServiceSchedule.Model;
using Microsoft.EntityFrameworkCore;

namespace CarServiceSchedule.Repositories
{
    public class DemandRepository:IDemandRepository
    {

          private readonly AppDbContext _context;

        public DemandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Demand> Create(Demand demand)
        {
            _context.Demands.Add(demand);
            await _context.SaveChangesAsync();

            return demand;
        }

        public async Task Delete(int id)
        {
            var demandToDelete = await _context.Demands.FindAsync(id);
            _context.Demands.Remove(demandToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Demand>> Get()
        {
            return await _context.Demands.ToListAsync();

        }

        public async Task<Demand> Get(int id)
        {
            return await _context.Demands.FindAsync(id);
        }

        public async Task Update(Demand demand)
        {
            _context.Entry(demand).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        
    }
}