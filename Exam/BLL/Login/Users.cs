using BLL.Login.Interface;
using BLL.Login.User;
using Safe_and_Read;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Login
{
    public class Users : ILogin
    {
        List<User_> users;
        Read_Write rw;
        public Users() 
        {
            users = new List<User_>();
            rw = new Read_Write();

            List<string> strings = rw.Read_user("Users.txt", Users_Data.Users);
            if(strings!= null)
            {
                foreach (string str in strings)
                {
                    string[] name_last = str.Split(' ');
                    User_ user = new User_(name_last[0], name_last[1]);
                    users.Add(user);
                }

                for (int i = 0; i < users.Count; i++)
                {
                    strings.Clear();
                    strings = rw.Read_user($@"User_Rezult\{users[i].Login}.txt", Users_Data.Data);
                    foreach (string str in strings)
                    {
                        users[i].rezult.Add(str);
                    }
                }
            }
           

        }
        
        public void addRezult(User_ us,string str)
        {
            rw.Write_User($"User_Rezult/{us.Login}.txt", str, Users_Data.Data);
            us.rezult.Add(str); 
        }
        public void deleteRezult(User_ us,string del)
        {
            string path = $"User_Rezult/{us.Login}.txt";
            rw.Delete_User(path);
            us.rezult.Remove(del);
            
            foreach(string el in us.rezult)
            {
                rw.Write_User(path,el,Users_Data.Data);
            }
            
        }
        public User_ Enter(string username, string password)
        {
            foreach(User_ user in users)
            {
                if(user.Login==username && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }
       
        public List<User_> GetUsers()
        {
            return users;
        }

        public bool create_user(User_ user)
        {
            if (!is_Regex(user)) { return false; }

            bool is_ok = true;
            List<string> list = rw.Read_user("Users.txt", Users_Data.Users);
            if (list != null)
            {
                foreach (string str in list)
                {
                    string[] strsp = str.Split(' ');
                    if (user.Login == strsp[0]) { return false; }
                }
            }
            rw.Write_User("Users.txt", user.ToString(),Users_Data.Users);
            rw.Create_User($"User_Rezult/{user.Login}.txt");
            users.Add(user);
            return true;
        }


        public void delete_user(User_ user)
        {
            users.Remove(user);
            rw.Delete_User($"User_Rezult/{user.Login}.txt");

            rw.Delete_User("Users.txt");

            foreach (User_ us in users)
            {
                rw.Write_User("Users.txt", us.ToString(), Users_Data.Users);   
            }
        }

        public User_ get_user(int x)
        {
            if(x>users.Count || x < 0) { return users[0]; }
            return users[x]; 
        }


        public bool update_user(User_ user,string newpass)
        {
            user.Password = newpass;
            if (!is_Regex(user)) { return false; }
            rw.Delete_User("Users.txt");

            foreach (User_ us in users)
            {
                rw.Write_User("Users.txt", us.ToString(), Users_Data.Users);
            }
            return true;
        }

        private bool is_Regex(User_ user)
        {
            if (!Regex.IsMatch(user.Login, "^[a-z0-9_-]{3,16}$")) { return false; }
            if (!Regex.IsMatch(user.Password, "^[a-z0-9_-]{6,16}$")) { return false; }
            return true;
        }

        public List<string> getinfo(User_ user)
        {
            return rw.Read_user($"User_Rezult/{user.Login}.txt", Users_Data.Data);
        }
    }
}
