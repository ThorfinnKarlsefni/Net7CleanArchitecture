namespace Logistics.Domain.Entities
{
    public class Bank
    {
        public int Id { get; private set; }
        public string BankName { get; private set; }
        public int BankId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public ICollection<Waybill> Waybills { get; } = new List<Waybill>();

        public Bank(string bankName, int bankId)
        {
            BankName = bankName;
            BankId = bankId;
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
        }
    }
}

