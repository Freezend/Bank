using Bank.Models;

namespace Bank.Interfaces {
	public interface ITokenService {
		Task<string> CreateTokenAsync(AppUser appUser);
	}
}
