using System;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Infrastructure.Data;

namespace Logistics.Infrastructure.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly XhwtDbContext _context;

        public BankRepository(XhwtDbContext dbContext)
        {
            _context = dbContext;
        }

        public Bank? GetById(int bankId)
        {
            return _context.Banks.Where(b => b.BankId == bankId).FirstOrDefault();
        }

        public async Task<Bank> CreateBank(Bank bank)
        {
            await _context.Banks.AddAsync(bank);
            await _context.SaveChangesAsync();
            return bank;
        }
    }
}

