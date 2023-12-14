using System;
using Logistics.Domain.Entities;

namespace Logistics.Domain.Interfaces.Repositories
{
    public interface IBankRepository
    {
        Bank? GetById(int bankId);
        Task<Bank> CreateBank(Bank bank);
    }
}

