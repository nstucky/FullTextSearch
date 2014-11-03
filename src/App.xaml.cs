using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FullTextSearch
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private void Application_Startup(object sender, StartupEventArgs e)
    {
      try
      {
        MainWindow oMainWindow = new MainWindow();
        oMainWindow.Show();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "An unhandled exception has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
  }
}
