using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Safe_and_Read
{
    public class Read_Write
    {
        Cesar cesar;
        public Read_Write()
        {
           cesar = new Cesar();
        }
        public List<string> Read_user(string path, Users_Data users_Data)
        {

            if (File.Exists(path))
            {
                List<string> strings = new List<string>();
                using (FileStream fl = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fl, Encoding.Unicode))
                    {
                        while (sr.Peek() > 0)
                        {
                            if(users_Data==Users_Data.Users)
                              strings.Add(cesar.toAdekvatno(sr.ReadLine()));
                            if (users_Data == Users_Data.Data)
                            {
                                strings = File.ReadAllLines(path).ToList<string>();
                                return strings;
                            }
                              
                               
                            
                             
                        }
                        return strings;
                    }
                }
               
            }
            return null;
        }
        public void Write_User(string path, string rez, Users_Data users_Data)
        {
            if (File.Exists(path))
            {
                using (FileStream fl = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sr = new StreamWriter(fl, Encoding.Unicode))
                    {
                        if(users_Data==Users_Data.Users)
                          sr.WriteLine(cesar.toCesar(rez));
                        else
                            sr.WriteLine(rez);
                    }
                }
            }
            else
            {
                using (FileStream fl = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sr = new StreamWriter(fl, Encoding.Unicode))
                    {
                        if (users_Data == Users_Data.Users)
                            sr.WriteLine(cesar.toCesar(rez));
                        else
                            sr.WriteLine(rez);
                    }
                }
            }

        }
        public void Create_User(string path)
        {
           
            File.Create(path);
        }
        public void Delete_User(string path)
        {
            File.Delete(path);
        }
        public Dictionary<string, List<string>> Read_Quest(string path) {


            if (File.Exists(path))
            {
                Dictionary<string, List<string>> key = new Dictionary<string, List<string>>();
                string[] str=File.ReadAllLines(path);

                for(int i=0;i<str.Length;i+=7)
                {
                    
                    List<string> list = new List<string>();
                    list.Add(str[i + 1]);
                    list.Add(str[i + 2]);
                    list.Add(str[i + 3]);
                    list.Add(str[i + 4]);
                    list.Add(str[i + 5]);
                    list.Add(str[i + 6]);
                    key.Add(str[i], list);
                    
                    
                }
                return key;
            }   
            return null;
        }

        

    }
    public enum Users_Data
    {
        Users,
        Data,
    }
}
