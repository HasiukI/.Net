using BLL.Logic;
using BLL.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MyTodoList.ViewModel
{
    internal class Bridge : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void onPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
        
        public Bridge() {
            tasks_bll = new TodoList();

        }


        private TodoList tasks_bll;
        public List<Todo> TaskList { get => new List<Todo>(tasks_bll.Get_all());}


        private string _task;
        public string Task
        {
            get => _task; 
            set { 
                 onPropertyChanged(nameof(Task));
                _task = value;
            }
        }


        public ICommand StartCreateTask
        {
            get =>  new RelayCommand(() => CreateTask());
        }

      
        private void CreateTask()
        {
             try
            {
                if (_task == null)
                    throw new Exception("Не найдено завдання");
                if (_task.Length <= 3)
                    throw new Exception("Замалий формат завдання");
                if (_task.Length >= 500)
                    throw new Exception("Перевищено ліміт завдання");

                tasks_bll.Add_Todo(new BLL.Model.Todo { DateTime_Create = DateTime.Now, IsCompleted = false, IsImportant = false, Todo_str = _task });
                Task = "";
                onPropertyChanged(nameof(TaskList));
                onPropertyChanged(nameof(Task));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
