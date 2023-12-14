using System.Security.Claims;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Services;
using Logistics.Infrastructure.Data;
using Logistics.WebApi.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logistics.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    // [Authorize]
    public class InvoiceController : Controller
    {
        private readonly IBankService _bankService;
        private readonly IConsigneeService _consigneeService;
        private readonly IShipperService _shipperService;
        private readonly IWaybillService _waybillService;
        private readonly XhwtDbContext _context;

        public InvoiceController(IBankService bankService, IConsigneeService consigneeService, IShipperService shipperService, IWaybillService waybillService, XhwtDbContext dbContext)
        {
            _bankService = bankService;
            _consigneeService = consigneeService;
            _shipperService = shipperService;
            _waybillService = waybillService;
            _context = dbContext;
        }

        /// <summary>
        /// artificial invoice
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Invoices([FromBody] InvoiceRequest req)
        {
            if (!Guid.TryParse(this.User.FindFirstValue((ClaimTypes.NameIdentifier)), out Guid userId))
                throw new AggregateException("请重新登录");

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var shipperId = await _shipperService.CreateShipper(req.ShipperName, req.ShipperPhone, req.IdCard);
                var consigneeId = await _consigneeService.CreateConsignee(req.ConsigneeName, req.ConsigneePhone);
                //var bankId = await _bankService.CreateBank(req.BankId);
                var waybill = new Waybill
                {
                    ShipperId = shipperId,
                    ConsigneeId = consigneeId,
                    //BankId = bankId,
                    ToStation = req.ToStation,
                    TransitStation = req.TransitStation,
                    Address = req.Address,
                    //CargoId = userId,
                    CargoName = req.CargoName,
                    CargoId = req.CargoId,
                    CargoValue = req.CargoValue,
                    Quantity = req.Quantity,
                    Volume = req.Volume,
                    Weight = req.Weight,
                    Packing = req.Packing,
                    InsuranceFee = req.InsuranceFee,
                    //Fee
                    FreightFee = req.FreightFee,
                    BackFreightFee = req.BackFreightFee,
                    DeliveryFee = req.DeliveryFee,
                    NotificationFee = req.NotificationFee,

                    Remarks = req.Remarks,
                };

                await _waybillService.InvoiceWaybill(waybill);
                transaction.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}

