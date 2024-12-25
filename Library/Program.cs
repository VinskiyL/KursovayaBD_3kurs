using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    internal static class Program
    {
        /// <summary> 
        /// Главная точка входа для приложения. 
        /// </summary> 
        [STAThread]
        static async Task Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Запуск планировщика
            await Scheduler.Start();

            // Запуск графического интерфейса
            Application.Run(new Menu());

        }
    }
}

