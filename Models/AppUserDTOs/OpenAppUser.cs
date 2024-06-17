using System.ComponentModel.DataAnnotations;

namespace Bank.Models.AppUserDTOs {
	public class OpenAppUser {
		[Required]
		public string UserName { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;

	}
}
