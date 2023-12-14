using System;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;

namespace Logistics.Domain.Services
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _repository;

        public BankService(IBankRepository bankRepository)
        {
            _repository = bankRepository;
        }
        public async Task<int> CreateBank(string bankName, int bankId)
        {
            var existingBank = _repository.GetById(bankId);
            if (existingBank != null)
                return existingBank.Id;
            var newBank = new Bank(bankName, bankId);
            var bank = await _repository.CreateBank(newBank);
            return bank.Id;
        }
    }
}

