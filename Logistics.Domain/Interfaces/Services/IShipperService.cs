using System;
using Logistics.Domain.Entities;

namespace Logistics.Domain.Interfaces.Services
{
    public interface IShipperService
    {
        Task<int> CreateShipper(string name, string phone, string? idCard);
    }
}
