using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Recipea.Pages.Auth
{
    public class LoginModel : PageModel
    {
        [BindProperty] public string Username { get; set; } = "";
        [BindProperty] public string Password { get; set; } = "";

        public async Task<IActionResult> OnPostAsync()
        {
            // Mock login
            var claims = new List<Claim> { new(ClaimTypes.Name, Username) };
            var identity = new ClaimsIdentity(claims, "FakeCookieAuth");
            await HttpContext.SignInAsync("FakeCookieAuth", new ClaimsPrincipal(identity));
            return RedirectToPage("/Recipes/Index");
        }
    }
}
