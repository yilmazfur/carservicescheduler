using System.Collections.Generic;
using System.Threading.Tasks;
using CarServiceSchedule.Model;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarServiceSchedule.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Car> Create(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task Delete(int id)
        {
            var bookToDelete = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(bookToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Car>> Get()
        {
            return await _context.Cars.ToListAsync();

        }

        public async Task<Car> Get(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task Update(Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}