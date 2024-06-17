using Bank.Models;

namespace Bank.Interfaces
{
    public interface IBankAccountRepository {
		Task<List<BankAccount>> GetAllAsync();
		Task<BankAccount?> GetByIdAsync(int id);
		Task<BankAccount> CreateAsync(BankAccount bankAccount);
		Task<BankAccount?> UpdateAsync(int id, BankAccount bankAccount);
		Task<BankAccount?> DeleteAsync(int id);
		Task<bool> CheckAsync(int id);
	}
}
