using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OdontoApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Usuario { get; set; }

        [BindProperty]
        public string Clave { get; set; }

        public IActionResult OnPost()
        {
            // ⚠️ Aquí podrías agregar validación real más adelante

            if (Usuario == "admin" && Clave == "123") // Solo ejemplo
            {
                return RedirectToPage("/Dashboard/Index");
            }

            TempData["Error"] = "Usuario o contraseña incorrectos";
            return Page();
        }
    }
}
