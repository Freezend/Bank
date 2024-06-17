using Bank.Data;
using Bank.Interfaces;
using Bank.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Bank.Repository
{
    public class BankAccountRepository : IBankAccountRepository {
		private readonly ApplicationDBContext _dbContext;
		public BankAccountRepository(ApplicationDBContext dbContext) {
			_dbContext = dbContext;
		}
		public async Task<List<BankAccount>> GetAllAsync() {
			return await _dbContext.BankAccounts.Include(x => x.Transactions).ToListAsync();
		}
		public async Task<BankAccount?> GetByIdAsync(int id) {
			return await _dbContext.BankAccounts.Include(x => x.Transactions).FirstOrDefaultAsync(x => x.Id == id);
		}
		public async Task<BankAccount> CreateAsync(BankAccount bankAccount) {
			await _dbContext.BankAccounts.AddAsync(bankAccount);
			await _dbContext.SaveChangesAsync();
			return bankAccount;
		}
		public async Task<BankAccount?> UpdateAsync(int id, BankAccount newBankAccount) {
			var bankAccount = await _dbContext.BankAccounts.FindAsync(id);
			if (bankAccount == null)
				return null;

			bankAccount.Name = newBankAccount.Name;

			await _dbContext.SaveChangesAsync();
			return bankAccount;
		}
		public async Task<BankAccount?> DeleteAsync(int id) {
			var bankAccount = await _dbContext.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);
			if (bankAccount == null)
				return null;

			var transactionsFromAccount = await _dbContext.Transactions.Where(x => x.FromAccountId == id).ToListAsync();
			var transactionsToAccount = await _dbContext.Transactions.Where(x => x.ToAccountId == id).ToListAsync();
			foreach (var t in transactionsFromAccount)
				t.FromAccountId = null;
			foreach (var t in transactionsToAccount)
				t.ToAccountId = null;

			_dbContext.BankAccounts.Remove(bankAccount);
			await _dbContext.SaveChangesAsync();
			return bankAccount;
		}
		public async Task<bool> CheckAsync(int id) {
			return await _dbContext.BankAccounts.AnyAsync(x => x.Id == id);
		}
	}
}
