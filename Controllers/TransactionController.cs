﻿using Bank.Interfaces;
using Bank.Mappers;
using Bank.Models;
using Bank.Models.TransactionDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers {
	[Route("api/transaction")]
	[ApiController]
	public class TransactionController : ControllerBase {
		private readonly IBankAccountRepository _bankAccountRepository;
		private readonly ITransactionRepository _transactionRepository;
		public TransactionController(IBankAccountRepository bankAccountRepository, ITransactionRepository transactionRepository) {
			_bankAccountRepository = bankAccountRepository;
			_transactionRepository = transactionRepository;
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAll() {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var transactions = await _transactionRepository.GetAllAsync();
			var readTransactions = transactions.Select(x => x.ToReadTransaction());

			return Ok(readTransactions);
		}

		[HttpGet("{id:int}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetById([FromRoute] int id) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var transaction = await _transactionRepository.GetByIdAsync(id);
			if (transaction == null)
				return NotFound();

			var readTransaction = transaction.ToReadTransaction();
			return Ok(readTransaction);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create([FromBody] CreateTransaction createTransaction) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!await _bankAccountRepository.CheckAsync(createTransaction.FromBankAccountId ?? 0))
				return BadRequest("First BankAccount does not exist.");

			if (!await _bankAccountRepository.CheckAsync(createTransaction.ToBankAccountId ?? 0))
				return BadRequest("Second BankAccount does not exist.");

			if (createTransaction.FromBankAccountId == createTransaction.ToBankAccountId)
				return BadRequest("BankAccounts are the same.");

			var transaction = createTransaction.FromCreateTransaction();
			await _transactionRepository.CreateAsync(transaction);

			return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction.ToReadTransaction());
		}

		[HttpPut("{id:int}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTransaction updateTransaction) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var transaction = await _transactionRepository.UpdateAsync(id, updateTransaction.FromUpdateTransaction());

			if (transaction == null)
				return NotFound();

			var readTransaction = transaction.ToReadTransaction();
			return Ok(readTransaction);
		}

		[HttpDelete("{id:int}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete([FromRoute] int id) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var transaction = await _transactionRepository.DeleteAsync(id);
			if (transaction == null)
				return NotFound();

			var readTransaction = transaction.ToReadTransaction();
			return Ok(readTransaction);
		}
	}
}
