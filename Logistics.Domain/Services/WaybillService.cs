using System;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;

namespace Logistics.Domain.Services
{
    public class WaybillService : IWaybillService
    {
        private readonly IWaybillRepository _waybillRepository;

        public WaybillService(IWaybillRepository waybillRepository)
        {
            _waybillRepository = waybillRepository;
        }
        public async Task<bool> InvoiceWaybill(Waybill waybill)
        {
            return await _waybillRepository.CreateWaybill(waybill);
        }
    }
}

