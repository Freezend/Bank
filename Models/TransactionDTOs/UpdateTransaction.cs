using System.ComponentModel.DataAnnotations;

namespace Bank.Models.TransactionDTOs {
	public class UpdateTransaction {
		[MaxLength(256, ErrorMessage = "Description cannot be over 256 characters.")]
		public string Description { get; set; } = string.Empty;
	}
}
