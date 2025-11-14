using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCafe.Models
{
    public class RegisterFormData
    {
        public string FullName { get; }
        public string Password { get; }
        public string ConfirmPassword { get; }
        public string Phone { get; }
        public string Address { get; }
        public DateTime DateOfBirth { get; }
        public string Role { get; }

        public RegisterFormData(string fullName, string password, string confirmPassword,
            string phone, string address, DateTime dateOfBirth, string role)
        {
            FullName = fullName;
            Password = password;
            ConfirmPassword = confirmPassword;
            Phone = phone;
            Address = address;
            DateOfBirth = dateOfBirth;
            Role = role;
        }
    }
}
