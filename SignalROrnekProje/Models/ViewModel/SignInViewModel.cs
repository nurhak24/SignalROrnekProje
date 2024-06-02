using System.ComponentModel.DataAnnotations;

namespace SignalROrnekProje.Models.ViewModel
{
    public record SignInViewModel([Required] string Email, [Required] string Password);
    
}
