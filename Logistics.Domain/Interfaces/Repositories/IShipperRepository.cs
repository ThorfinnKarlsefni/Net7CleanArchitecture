using System;
using Logistics.Domain.Entities;

namespace Logistics.Domain.Interfaces.Repositories
{
    public interface IShipperRepository
    {
        Shipper? GetByPhone(string phone);
        Task<Shipper> CreateShipper(Shipper shipper);
        Task<List<Shipper>> GetShipperList(string phone);
    }
}

