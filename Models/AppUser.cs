using Microsoft.AspNetCore.Identity;

namespace Bank.Models {
	public class AppUser : IdentityUser {
		public List<BankAccount> BankAccounts { get; set; } = [];
	}
}
