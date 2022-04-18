using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SunTestProject.Models
{
    public class RegistrationViewModel
    {
        [Required]
        public string Email { get; set; }
        [RegularExpression(@"^(?=.{8,16}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$", ErrorMessage = "минимальная длина 8 карактеров и максимальная 16 по крайней мере одна цифра, нижний регистр и один верхний регистр")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; }
    }

}
