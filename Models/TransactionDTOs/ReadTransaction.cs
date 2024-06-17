namespace Bank.Models.TransactionDTOs {
	public class ReadTransaction {
		public int Id { get; set; }
		public DateTime DateTime { get; set; } = DateTime.Now;
		public string Description { get; set; } = string.Empty;

		public decimal Amount { get; set; } = decimal.Zero;

		public int? FromAccountId { get; set; }
		public int? ToAccountId { get; set; }
	}
}
