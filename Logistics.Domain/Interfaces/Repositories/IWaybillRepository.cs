using System;
using Logistics.Domain.Entities;

namespace Logistics.Domain.Interfaces.Repositories
{
    public interface IWaybillRepository
    {
        Task<bool> CreateWaybill(Waybill waybill);
    }
}

