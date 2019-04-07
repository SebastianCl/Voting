namespace Voting.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class AccountController : Controller
    {
        private readonly IUserHelper userHelper;

        public AccountController(IUserHelper userHelper)
        {
            this.userHelper = userHelper;
        }

        public IActionResult Login()
        {
            //user es un atributo de la clase controller
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }                    
                    return this.RedirectToAction("Index", "Home"); //controlador login a controlador home
                }
            }
            this.ModelState.AddModelError(string.Empty, "Failed to login.");
            return this.View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await this.userHelper.LogoutAsync();
            return this.RedirectToAction("Index", "Home");
        }
    }
}
