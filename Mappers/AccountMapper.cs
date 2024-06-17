using Bank.Models;
using Bank.Models.AppUserDTOs;

namespace Bank.Mappers {
	public static class AccountMapper {
		public static ReadAppUser ToReadAppUser(this AppUser appUser, string token) {
			return new ReadAppUser {
				UserName = appUser.UserName ?? "",
				Email = appUser.Email ?? "",
				Token = token
			};
		}
	}
}
