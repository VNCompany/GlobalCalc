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
        private readonly string? facadeDataLoadStatus;

        public App()
        {
            try
            {
                facadeDataLoadStatus = ServicesManager.Services.App.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Фатальная ошибка!");
                Shutdown(-1);
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (facadeDataLoadStatus != null)
                MessageBox.Show(facadeDataLoadStatus, "Внимание!");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (e.ApplicationExitCode == 0)
                ServicesManager.Services.Dispose();

            base.OnExit(e);
        }
    }
}