using BankSystem.Domain;
using BankSystem.WebApp.Models;
using BankSystem.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;

namespace BankSystem.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountHttpService _accountHttpService;
        public int UserId
        {
            get
            {
                return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            }
        }

        public HomeController(ILogger<HomeController> logger, IAccountHttpService accountHttpService)
        {
            _logger = logger;
            _accountHttpService = accountHttpService;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel model = new HomeViewModel();
            try
            {
                model.AccountOwner = User.Identity.Name;
                model.Accounts = await _accountHttpService.GetUserAccounts(UserId);
                if (TempData["Message"] != null) {
                    ViewBag.Message = JsonConvert.DeserializeObject < MessageViewModel>( TempData["Message"].ToString());
                }
            }
            catch (Exception)
            {

            }
            return View(model);
        }
        public async Task<IActionResult> Transaction(string transactionType, int accountId, decimal amount) 
        {
            try
            {
                ServiceResult<AccountEntity> result = null;
                if(transactionType == "Withdraw")
                    result= await _accountHttpService.Withdraw(accountId, amount);
                if (transactionType == "Deposit")
                    result = await _accountHttpService.Deposit(accountId, amount);
                if (result != null) {
                    if (result.IsSuccess)
                        TempData["Message"] = JsonConvert.SerializeObject(new MessageViewModel { MessageType = "success", Message = result.Message });
                    else
                        TempData["Message"] = JsonConvert.SerializeObject(new MessageViewModel { MessageType = "danger", Message = result.Message });
                }
            }
            catch (Exception)
            {


            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CreateAccount(decimal amount) 
        {
            try
            {
                var result = await _accountHttpService.CreateAccount(UserId, amount);
                if (result != null)
                {
                    if (result.IsSuccess)
                        TempData["Message"] = JsonConvert.SerializeObject( new MessageViewModel { MessageType = "success", Message = result.Message });
                    else
                        TempData["Message"] = JsonConvert.SerializeObject(new MessageViewModel { MessageType = "danger", Message = result.Message });
                }
            }
            catch (Exception)
            {

               
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}