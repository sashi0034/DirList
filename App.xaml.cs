using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DirList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var mutex = new Mutex(false, Assembly.GetExecutingAssembly().GetName().Name);

            // ミューテックスの所有権を要求
            if (!mutex.WaitOne(0, false))
            {
                MessageBox.Show("このアプリケーションは多重起動出来ません。");
                Shutdown();
            }
        }
    }
}
