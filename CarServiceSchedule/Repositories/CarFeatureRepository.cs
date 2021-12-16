using System.Collections.Generic;
using System.Threading.Tasks;
using CarServiceSchedule.Model;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarServiceSchedule.Repositories
{
    public class CarFeatureRepository : ICarFeatureRepository
    {
        private readonly AppDbContext _context;

        public CarFeatureRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CarFeature> Create(CarFeature car)
        {
            _context.CarFeatures.Add(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task Delete(int id)
        {
            var bookToDelete = await _context.CarFeatures.FindAsync(id);
            _context.CarFeatures.Remove(bookToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarFeature>> Get()
        {
            return await _context.CarFeatures.ToListAsync();

        }

        public async Task<CarFeature> Get(int id)
        {
            return await _context.CarFeatures.FindAsync(id);
        }

        public async Task Update(CarFeature car)
        {
            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}