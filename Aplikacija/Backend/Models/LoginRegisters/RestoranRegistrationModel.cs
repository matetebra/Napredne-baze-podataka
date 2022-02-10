using System.ComponentModel.DataAnnotations;

namespace Backend.Models.LoginRegisters
{
    public class RestoranRegistrationModel
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

        [Required(ErrorMessage = " Naziv is required")]
        public String? Naziv { get; set; }

        [Required(ErrorMessage = " Adresa is required")]
        public String? Adresa { get; set; }

        [Required(ErrorMessage = " Grad is required")]
        public String? Grad { get; set; }

        [Required(ErrorMessage = " Telefon is required")]
        public String? Telefon { get; set; }

        [Required(ErrorMessage = " Opis is required")]
        public String? Opis { get; set; }

        [Required(ErrorMessage = " RadnoVreme is required")]
        public int? pocetakRV { get; set; }
        [Required(ErrorMessage = " RadnoVreme is required")]
        public int? krajRV { get; set; }

        [Required(ErrorMessage = " VremeDostave is required")]
        public String? VremeDostave { get; set; }

        [Required(ErrorMessage = " CenaDostave is required")]
        public int CenaDostave { get; set; }

        [Required(ErrorMessage = " LimitDostave is required")]
        public int LimitDostave { get; set; }

        [Required(ErrorMessage = " Kapacitet is required")]
        public int Kapacitet { get; set; }
    }
}