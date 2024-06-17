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

			modelBuilder.Entity<Transaction>()
				.HasOne(t => t.FromAccount)
				.WithMany(a => a.Transactions)
				.HasForeignKey(t => t.FromAccountId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Transaction>()
				.HasOne(t => t.ToAccount)
				.WithMany()
				.HasForeignKey(t => t.ToAccountId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Restrict);

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
