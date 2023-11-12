using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class Todo
    {
        public string Todo_str { get; set; }
        public DateTime DateTime_Create { get; set; }
        public DateTime? DateTime_End { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsImportant { get; set; }
    }
}
