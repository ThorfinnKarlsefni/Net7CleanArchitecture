using System;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Infrastructure.Data;

namespace Logistics.Infrastructure.Repositories
{
    public class WaybillRepository : IWaybillRepository
    {
        private readonly XhwtDbContext _context;

        public WaybillRepository(XhwtDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> CreateWaybill(Waybill waybill)
        {
            await _context.Waybills.AddAsync(waybill);
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}

