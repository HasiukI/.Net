using BLL.Interface;
using BLL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Logic
{
    public class TodoList : Services
    {
        private List<Todo> todos;
        public TodoList() { 
            todos = new List<Todo>();
            todos.Add(new Todo() { Todo_str="Task 1", DateTime_Create=DateTime.Now});
            todos.Add(new Todo() { Todo_str="Task 2", DateTime_Create=DateTime.Now});
            todos.Add(new Todo() { Todo_str="Task 3", DateTime_Create=DateTime.Now});
        }

        public void Add_Todo(Todo todo)
        {
            if (todo != null)
            {
              todos.Add(todo);
            }     
        }

        public bool Delete_Todo(Todo todo)
        {
            if (todo != null)
            {
                todos.Remove(todo);
                return true;
            }
            return false;
        }

        public bool Finish_Todo(Todo todo)
        {
            if (todo != null)
            {
                todo.DateTime_End = DateTime.Now;
                todo.IsCompleted = true;
                return true;
            }
            return false;
        }

        public  List<Todo> Get_all()
        {
            return todos;
        }

        public bool Update_Todo(Todo todo)
        {
            throw new NotImplementedException();
        }
    }
}
