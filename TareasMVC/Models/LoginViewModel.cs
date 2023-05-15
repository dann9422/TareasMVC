using System.ComponentModel.DataAnnotations;

namespace TareasMVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="El campo{0} es requerido")]
        [EmailAddress(ErrorMessage ="El campo de correo debe de tener un formato valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo{0} es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Recuerdame")]
        public bool Recuerdame { get; set; }

    }
}
