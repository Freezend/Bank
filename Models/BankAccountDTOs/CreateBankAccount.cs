using Bank.Models.TransactionDTOs;
using System.ComponentModel.DataAnnotations;

namespace Bank.Models.BankAccountDTOs {
	public class CreateBankAccount {
		[Required]
		[MinLength(5, ErrorMessage = "Name must be 5 characters.")]
		[MaxLength(32, ErrorMessage = "Name cannot be over 32 characters.")]
		public string Name { get; set; } = string.Empty;
		[Required]
		[MinLength(15, ErrorMessage = "Number must be 15 characters.")]
		[MaxLength(34, ErrorMessage = "Number cannot be over 34 characters.")]
		public string Number { get; set; } = string.Empty;
	}
}
