using System;
namespace GECF.Models
{
    public class LoginModel
    {
        public LoginModel(string userName, string password)
        {
            username = userName;
            this.password = password;
        }

        public string username { get; set; }
        public object password { get; set; }
    }
}

