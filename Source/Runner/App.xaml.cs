using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Runner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Runner.MainWindow window = new MainWindow();
            RunnerViewModel VM = new RunnerViewModel();
            window.DataContext = VM;
            window.Show();
        }
    }
}
