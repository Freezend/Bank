using System.ComponentModel.DataAnnotations;

namespace Bank.Models.TransactionDTOs {
	public class CreateTransaction {
		[MaxLength(256, ErrorMessage = "Description cannot be over 256 characters.")]
		public string Description { get; set; } = string.Empty;

		[Required]
		[Range(0.01, 1000000000.00)]
		public decimal Amount { get; set; } = decimal.Zero;

		public int? FromAccountId { get; set; }
		public int? ToAccountId { get; set; }
	}
}
