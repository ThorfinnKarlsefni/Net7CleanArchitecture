using System;
namespace Logistics.WebApi.Dto
{
    public record ShipperList(string ShipperName, string ShipperPhone);
    public class ShipperDto
    {
        public static ShipperList CreateShipperList(string name, string phone)
        {
            return new ShipperList(name, phone);
        }
    }
}

