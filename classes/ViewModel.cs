using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace test1.classes
{
    class ViewModel : INotifyPropertyChanged
    {
        Model model = new Model();
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public string Terminal { get; set; }
        public bool Button_0_vis { get; set; } = true;
        public bool Button_1_vis { get; set; } = true;
        public bool Button_2_vis { get; set; } = true;
        
        public object locker = new object();
        public int Progress { get; set; }
        public int Maximum { get; set; } = 0;
        public Command Button_0 //процедура выполнения комманды по кнопке
        {
            get
            {
                return new Command(async o => {
                    try
                    {
                        maximum_plus();
                        NotifyPropertyChanged("Maximum");
                        Button_0_vis = false;
                        NotifyPropertyChanged("Button_0_vis");
                        ret(await model.RetStr(ret, progress_inc)); //вывод в окно возвращаемого значения вызываемого асинхронного метода с передачей ему методов вывода в окно и инкремента прогрессбара
                        Button_0_vis = true;
                        NotifyPropertyChanged("Button_0_vis");
                        maximum_minus();
                    }
                    catch (AggregateException e)
                    {
                        ret(e.Message);
                    }
                    
                });
            }
        }
        public Command Button_1
        {
            get
            {
                return new Command(async o =>
                {
                    try
                    {
                        maximum_plus();
                        NotifyPropertyChanged("Maximum");
                        Button_1_vis = false;
                        NotifyPropertyChanged("Button_1_vis");
                        ret(await model.RetInt(ret, progress_inc));
                        Button_1_vis = true;
                        NotifyPropertyChanged("Button_1_vis");
                        maximum_minus();
                    }
                    catch (AggregateException e)
                    {
                        ret(e.Message);
                    }
                });
            }
        }
        public Command Button_2
        {
            get
            {
                return new Command(async o =>
                {
                    try
                    {
                        maximum_plus();
                        Button_2_vis = false;
                        NotifyPropertyChanged("Button_2_vis");
                        ret(await model.RetChar(ret, progress_inc));
                        Button_2_vis = true;
                        NotifyPropertyChanged("Button_2_vis");
                        maximum_minus();
                    }
                    catch (AggregateException e)
                    {
                        ret(e.Message);
                    }
                });
            }
        }
        void maximum_minus() //метод уменьшения интервала прогрессбара после выполнения комманды
        {
            lock(locker)
            {
                Progress -= 1000;
                Maximum -= 1000;
                NotifyPropertyChanged("Maximum");
            }
        }
        void maximum_plus() //метод увеличения интервала прогрессбара перед выполнением комманды
        {
            lock(locker)
            {
                Maximum += 1000;
                NotifyPropertyChanged("Maximum");
            }
        }
        void progress_inc() // метод инкремента значения прогрессбара
        {
            lock (locker)
            {
                Progress++;
                NotifyPropertyChanged("Progress");
            }
        }
        void ret(object message) //метод вывода в textbox
        {
            Terminal += message.ToString() + "\n";
            NotifyPropertyChanged("Terminal");
        }
    }
    class Command : ICommand
    {
        public Action<object> execute;

        public event EventHandler CanExecuteChanged;

        public Command(Action<object> execute)
        {
            this.execute = execute;
        }

    public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

    }
}
