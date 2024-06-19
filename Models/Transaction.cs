using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Models
{
    public class Transaction {
		public int Id { get; set; }
		public DateTime DateTime { get; set; } = DateTime.Now;
		public string Description { get; set; } = string.Empty;

		[Column(TypeName = "decimal(18,2)")]
		public decimal Amount { get; set; } = decimal.Zero;

		public int? FromBankAccountId { get; set; }
		public BankAccount? FromBankAccount { get; set; }
		public int? ToBankAccountId { get; set; }
		public BankAccount? ToBankAccount { get; set; }
	}
}
