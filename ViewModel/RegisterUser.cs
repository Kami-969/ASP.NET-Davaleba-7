using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C.R.E.A.M.ViewModel
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "გთხოვთ შეავსოთ მომხმარებელი")]
        [StringLength(12, MinimumLength = 4, ErrorMessage = "min = 4 max = 12")]
        public string Username { get; set; }

        [Required(ErrorMessage = "გთხოვთ შეავსოთ პაროლი")]
        [DataType(DataType.Password)]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "min = 6 max = 12")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "პაროლები არ ემთხვევა")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "გთხოვთ შეავსოთ ელ.ფოსტა")]
        [DataType(DataType.EmailAddress, ErrorMessage = "გთხოვთ შეიყვანოთ რეალური ელ.ფოსტა")]
        public string Email { get; set; }

    }
}