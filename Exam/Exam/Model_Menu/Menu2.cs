using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Exam.Model_Menu
{
    public class Menu2
    {
        string quest = " ";
        string true_quest;
        bool work = true;
        int num, max_num;
        bool[] who_start;
        char ch;



        Dictionary<int, string> dictionary;
        //System.Media.SoundPlayer button; 

        public Menu2()
        {
           
        }
        public Menu2(List<string> str, string tq)
        {
            // button = new System.Media.SoundPlayer(@"button_press.wav");
            true_quest = tq;
            dictionary = new Dictionary<int, string>();
            quest = str[0];
            for (int i = 1; i < str.Count; i++)
            {
                dictionary.Add(i-1, str[i]);
            }
            dictionary.Add(5, "Вiдповiсти");
            Console.WriteLine(dictionary.Count);
            num = 0; max_num = dictionary.Count;
            who_start = new bool[dictionary.Count];
            for(int i=0; i<who_start.Length; i++)
            {
                who_start[i] = false;
            }
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
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(quest+".(На це питання декiлька вiдповiдей.)");


                if (num == dict.Key) { Console.ForegroundColor = ConsoleColor.DarkCyan; }
                else if (who_start[dict.Key])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                
                Console.SetCursorPosition(0, x++);
                Console.WriteLine(dict.Value);
            }


        }
        private int control()
        {

            ch = Console.ReadKey().KeyChar;
            if (ch == 13)
            {

                int x= start(num);
                if (x != -1)
                {
                    return x;
                }

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

        private int start(int index)
        {
            if (index == max_num-1)
            {

                bool ok=true;
                string rez = true_quest;
                for(int i = 0; i < dictionary.Count; i++)
                {
                    if (who_start[i])
                    {
                    
                        if (rez.IndexOf((i+1).ToString()) == -1) { ok = false;break; }
                    }
                   
                }

                if (ok) return 1;
                else  return -100;

               
            }
            else
            {
                who_start[index] = true;
            }
            return -1;

        }

       
    }
}
