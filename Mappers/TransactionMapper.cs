using Bank.Models;
using Bank.Models.TransactionDTOs;

namespace Bank.Mappers {
	public static class TransactionMapper {
		public static ReadTransaction ToReadTransaction(this Transaction transaction) {
			return new ReadTransaction {
				Id = transaction.Id,
				DateTime = transaction.DateTime,
				Description = transaction.Description,
				Amount = transaction.Amount,
				FromAccountId = transaction.FromAccountId,
				ToAccountId = transaction.ToAccountId,
			};
		}
		public static Transaction FromCreateTransaction(this CreateTransaction createTransaction) {
			return new Transaction {
				Description = createTransaction.Description,
				Amount = createTransaction.Amount,
				FromAccountId = createTransaction.FromAccountId,
				ToAccountId = createTransaction.ToAccountId,
			};
		}
		public static Transaction FromUpdateTransaction(this UpdateTransaction updateTransaction) {
			return new Transaction {
				Description = updateTransaction.Description,
			};
		}
	}
}
