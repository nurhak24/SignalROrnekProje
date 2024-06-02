using System.ComponentModel.DataAnnotations;

namespace SignalROrnekProje.Models.ViewModel
{
    public record SignUpViewModel([Required]string Email,[Required]string Password,[Required]string ConfirmPassword);
   
}
