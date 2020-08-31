using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WssCRM.Register
{
    public class UserForChangeRole
    {

        public string UserName { get; set; }


        public string UserId { get; set; }
        public List<IdentityRole> roles = new List<IdentityRole>();
        public List<IdentityRole> allroles = new List<IdentityRole>();
    }
}
