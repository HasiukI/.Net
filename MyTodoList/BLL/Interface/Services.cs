using BLL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface Services
    {
        void Add_Todo(Todo todo);
        bool Delete_Todo(Todo todo);
        bool Update_Todo(Todo todo);
        List<Todo> Get_all();

        bool Finish_Todo(Todo todo);
    }
}
