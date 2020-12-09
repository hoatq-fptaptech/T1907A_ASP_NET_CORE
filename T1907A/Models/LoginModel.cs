using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T1907A.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Pwd { get; set; }
    }

    public class LoginResult
    {
        public string Token { get; set; }
        public User User { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class ChangePwd
    {
        public string Current { get; set; }
        public string NewPwd { get; set; }
        public string ConfirmPwd { get; set; }
    }
}
