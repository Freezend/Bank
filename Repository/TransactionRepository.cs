using Bank.Data;
using Bank.Interfaces;
using Bank.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Bank.Repository {
	public class TransactionRepository : ITransactionRepository {
		private readonly ApplicationDBContext _dbContext;
		public TransactionRepository(ApplicationDBContext dbContext) {
			_dbContext = dbContext;
		}

		public async Task<List<Transaction>> GetAllAsync() {
			var transactions = _dbContext.Transactions.ToListAsync();
			return await transactions;
		}

		public async Task<Transaction?> GetByIdAsync(int id) {
			var transactions = await _dbContext.Transactions.SingleOrDefaultAsync(x => x.Id == id);
			return transactions;
		}

		public async Task<Transaction> CreateAsync(Transaction transaction) {
			await _dbContext.Transactions.AddAsync(transaction);
			await _dbContext.SaveChangesAsync();
			return transaction;
		}

		public async Task<Transaction?> UpdateAsync(int id, Transaction newTransaction) {
			var transaction = await _dbContext.Transactions.SingleOrDefaultAsync(x => x.Id == id);
			if (transaction == null)
				return null;

			transaction.Description = newTransaction.Description;

			await _dbContext.SaveChangesAsync();
			return transaction;
		}

		public async Task<Transaction?> DeleteAsync(int id) {
			var transaction = await _dbContext.Transactions.FirstOrDefaultAsync(x => x.Id == id);
			if (transaction == null)
				return null;

			_dbContext.Transactions.Remove(transaction);
			await _dbContext.SaveChangesAsync();
			return transaction;
		}
	}
}
