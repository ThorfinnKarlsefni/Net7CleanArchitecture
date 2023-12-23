namespace Logistics.Domain.Entities
{
    public class Shipper
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string? IdCard { get; private set; }

        public ICollection<Waybill> Waybills { get; private set; } = new List<Waybill>();

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public Shipper(string name, string phone, string? idCard)
        {
            Name = name;
            Phone = phone;
            IdCard = idCard;
            CreatedAt = DateTime.Now;
        }
    }
}

