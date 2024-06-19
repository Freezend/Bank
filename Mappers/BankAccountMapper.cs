using Bank.Models;
using Bank.Models.BankAccountDTOs;

namespace Bank.Mappers
{
	public static class BankAccountMapper {
		public static ReadBankAccount ToReadBankAccount(this BankAccount bankAccount) {
			return new ReadBankAccount {
				Id = bankAccount.Id,
				Name = bankAccount.Name,
				Number = bankAccount.Number,
				Balance = bankAccount.Balance,
				AppUserId = bankAccount.AppUserId,
				ReadTransactionsFrom = bankAccount.TransactionsFrom.Select(x => x.ToReadTransaction()).ToList(),
				ReadTransactionsTo = bankAccount.TransactionsTo.Select(x => x.ToReadTransaction()).ToList(),
			};
		}
		public static BankAccount FromCreateBankAccount(this CreateBankAccount createBankAccount) {
			return new BankAccount {
				Name = createBankAccount.Name,
				Number = createBankAccount.Number,
				AppUserId = createBankAccount.AppUserId,
			};
		}
		public static BankAccount FromUpdateBankAccount(this UpdateBankAccount updateBankAccount) {
			return new BankAccount {
				Name = updateBankAccount.Name,
				Number = updateBankAccount.Number,
			};
		}
	}
}
