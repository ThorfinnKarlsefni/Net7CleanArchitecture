using System;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;

namespace Logistics.Domain.Services
{
    public class ShipperService : IShipperService
    {
        private readonly IShipperRepository _repository;

        public ShipperService(IShipperRepository shipperRepository)
        {
            _repository = shipperRepository;
        }

        public async Task<int> CreateShipper(string name, string phone, string? idCard)
        {
            var existingShipper = _repository.GetByPhone(phone);
            if (existingShipper != null)
                return existingShipper.Id;

            var newShipper = new Shipper(name, phone, idCard);
            var shipper = await _repository.CreateShipper(newShipper);
            return shipper.Id;
        }
    }
}

