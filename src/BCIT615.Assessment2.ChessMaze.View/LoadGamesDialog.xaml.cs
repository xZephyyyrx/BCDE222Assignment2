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
using BCIT615.Assessment2.ChessMaze.Data;

namespace BCIT615.Assessment2.ChessMaze.View;

public partial class LoadGameDialog : Window
{
  public LoadGameDialog(List<SavedGame> saves)
  {
    InitializeComponent();

    SavedGameListBox.ItemsSource = saves;
  }

  public void ConfirmLoad_Click(object sender, RoutedEventArgs e)
  {
    if (SavedGameListBox.SelectedItem is SavedGame selected)
    {
      DialogResult = true;
    }
    else
    {
      DialogMessageText.Text = "Cannot load without a game selected!";
      DialogMessageText.Foreground = Brushes.Red;
    }
  }

  public void CancelLoad_Click(object sender, RoutedEventArgs e)
  {
    DialogResult = false;
  }
}
