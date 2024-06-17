using Bank.Models;

namespace Bank.Interfaces {
	public interface ITokenService {
		string CreateToken(AppUser appUser);
	}
}
