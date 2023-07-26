using Microsoft.AspNetCore.Mvc;
using WAB.BLL.Services.Abstract;

namespace WAB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly TransactionBaseService _transactionService;

    public TransactionController(TransactionBaseService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactions()
    {
        var transactions = await _transactionService.GetTransactions();
        return Ok(transactions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionById(int id)
    {
        var transaction = await _transactionService.GetTransactionById(id);
        return Ok(transaction);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetTransactionsByUserId(int userId)
    {
        var transactions = await _transactionService.GetTransactionsByUserId(userId);
        return Ok(transactions);
    }

    [HttpGet("{n}/user/{userId}")]
    public async Task<IActionResult> GetTransactionsByUserId(int userId, int n)
    {
        var transactions = await _transactionService.GetLastNTransactionsByUserId(userId, n);
        return Ok(transactions);
    }
}