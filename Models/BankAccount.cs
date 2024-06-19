using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Models {
	public class BankAccount {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;

		[Column(TypeName = "decimal(18,2)")]
		public decimal Balance { get; set; } = decimal.Zero;

		public string? AppUserId { get; set; }
		public AppUser? AppUser { get; set; }
        public List<Transaction> TransactionsFrom { get; set; } = [];
		public List<Transaction> TransactionsTo { get; set; } = [];
    }
}
