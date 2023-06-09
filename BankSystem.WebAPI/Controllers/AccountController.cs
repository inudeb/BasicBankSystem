using BankSystem.Application.Interfaces;
using BankSystem.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BankSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        /// Get a list of the accounts filtered by user id
        /// </summary>
        /// <param name="userid">User Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/{userid:int}")]
        [ProducesResponseType(typeof(AccountEntity[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAccounts(int userid)
        {
            return Ok(await _accountService.GetAccountByUser(userid));
        }
        /// <summary>
        /// Create an account for an user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="balance">Account initial balanace</param>
        /// <returns>Created account</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResult<AccountEntity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateAccount([FromBody]CreateAccountRequest request) 
        {
            return Ok(await _accountService.CreateAccount(request.UserId, request.Balance));
        }
        /// <summary>
        /// Execute a witdraw from an account
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="amount">The amount is going to be withdraw</param>
        /// <returns>Affected account</returns>
        [HttpPost]
        [Route("Withdraw")]
        [ProducesResponseType(typeof(ServiceResult<AccountEntity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Withdraw([FromBody]TransactionRequest request)
        {
            return Ok(await _accountService.Withdraw(request.AccountId, request.Amount));
        }
        /// <summary>
        /// Execute a deposit to an account
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="amount">The amount is going to be withdraw</param>
        /// <returns>Affected account</returns>
        [HttpPost]
        [Route("Deposit")]
        [ProducesResponseType(typeof(ServiceResult<AccountEntity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Deposit([FromBody] TransactionRequest request)
        {
            return Ok(await _accountService.Deposit(request.AccountId, request.Amount));
        }
    }
}
