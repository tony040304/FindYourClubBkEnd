using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ChangePasswordViewModel
    {
        public string Password { get; set; } = string.Empty;
        public string CheckPassword { get; set; }
    }
}
