using System.ComponentModel.DataAnnotations;

namespace SunTestProject.Models
{
    public class AuthorizationViewModel
    {
        [Required(ErrorMessage = "Не указан login")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}
