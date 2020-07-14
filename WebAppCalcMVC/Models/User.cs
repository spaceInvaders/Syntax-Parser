using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCalcMVC.Models
{
    public class User : IdentityUser
    {
        public List<Saving> Savings { get; set; }
        public User()
        {
            Savings = new List<Saving>();
        }
    }
}
