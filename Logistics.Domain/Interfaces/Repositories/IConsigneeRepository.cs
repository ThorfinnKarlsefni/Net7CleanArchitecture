using System;
using Logistics.Domain.Entities;

namespace Logistics.Domain.Interfaces.Repositories
{
    public interface IConsigneeRepository
    {
        Consignee? GetbyPhone(string phone);
        Task<Consignee> CreateConsignee(Consignee consignee);
        Task<List<Consignee>> GetConsigneeList(string phone);
    }
}
