using Bank.Models.TransactionDTOs;

namespace Bank.Models.BankAccountDTOs {
	public class ReadBankAccount {
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Number { get; set; } = string.Empty;

		public decimal Balance { get; set; } = decimal.Zero;

		public List<ReadTransaction> Transactions { get; set; } = [];
	}
}
