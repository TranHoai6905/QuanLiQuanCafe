using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCafe.Models
{
    public enum RegisterResult { Success = 1, UsernameExists = -1, PhoneExists = -2, Failed = 0 }
}
