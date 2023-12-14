using System;
using System.Security.Claims;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.WebApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    public class ShipperController : Controller
    {
        private readonly IShipperRepository _shipperRepository;
        public ShipperController(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        [HttpPost("ShipperList")]
        public async Task<IActionResult> ShipperList(string phone)
        {
            if (!Guid.TryParse(this.User.FindFirstValue((ClaimTypes.NameIdentifier)), out Guid userId))
                throw new AggregateException("请重新登录");
            var shippers = await _shipperRepository.GetShipperList(phone);
            List<ShipperList> shipperList = new List<ShipperList>();
            foreach (var shipper in shippers)
            {
                shipperList.Add(ShipperDto.CreateShipperList(shipper.Name, shipper.Phone));
            }
            return Ok(shipperList);

        }
    }
}

