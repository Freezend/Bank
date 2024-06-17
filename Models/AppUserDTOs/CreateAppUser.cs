using System.ComponentModel.DataAnnotations;

namespace Bank.Models.AppUserDTOs {
	public class CreateAppUser {
		[Required]
		public string? UserName { get; set; }
		[Required]
		[EmailAddress]
		public string? Email { get; set; }
		[Required]
		public string? Password { get; set; }
	}
}
