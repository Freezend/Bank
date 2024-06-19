using Bank.Models.BankAccountDTOs;

namespace Bank.Models.AppUserDTOs {
	public class ReadAppUser {
		public string UserName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;
		public List<ReadBankAccount> BankAccounts { get; set; } = [];
	}
}
