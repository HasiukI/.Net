using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safe_and_Read
{
    public class Cesar
    {
        string rezult;
        int temp_num;
        public int Num { get; set; }
        public Cesar() { Num = 3; }
        public string toCesar(string write)
        {
            rezult = "";
            for (int i = 0; i < write.Length; i++)
            {
               
                if (Convert.ToChar(write[i] + Num) >= 'A' && Convert.ToChar(write[i] + Num) <= 'Z' || Convert.ToChar(write[i] + Num) >= 'a' && Convert.ToChar(write[i] + Num) <= 'z')
                    rezult += Convert.ToChar(write[i] + Num);
                else if (write[i] >= 'a' && write[i] <= 'z')
                {
                    if (Convert.ToChar(write[i] + Num) > 'z')
                    {
                        temp_num = Num - ('z' - write[i]);
                        rezult += Convert.ToChar('a' - 1 + temp_num);
                    }
                }else if (write[i] >= 'A' && write[i] <= 'Z')
                {
                    if (Convert.ToChar(write[i] + Num) > 'Z')
                    {
                        temp_num = Num - ('Z' - write[i]);
                        rezult += Convert.ToChar('A' - 1 + temp_num);
                    }
                }
                else
                {
                    rezult += Convert.ToChar(write[i]);
                }
            }

            return rezult;
        }
        public string toAdekvatno(string write)
        {
            rezult = "";
            for (int i = 0; i < write.Length; i++)
            {
             
                if (Convert.ToChar(write[i] - Num) >= 'A' && Convert.ToChar(write[i] - Num) <= 'Z' || Convert.ToChar(write[i] - Num) >= 'a' && Convert.ToChar(write[i] - Num) <= 'z')
                    rezult += Convert.ToChar(write[i] - Num);
                else if (write[i] >= 'a' && write[i] <= 'z')
                {
                    if (Convert.ToChar(write[i] - Num) < 'a')
                    {
                        temp_num = Num + ('a' - write[i]);
                        rezult += Convert.ToChar('z' + 1 - temp_num);
                    }
                }else if (write[i] >= 'A' && write[i] <= 'Z')
                {
                    if (Convert.ToChar(write[i] - Num) < 'A')
                    {
                        temp_num = Num + ('A' - write[i]);
                        rezult += Convert.ToChar('Z' + 1 - temp_num);
                    }
                }
                else
                {
                    rezult += Convert.ToChar(write[i]);
                }
            }
            return rezult;
        }
    }
}
