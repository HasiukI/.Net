using Safe_and_Read;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Viktoruna.Model_Quest
{
    public class Quest
    {
        public string Name { get; }
        string shlyah;
        Read_Write rw;
        public Dictionary<string, List<string>> quest { get; set; }
        public Quest(string name)
        {
            Name = name;
            shlyah = $"Quest/{Name}.txt";

            quest = new Dictionary<string, List<string>>();
            rw = new Read_Write();
            
            quest= rw.Read_Quest(shlyah);
        }

       

    }
}
