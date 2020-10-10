using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoginApp.Models
{
    public class LoginDetails
    {
        [Key]
        public  int id { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        [NotMapped]
        public string Confirmpassword { set; get; }
}
}
