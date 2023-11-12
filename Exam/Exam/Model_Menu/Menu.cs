using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model_Menu
{
    public class Menu
    {
        string quest=" ";
        bool work = true;
        int num, max_num;
        char ch;


        Dictionary<int, string> dictionary;


        public Menu()
        {
            //dictionary = new Dictionary<int, string>();


            //for (int i = 0; i < str_button.Length; i++)
            //{
            //    dictionary.Add(i, str_button[i]);
            //}
            //num = 0; max_num = dictionary.Count - 1;
        }
        public Menu(string[] str)
        {
            
            dictionary = new Dictionary<int, string>();
            for (int i = 0; i < str.Length; i++)
            {
                dictionary.Add(i, str[i]);
            }
            num = 0; max_num = dictionary.Count - 1;
        }
        public Menu(List<string> str)
        {
            dictionary = new Dictionary<int, string>();
            quest = str[0];
            for (int i =1; i < str.Count; i++)
            {
                dictionary.Add(i, str[i]);
            }
            num = 0; max_num = dictionary.Count;
        }

        public int open_window()
        {
            int chose;
            while (work)
            {
                Console.Clear();
                print();

                chose = control();
                if (chose != -1)
                    return chose;
            }
            return -1;
        }
        private void print()
        {

            int x = 1;
            foreach (KeyValuePair<int, string> dict in dictionary)
            {
                Console.SetCursorPosition(1, x++);

                if (num == dict.Key) { Console.ForegroundColor = ConsoleColor.DarkCyan; }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine(dict.Value);
            }

            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(quest);

        }
        private int control()
        {

            ch = Console.ReadKey().KeyChar;
           
            if (ch == 13)
            {
                Console.Clear();
                return num;
            }
            if (ch == 'w' || ch == 'W')
            {
                num--;
                if (num < 0)
                {
                    num = max_num;
                }

            }

            if (ch == 's' || ch == 'S')
            {
                num++;
                if (num > max_num) { num = 0; }

            }
            return -1;
        }
      
    }
}
