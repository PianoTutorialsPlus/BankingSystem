using System.Windows;
using BankingSystem.UI.ViewModels;

namespace BankingSystem.UI.Views;

public partial class ShellMainView : Window
{
    public ShellMainView(ShellMainViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
