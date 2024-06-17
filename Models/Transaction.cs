using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Models
{
    public class Transaction {
		public int Id { get; set; }
		public DateTime DateTime { get; set; } = DateTime.Now;
		public string Description { get; set; } = string.Empty;

		[Column(TypeName = "decimal(18,2)")]
		public decimal Amount { get; set; } = decimal.Zero;

		public int? FromAccountId { get; set; }
		public BankAccount? FromAccount { get; set; }
		public int? ToAccountId { get; set; }
		public BankAccount? ToAccount { get; set; }
	}
}
