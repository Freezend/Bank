using Bank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser> {
		public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {
		
		}

		public DbSet<BankAccount> BankAccounts { get; set; }
		public DbSet<Transaction> Transactions { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<BankAccount>()
				.HasMany(b => b.TransactionsFrom)
				.WithOne(t => t.FromBankAccount)
				.HasForeignKey(t => t.FromBankAccountId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<BankAccount>()
				.HasMany(b => b.TransactionsTo)
				.WithOne(t => t.ToBankAccount)
				.HasForeignKey(t => t.ToBankAccountId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<AppUser>()
				.HasMany(u => u.BankAccounts)
				.WithOne(b => b.AppUser)
				.HasForeignKey(b => b.AppUserId)
				.OnDelete(DeleteBehavior.Cascade);

			List<IdentityRole> roles = [
				new IdentityRole {
					Name = "Admin",
					NormalizedName = "ADMIN"
				},
				new IdentityRole {
					Name = "User",
					NormalizedName = "USER"
				},
			];

			modelBuilder.Entity<IdentityRole>().HasData(roles);
		}
	}
}
