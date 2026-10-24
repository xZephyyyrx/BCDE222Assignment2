using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BCIT615.Assessment2.ChessMaze.ViewModel;

namespace BCIT615.Assessment2.ChessMaze.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  private readonly MainWindowViewModel viewModel = new();
    
  public MainWindow()
  {
    InitializeComponent();
    DataContext = viewModel;

    // This ensures the message display scrolls down to display any new messages added
    viewModel.MessageBoxOutput.CollectionChanged += (s, e) =>
    {
      MessageDisplayBox.ScrollToBottom();
    };
  }
}
