using System.ComponentModel.DataAnnotations;

namespace Backend.Models.LoginRegisters
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Email is required")]
        public String? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public String? Password { get; set; }

        [Required(ErrorMessage = " Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("Password", ErrorMessage = "Paswords doesn't match")]
        public String? ConfirmPassword { get; set; }

        [Required(ErrorMessage = " Ime is required")]
        public String? Ime { get; set; }

        [Required(ErrorMessage = " Prezime is required")]
        public String? Prezime { get; set; }

        [Required(ErrorMessage = " Adresa is required")]
        public String? Adresa { get; set; }

        [Required(ErrorMessage = " Grad is required")]
        public String? Grad { get; set; }

        [Required(ErrorMessage = " Telefon is required")]
        public String? Telefon { get; set; }
    }
}