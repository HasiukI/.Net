using BLL.Login;
using BLL.Login.User;
using BLL.Viktoruna;
using Exam.Model_Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Exam
{
   
    public delegate int MenuDel();
    public delegate void MenuChoseDel(int x);

    public class StartProject
    {
        bool work;
        int menu_count;
        Questions quests;
        Users users;
        User_ this_user;
        string[] login = { "Зайти в аккаунт", "Зареєструватись", "Вийти" };
        string[] adm = { "Показати усiх користувачiв i iх результати","Видалити користувача","Видалити досягнення", "Показати всi завдання","Змiнити котресь питання","Добавити вiкторину","Добавити питання до вiкторини", "Вийти" };
        string[] vikto = { "Стартувати вiкторину", "Переглянути результат минулих вiкторин", "Переглянути Топ-20 з конкретної вiкторини", "Змiнити пароль", "Вийти" };

        Menu menu_login,menu_viktor,menu_admin, menu_ch_vikt;
        public StartProject() {
            users=new Users();
            quests=new Questions();

            menu_login= new Menu(login);
            menu_viktor= new Menu(vikto);
            menu_admin = new Menu(adm);
            menu_ch_vikt = new Menu(quests.get_AllTheme());



            MenuEvent += menu_login.open_window;//m-0
            MenuChoseEvent += login_menu;

            MenuEvent += menu_viktor.open_window;//m-1
            MenuChoseEvent += user_menu;

            MenuEvent += menu_admin.open_window;//m-2
            MenuChoseEvent += admin_menu;

            MenuEvent += menu_ch_vikt.open_window;//m-3
            MenuChoseEvent += start_questmenu;

            menu_count = 0; work = true;
            to20();
        }

        public event MenuDel MenuEvent;
        public event MenuChoseDel MenuChoseEvent;


        public void startEvent()
        {
           
            int x = 0;
            while (work)
            {
                var tempwrite = MenuEvent.GetInvocationList();
                x = (tempwrite[menu_count] as MenuDel).Invoke();

                var tempChose = MenuChoseEvent.GetInvocationList();
                (tempChose[menu_count] as MenuChoseDel).Invoke(x);
            }
           
        }
        private void login_menu(int x)
        {
            string strlog, strpass;
            switch (x)
            {
                case 0:
                    Console.Write("Ведiть логiн: ");
                    strlog = Console.ReadLine();
                    Console.Write("Ведiть пароль: ");
                    strpass = Console.ReadLine();

                    this_user = users.Enter(strlog, strpass);
                    if (strlog == "admin" && strpass == "admin") {  menu_count = 2; }

                    if (this_user != null)
                    {
                        Console.WriteLine($"Привiт {strlog}");
                        menu_count=1;

                    }
                    else
                    {
                       if(menu_count!=2) Console.WriteLine("Помилка автентифiкацiї.");
                        else Console.WriteLine("Здарова");
                    }
                    System.Threading.Thread.Sleep(1500);
                    break;
                case 1:
                    Console.WriteLine("Пароль i логiн мають бути вiд 6 до 16 символiв. Дозволено використовувати букви цифри i знаки <_,->");
                    Console.WriteLine("Login: ");
                    strlog = Console.ReadLine();
                    Console.WriteLine("password: ");
                    strpass = Console.ReadLine();
                    User_ user = new User_(strlog, strpass);
                    if (users.create_user(user))
                    {
                        Console.WriteLine("Ваш акаунт створено.");
                    }
                    else
                    {
                        Console.WriteLine("Помилка при створеннi аккаунта. Попробуйте ще раз");
                    }
                    System.Threading.Thread.Sleep(1500);
                    break;
                case 2:
                    work = false;
                    break;
            }
        }
        private void user_menu(int x)
        {
            string strpass;
            switch (x)
            {
                case 0:
                    menu_count = 3;             
                    break;
                case 1:
                    foreach(string str in this_user.rezult)
                    {
                        Console.WriteLine(str);
                    }
                    Console.WriteLine("Для продовження натиснiсть будь що");
                    Console.ReadLine();
                    break;
                case 2:
                    string[] txt = quests.get_AllTheme();
                    Menu mmm=new Menu(txt);
                    int mm = mmm.open_window();
                    foreach(var str in quests.rec)
                    {
                        if (str.Name == txt[mm])
                        {
                            foreach(string el in str.Records) {
                                Console.WriteLine(el);
                            }
                            break;
                        }
                    }
                    Console.WriteLine("Для продовження натиснiсть будь що");
                    Console.ReadLine();
                    break;
                case 3:
                    Console.Write("Ведіть новий пароль: ");
                    strpass= Console.ReadLine();
                    if(users.update_user(this_user, strpass))
                    {
                        Console.WriteLine("Ваш пароль змінено");
                    }
                    else
                    {
                        Console.WriteLine("Помилка ведення нового пароля");
                    }
                    System.Threading.Thread.Sleep(1500);
                    break;
                case 4:
                    work= false;
                    break;
            }
        }
        private void admin_menu(int x)
        {
            switch (x)
            {
                case 0:
                    foreach(User_ us in users.GetUsers())
                    {
                        Console.WriteLine($"\t{us}");
                        foreach (string rez in us.rezult)
                        {
                            Console.WriteLine(rez);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("Для продовження натиснiсть будь що");
                    Console.ReadLine();
                    break;
                case 1:
                    int count = 1;
                    foreach (User_ us in users.GetUsers())
                    {
                        Console.WriteLine($"{count++}. {us.Login}");
                    }
                    Console.WriteLine("Кого видалити?");
                    count=Convert.ToInt32(Console.ReadLine());
                    count--;
                    users.delete_user(users.get_user(count));
                    break;
                case 2:
                    int count_1= 1,count_2;
                    foreach (User_ us in users.GetUsers())
                    {
                        count_2 = 1;
                        Console.WriteLine($"\t{count_1++}. {us.Login}");
                        foreach(string str in us.rezult)
                        {
                            Console.WriteLine($"{count_2++}. {str}");
                        }
                       
                    }

                    Console.Write("Кому видалити(Ведiть номер користувача): ");
                    count_1 = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Що видалити(Ведiть номер результату): ");
                    count_2 = Convert.ToInt32(Console.ReadLine());

                    users.deleteRezult(users.get_user(count_1-1), users.get_user(count_1-1).rezult[count_2-1]);
                    break;
                case 3:
                    
                    foreach(string th in quests.get_AllTheme())
                    {
                        Console.WriteLine(th);
                        foreach(var it in quests.startExam(th))
                        {
                            Console.WriteLine($"\t{it.Key}");
                            foreach(string val in it.Value) 
                                Console.WriteLine($"\t\t{val}");
                        }
                    }

                    Console.WriteLine("Для продовження натиснiсть будь що");
                    Console.ReadLine();
                    break;
                case 4:
                    foreach (string th in quests.get_AllTheme())
                    {
                        Console.WriteLine(th);
                       
                    }

                    Console.Write("Ведiть назву вiкторину у якiй потрiбно замiнити питання: ");
                    string name=Console.ReadLine();

                    List<string> list=new List<string>();
                    int con = 0, counter=0;
                    foreach (string th in quests.get_AllTheme())
                    {
                        if (name == th)
                        {
                            foreach (var it in quests.startExam(th))
                            {
                                con = 0;
                                //list.Add(it.Key);
                                Console.WriteLine($"\t{counter++}.Питання: {it.Key}");
                                foreach (string val in it.Value)
                                {
                                    //list.Add(val);
                                    if(con==0)Console.WriteLine($"\t\tВiдповiдь: {val}");
                                    else Console.WriteLine($"\t\t\t{val}");
                                    con++;
                                }
                               
                                   
                            }
                        }
                    }
                    Console.WriteLine("У якому питанні вихочете щось змінити: ");
                    int num_pos = Convert.ToInt32(Console.ReadLine());

                    con = 0;
                    bool worked = true;
                    foreach (string th in quests.get_AllTheme())
                    {
                        if (name == th)
                        {
                            
                            foreach (var it in quests.startExam(th))
                            {
                                if(con == num_pos)
                                {
                                    list.Add(it.Key);

                                    foreach (string val in it.Value)
                                    {
                                        list.Add(val);
                                       
                                    }
                                    worked = false; 
                                }
                                con++;

                                 
                                if (!worked) { break; }

                            }
                        }
                        if (!worked) { break; }
                    }

                    Menu m = new Menu(list);
                    int num=m.open_window();

                    Console.Write("Ведiть нове речення: ");
                    string newstr = Console.ReadLine();
                    if (!String.IsNullOrEmpty(newstr))
                    {
                        list[num] = newstr;
                    }

                    quests.delete_Quest(name, list);
                   
                    break;
                case 5:
                    Console.WriteLine("Ведiть назву вiкторини: ");
                    quests.create_new_Quest(Console.ReadLine());
                    break;
                case 6:
                    Console.Write("Виберiть куди добавим питання: ");
                    System.Threading.Thread.Sleep(1000);
                    string[] name_leng = quests.get_AllTheme();
                    Menu menuu= new Menu(name_leng);
                    int number=menuu.open_window();


                    List<string> l =new List<string>();
                    Console.WriteLine("Ведiть запитання");
                    string answer=Console.ReadLine();
                    l.Add(answer.ToString());
                    for(int i = 0; i < 6; i++)
                    {
                        if (i == 0) Console.WriteLine("Ведiть правильний номер(Якщо напишете двох-трьохзначнi числа питання буде з декiлькома відповiдями <123>): ");
                        else Console.WriteLine($"Ведiть {i} варiант вiдповідi");
                        l.Add(Console.ReadLine());
                    }

                    quests.addAnswer(name_leng[number], l);
                    
                    break;
                case 7:
                    work = false;
                    break;
            }
        }
        private void start_questmenu(int x)
        {
            string name = quests.quests[x].Name;
            Dictionary<string, List<string>> keys = quests.startExam(name);
            int rezult = 0;
            List<string> temp = new List<string>();
            int count = 0;
            foreach (var item in keys)
            {
                count = 0;
                if (item.Value[0].Length > 1)
                {
                    temp.Add(item.Key);
                    foreach (string sttemp in item.Value)
                    {
                        if (count != 0) temp.Add(sttemp);
                        count++;
                    }
                    Menu2 me2= new Menu2(temp, item.Value[0]);
                    if (me2.open_window() == 1) { rezult++; }
                }
                else
                {
                    temp.Add(item.Key);
                    foreach(string sttemp in item.Value)
                    {
                        if(count!=0)temp.Add(sttemp);
                        count++;
                    }
                    Menu st= new Menu(temp);
                    int chose=st.open_window();
                    if (chose == Convert.ToInt32(item.Value[0])) { rezult++; }
                }
                temp.Clear();
              
            }
            Console.WriteLine($"Ваш Результат:{rezult}/{keys.Count}. {DateTime.Now} ");
            System.Threading.Thread.Sleep(1500);
            users.addRezult(this_user, $"{name}. Результат: {rezult}/{keys.Count}. {DateTime.Now}");
            foreach(var el in quests.rec)
            {
                if(el.Name== name)
                {
                    el.to_Record($"{name}. Результат: {rezult}/{keys.Count}. {DateTime.Now}", this_user);
                }
            }

            menu_count = 1;
        }
       
       private void to20()
        {
            foreach(User_ us in users.GetUsers())
            {
                List<string> li = users.getinfo(us);
                foreach(string st in li)
                {
                    string[] strrez = st.Split(' ');
                    strrez[0]=strrez[0].Remove(strrez[0].Length - 1);

                    foreach (var el in quests.rec)
                    {
                        if (el.Name == strrez[0])
                        {
                            el.to_Record(st, us);
                            break;
                        }
                    }
                }

            }
        }
    }
}
