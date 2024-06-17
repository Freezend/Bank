using Bank.Models;

namespace Bank.Interfaces {
	public interface ITransactionRepository {
		Task<List<Transaction>> GetAllAsync();
		Task<Transaction?> GetByIdAsync(int id);
		Task<Transaction> CreateAsync(Transaction transaction);
		Task<Transaction?> UpdateAsync(int id, Transaction transaction);
		Task<Transaction?> DeleteAsync(int id);
	}
}
