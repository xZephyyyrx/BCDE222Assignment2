using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BCIT615.Assessment2.ChessMaze.ViewModel;

public sealed class MainWindowViewModel : INotifyPropertyChanged
{
  public event PropertyChangedEventHandler? PropertyChanged;

  // This is used by the ItemControl in MainWindwo.xaml. It allows messages to be
  // displayed with different colors depending on what type of message they are
  public ObservableCollection<StyledMessage> MessageBoxOutput { get; } = new();

  public MainWindowViewModel() {
    this.AddMessage(new StyledMessage("Initializing Program", "Green"));
    this.AddMessage(new StyledMessage("Loading Assets...", "Green"));
    this.AddMessage(new StyledMessage("Loading Complete!", "Green"));
    this.AddMessage(new StyledMessage("Hello World!", "Yellow"));
  }

  private void AddMessage(StyledMessage message)
  {
    MessageBoxOutput.Add(message);
  }
  
  private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}
