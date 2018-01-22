using SimpleMathQuiz.Core.ViewModels;
using SimpleMathQuiz.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleMathQuiz.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainView app = new MainView();
            MainViewModel context = new MainViewModel();
            app.DataContext = context;
            app.Show();
        }
    }
}
