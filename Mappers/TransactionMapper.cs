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
				FromBankAccountId = transaction.FromBankAccountId,
				ToBankAccountId = transaction.ToBankAccountId,
			};
		}
		public static Transaction FromCreateTransaction(this CreateTransaction createTransaction) {
			return new Transaction {
				Description = createTransaction.Description,
				Amount = createTransaction.Amount,
				FromBankAccountId = createTransaction.FromBankAccountId,
				ToBankAccountId = createTransaction.ToBankAccountId,
			};
		}
		public static Transaction FromUpdateTransaction(this UpdateTransaction updateTransaction) {
			return new Transaction {
				Description = updateTransaction.Description,
			};
		}
	}
}
