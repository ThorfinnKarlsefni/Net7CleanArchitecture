namespace Logistics.Domain.Entities
{
    public class Consignee
    {
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public string Phone { get; private set; }

        public ICollection<Waybill> Waybills { get; private set; } = new List<Waybill>();

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public Consignee(string name, string phone)
        {
            Name = name;
            Phone = phone;
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
        }
    }
}

