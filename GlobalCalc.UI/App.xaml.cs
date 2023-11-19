using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace GlobalCalc.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += OnDispatcherUnhandledException;

            if (ServicesManager.Services.App.Status != null)
                MessageBox.Show(ServicesManager.Services.App.Status, "Внимание!");
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Фатальная ошибка");
            Shutdown(-1);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (e.ApplicationExitCode == 0)
                ServicesManager.Services.Dispose();
            
            base.OnExit(e);
        }
    }
}