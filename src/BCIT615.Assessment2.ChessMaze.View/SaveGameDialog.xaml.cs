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

namespace BCIT615.Assessment2.ChessMaze.View;

public partial class SaveGameDialog : Window
{
  public string SaveName { get; private set; } = string.Empty;

  public SaveGameDialog()
  {
    InitializeComponent();
  }

  public void ConfirmSave_Click(object sender, RoutedEventArgs e)
  {
    
    SaveName = SaveGameNameTextBox.Text.Trim();

    if (!string.IsNullOrWhiteSpace(SaveName))
    {
      DialogResult = true;
    }
    else
    {
      DialogMessageText.Text = "A valid name is needed to save!";
      DialogMessageText.Foreground = Brushes.Red;
    }
  }

  public void CancelSave_Click(object sender, RoutedEventArgs e)
  {
    DialogResult = false;
  }
}
