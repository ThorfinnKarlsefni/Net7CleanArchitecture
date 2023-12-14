using System;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure.Repositories
{
    public class ConsigneeRepository : IConsigneeRepository
    {
        private readonly XhwtDbContext _context;
        public ConsigneeRepository(XhwtDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Consignee> CreateConsignee(Consignee consignee)
        {
            await _context.Consignees.AddAsync(consignee);
            await _context.SaveChangesAsync();
            return consignee;
        }

        public Consignee? GetbyPhone(string phone)
        {
            return _context.Consignees.Where(c => c.Phone == phone).FirstOrDefault();
        }

        public async Task<List<Consignee>> GetConsigneeList(string phone)
        {
            return await _context.Consignees.Where(c => EF.Functions.Like(c.Phone, $"{phone}%")).OrderByDescending(c => c.CreatedAt).Take(10).ToListAsync();
        }
    }
}

