using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SummerFreshFlowers.Models
{
    public class SendGridCredential
    {
        [Key]//setting up a 1-1 relationship using UserId as primarykey & foreignkey
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}