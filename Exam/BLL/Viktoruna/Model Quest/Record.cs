using BLL.Login.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Viktoruna.Model_Quest
{
    public class Record
    {
        public string Name { get; set; }

        public string[] Records { get; set; }

        public Record() { 
            Records= new string[20];
        }

        public void to_Record(string record, User_ us)
        {
            string[] sp= record.Split(' ');
            string[] rez = sp[2].Split('/');
            string[] rez1, sp1;

            for (int i=0;i<Records.Length;i++)
            {   
                if(Records[i] != null)
                {
                    sp1 = Records[i].Split(' ');
                    rez1 = sp1[3].Split('/');
                    if (Records[i] == null || Convert.ToInt32(rez[0]) >= Convert.ToInt32(rez1[0]))
                    {
                        for (int j = 19; j > i; j--)
                        {
                            Records[j] = Records[j - 1];
                        }
                        Records[i] = $"{us.Login}. {record}";
                        break;
                    }
                }
                Records[i] = $"{us.Login}. {record}";
                break;
            }
        }
    }
}
