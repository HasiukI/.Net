using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Login.User;

namespace BLL.Login.Interface
{
    interface ILogin
    {
        bool create_user(User_ acc);
        bool update_user(User_ acc, string newpass);
        void delete_user(User_ acc);
        User_ get_user(int x);
    }
}
