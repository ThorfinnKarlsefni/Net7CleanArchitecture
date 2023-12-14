using System;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;

namespace Logistics.Domain.Services
{
    public class ConsigneeService : IConsigneeService
    {
        private readonly IConsigneeRepository _repository;

        public ConsigneeService(IConsigneeRepository consigneeRepository)
        {
            _repository = consigneeRepository;
        }

        public async Task<int> CreateConsignee(string name, string phone)
        {
            var existingConsignee = _repository.GetbyPhone(phone);
            if (existingConsignee != null)
                return existingConsignee.Id;
            var newConsignee = new Consignee(name, phone);
            var consignee = await _repository.CreateConsignee(newConsignee);
            return consignee.Id;
        }
    }
}

