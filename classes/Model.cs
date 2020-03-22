using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace test1.classes
{
    
    class Model
    {
        const int cnt = 2000000000;
        public async Task<string> RetStr(Action<object> action, Action progress) //метод принимающий метод вывода в окно и метод инкремента прогрессбара
        {
            string str = "started";
            return await Task.Run(() =>         //тело метода выполняющее обработку
            {
                int i = cnt;
                action(str);
                while (--i != 0)
                {
                    if(i % (cnt / 1000) == 0) progress();
                    if (i == cnt / 2) action("half done");
                }
                progress();
                str = "done";
                return str;
            });
        }
        public async Task<int> RetInt(Action<object> action, Action progress) 
        {
            int str = 0;
            return await Task.Run(() =>
            {
                int i = cnt;
                action(str);
                while (--i != 0)
                {
                    if (i % (cnt / 1000) == 0) progress();
                    if (i == cnt / 2) action(1);
                }
                str = 2;
                progress();
                return str;
            });
        }
        public async Task<char> RetChar(Action<object> action, Action progress)
        {

            char str = 'S';
            return await Task.Run(() =>
            {
                int i = cnt;
                action(str);
                while (--i != 0)
                {
                    if (i % (cnt / 1000) == 0) progress();
                    if (i == cnt / 2) action('H');
                }
                str = 'D';
                progress();
                return str;
            });
        }
    }
}