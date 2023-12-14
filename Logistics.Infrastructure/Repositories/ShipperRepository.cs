using System;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly XhwtDbContext _context;

        public ShipperRepository(XhwtDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Shipper> CreateShipper(Shipper shipper)
        {
            await _context.Shippers.AddAsync(shipper);
            await _context.SaveChangesAsync();
            return shipper;
        }

        public Shipper? GetByPhone(string phone)
        {
            return _context.Shippers.Where(s => s.Phone == phone).FirstOrDefault();
        }

        public async Task<List<Shipper>> GetShipperList(string phone)
        {
            return await _context.Shippers.Where(s => EF.Functions.Like(s.Phone, $"{phone}%")).OrderByDescending(s => s.CreatedAt).Take(10).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}

