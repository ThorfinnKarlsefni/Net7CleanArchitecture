using System;
namespace Logistics.Domain.Interfaces.Services
{
    public interface IBankService
    {
        Task<int> CreateBank(string bankName, int bankId);
    }
}

