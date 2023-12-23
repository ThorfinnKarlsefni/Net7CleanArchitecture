using Logistics.Domain.Entities.Identity;

namespace Logistics.Domain.Entities
{
    public class Waybill
    {
        public Guid WaybillId { get; private set; }
        // user info
        public int ShipperId { get; set; }
        public Shipper Shipper { get; } = null!;
        public int ConsigneeId { get; set; }
        public Consignee Consignee { get; } = null!;

        //station
        public int ToStation { get; set; }
        public int? TransitStation { get; set; }
        public string Address { get; set; } = null!;

        // cargo
        public string CargoName { get; set; } = null!;
        public string? CargoId { get; set; }
        public int Quantity { get; set; }
        public float? Volume { get; set; }
        public float? Weight { get; set; }
        public int? Packing { get; set; }
        public int? CargoValue { get; set; }
        public decimal? InsuranceFee { get; set; }

        //Fee 
        public decimal FreightFee { get; set; }
        public decimal? CodcFee { get; set; }
        public decimal? BackFreightFee { get; set; }
        public decimal? DeliveryFee { get; set; }
        public decimal? NotificationFee { get; set; }

        public string? Remarks { get; set; }

        //bank
        public int? BankId { get; set; }
        public Bank? Bank { get; set; }

        // Creator
        public Guid? CreatorId { get; set; }
        public User? Creator { get; }
        public Guid? BargainerId { get; set; }
        public User? Bargainer { get; } = null!;

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime? PayAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public Waybill()
        {
            WaybillId = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
        }
    }
}
