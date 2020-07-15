using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C.R.E.A.M.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "min = 6 max = 12")]
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid ConfirmationCode { get; set; }
        public bool IsActive { get; set; } = false;

        public List<Role> Roles { get; set; }


    }
}