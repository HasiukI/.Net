using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Sys_homework_1.ViewModel
{
    public class ViewModelcs : INotifyPropertyChanged
    {
        private TimerCallback timerCallback;
        private Timer timer;
        public ViewModelcs()
        {
            Processes = new ObservableCollection<Process>(Process.GetProcesses());

            timerCallback = new TimerCallback(MyTimer);
            timer = new Timer(timerCallback);
            timer.Change(10000, 1000);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void onPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
      
       
        private void MyTimer(Object obj)
        {
           
           Processes= new ObservableCollection<Process>(Process.GetProcesses());
        }

     

        public ObservableCollection<Process> Processes { get; set; }
        private Process _process;
      
 
        public int ProccesCount {
            get
            {
                if (_process != null)
                 return Processes.Where(p => p.Id == _process.Id).Count();
                return 0;
            }
            set=>onPropertyChanged(nameof(ProccesCount)); 
        }
        public Process CurentProcces
        {
            get => _process;
            set
            {
                _process = value;
                onPropertyChanged(nameof(CurentProcces));
            }
        }

        public ICommand KillProcces
        {
            get => new RelayCommand(() => _process.Kill());
        }
        public ICommand KillTimer
        {
            get => new RelayCommand(() => timer.Dispose());
          
        }
    }
}
