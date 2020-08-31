using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WssCRM.Register
{
    public class UserFromCLient
    {

        public string UserName { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string token { get; set; }
        //[Required]
        //[Compare("Password", ErrorMessage = "Пароли не совпадают")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Подтвердить пароль")]
        //public string PasswordConfirm { get; set; }
    }
}
