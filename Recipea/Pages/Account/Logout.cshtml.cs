using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Recipea.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public string? ReturnUrl { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Sign out the user
            await _signInManager.SignOutAsync();
            
            // Redirect to index page (user is now logged out)
            return RedirectToPage("/Index");
        }
    }
}

