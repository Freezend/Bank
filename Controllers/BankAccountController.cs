using Bank.Interfaces;
using Bank.Mappers;
using Bank.Models;
using Bank.Models.BankAccountDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bank.Controllers {

	[Route("api/bankAccount")]
	[ApiController]
	public class BankAccountController : ControllerBase {
		private readonly UserManager<AppUser> _userManager;
		private readonly IBankAccountRepository _bankAccountRepository;
        public BankAccountController(UserManager<AppUser> userManager, IBankAccountRepository bankAccountRepository) {
            _userManager = userManager;
			_bankAccountRepository = bankAccountRepository;
        }
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAll() {
			var bankAccounts = await _bankAccountRepository.GetAllAsync();
			var readBankAccounts = bankAccounts.Select(x => x.ToReadBankAccount()).ToList();
			return Ok(readBankAccounts);
		}
		[HttpGet("{id:int}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetById([FromRoute] int id) {
			var bankAccount = await _bankAccountRepository.GetByIdAsync(id);
			if (bankAccount == null)
				return NotFound();

			var readBankAccount = bankAccount.ToReadBankAccount();
			return Ok(readBankAccount);
		}
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create(CreateBankAccount createBankAccount) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == createBankAccount.AppUserId);
			if (appUser == null)
				return NotFound("User doesn't exist.");

			var bankAccount = createBankAccount.FromCreateBankAccount();
			await _bankAccountRepository.CreateAsync(bankAccount);
			return CreatedAtAction(nameof(GetById), new { id = bankAccount.Id }, bankAccount.ToReadBankAccount());
		}
		[HttpPut("{id:int}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBankAccount updateBankAccount) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var bankAccount = await _bankAccountRepository.UpdateAsync(id, updateBankAccount.FromUpdateBankAccount());
			if (bankAccount == null)
				return NotFound();

			return Ok(bankAccount.ToReadBankAccount());
		}
		[HttpDelete("{id:int}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete([FromRoute] int id) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var bankAccount = await _bankAccountRepository.DeleteAsync(id);
			if (bankAccount == null)
				return NotFound();

			return Ok(bankAccount.ToReadBankAccount());
		}
    }
}
