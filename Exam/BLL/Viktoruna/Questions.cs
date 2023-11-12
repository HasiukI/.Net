using BLL.Viktoruna.Model_Quest;
using Safe_and_Read;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Viktoruna
{
    class Randomaizer
    {
        static Random random = new Random();
        public static int GetRand(int x)
        {
            return random.Next(0, x);
        }
    }
    public class Questions
    {
        public List<Quest> quests { get; }
        public List<Record> rec { get; }
        Read_Write rw;

        public Questions() {
           quests = new List<Quest>();
           rw= new Read_Write();

           List<string> list= new List<string>();
           list = rw.Read_user("All_Theme.txt", Users_Data.Data);
           rec= new List<Record>();

            foreach(string item in list)
            {  
                Record record = new Record() { Name=item};
                rec.Add(record);
                quests.Add(new Quest(item));
            }
            
        }
        
        public Dictionary<string,List<string>> startExam(string name) {
            if (name == "Random")
            {
                Dictionary<string,List<string>> rand= new Dictionary<string,List<string>>();
                for(int i = 0; i < 5; i++)
                {
                   
                    foreach (Quest item in quests)
                    {
                        if(item.Name == "Random") { break; }
                        string path = $"Quest/{item.Name}.txt";
                        item.quest = rw.Read_Quest(path);
                        int x = item.quest.Count, count = 0;
                        int rnd = Randomaizer.GetRand(x);

                        foreach (var it in item.quest)
                        {

                            if (count == rnd)
                            {
                                string answer = it.Key;
                                List<string> strings = it.Value;
                                if (strings != null)
                                {
                                    bool ook = true;
                                    foreach(var r in rand)
                                    {
                                        if (answer == r.Key) ook = false;
                                    }
                                    if(ook)
                                    rand.Add(answer, strings);
                                    break;
                                }
                            }
                            count++;
                        }

                    }
                   
                }
                return rand;

            }
            foreach (Quest item in quests)
            {
                if (item.Name == name)
                {
                    string path = $"Quest/{item.Name}.txt";
                    item.quest=rw.Read_Quest(path);
                    return item.quest;
                }
            }
            return null;
        }

        public void create_new_Quest(string str) {
            string rez = $"Quest/{str}.txt";
            rw.Create_User(rez);
            rw.Write_User("All_Theme.txt", str, Users_Data.Data);
        }
        public void delete_Quest(string str,List<string> li)
        {
            string rez = $"Quest/{str}.txt";
            rw.Delete_User(rez);

            rw.Delete_User("All_Theme.txt");

            foreach(var item in quests)
            {
                rw.Write_User("All_Theme.txt", item.Name, Users_Data.Data);
            }

            foreach(string it in li)
            {
                rw.Write_User(rez,it,Users_Data.Data);
                
            }

        }
        public void addAnswer(string pat,List<string> li)
        {
            string path = $"Quest/{pat}.txt";
            foreach(string str in li)
            {
                rw.Write_User(path, str, Users_Data.Data);
            }
        }
        public string[] get_AllTheme()
        {
            return rw.Read_user("All_Theme.txt", Users_Data.Data).ToArray();
        }

    }
}
