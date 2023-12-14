using System;
using Logistics.Domain.Entities;

namespace Logistics.Domain.Interfaces.Services
{
    public interface IWaybillService
    {
        Task<bool> InvoiceWaybill(Waybill waybill);
    }
}

