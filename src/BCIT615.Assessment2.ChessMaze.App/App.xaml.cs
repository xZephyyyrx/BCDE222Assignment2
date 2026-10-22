using System.Configuration;
using System.Data;
using System.Windows;
using BCIT615.Assessment2.ChessMaze.View;

namespace BCIT615.Assessment2.ChessMaze.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
  // This function was provided by AI tools to allow the seperation
  // of MainWindow.xaml and its code behind into a seperate View namespace
  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    MainWindow mainWindow = new MainWindow();
    mainWindow.Show();
  }
}

