using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Login.User
{
    public class User_
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public List<string> rezult { get; set; }
        public User_(string login, string password)
        {
            Login = login;
            Password = password;
            rezult = new List<string>();
        }
        public override string ToString()
        {
            return $"{Login} {Password}";
        }
    }
}
