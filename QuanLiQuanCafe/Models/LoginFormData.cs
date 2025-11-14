using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCafe.Models
{
    public class LoginFormData
    {
        public string Username { get; }
        public string Password { get; }

        public LoginFormData(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
