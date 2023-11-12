using BLL.Login;
using BLL.Login.User;
using BLL.Viktoruna;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Users users = new Users();
            Questions questions = new Questions();
            int x;

            while (true)
            {
                Console.WriteLine("0reg,1vhid,2all,3+rezult,4delete,5rezult, 6 quest, 7newfilequest");
                x = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (x)
                {

                    case 0:
                        string str1, str2;
                        Console.WriteLine("Login: ");
                        str1 = Console.ReadLine();
                        Console.WriteLine("password: ");
                        str2 = Console.ReadLine();
                        User_ user = new User_(str1, str2);
                        users.create_user(user);
                        break;
                    case 1:
                        Console.WriteLine("Login: ");
                        str1 = Console.ReadLine();
                        Console.WriteLine("password: ");
                        str2 = Console.ReadLine();
                        if (users.Enter(str1, str2) != null)
                        {
                            Console.WriteLine("Yes");
                        }
                        else
                        {
                            Console.WriteLine("No");
                        }
                        Console.ReadLine();
                        break;
                    case 2:
                        foreach (var item in users.GetUsers())
                        {
                            Console.WriteLine($"{item.Login}  {item.Password}");
                        }
                        break;
                    case 3:
                        //int count = 0;
                        //foreach (var item in users.GetUsers())
                        //{
                        //    Console.WriteLine($"{count++}. {item}");
                        //}
                        //Console.WriteLine("Who: ");
                        //count = Convert.ToInt32(Console.ReadLine());
                        //string str = Console.ReadLine();
                        //users.SPROBAREz(users.get_user(count), str);

                        break;
                    case 4:
                        int pos = Convert.ToInt32(Console.ReadLine());
                        users.delete_user(users.get_user(pos));

                        break;
                    case 5:
                        foreach (var item in users.GetUsers())
                        {
                            Console.WriteLine(item.Login);
                            foreach (var rez in item.rezult)
                            {
                                Console.WriteLine(rez);
                            }
                        }
                        break;
                    case 6:
                        //foreach (var item in questions.Math())
                        //{
                        //    Console.WriteLine(item.Key);
                        //    foreach (var item2 in item.Value)
                        //    {
                        //        Console.WriteLine(item2);
                        //    }

                        //}
                        break;
                    case 7:
                        string strq= Console.ReadLine();
                        questions.create_new_Quest(strq);
                        break;
                    case 8:
                        foreach(var qu in questions.quests)
                        {
                            Console.WriteLine(qu.Name);
                        }
                        break;


                }

            }
        }
    }
}
