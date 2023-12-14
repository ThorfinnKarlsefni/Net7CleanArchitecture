using System;
using Logistics.Domain.Entities;

namespace Logistics.Domain.Interfaces.Services
{
    public interface IConsigneeService
    {
        Task<int> CreateConsignee(string name, string phone);
    }
}

