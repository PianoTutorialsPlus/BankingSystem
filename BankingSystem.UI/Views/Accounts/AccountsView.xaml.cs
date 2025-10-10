using System.Windows.Controls;
using BankingSystem.UI.ViewModels;

namespace BankingSystem.UI.Views;

public partial class AccountsView : UserControl
{
    public AccountsView(AccountsViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
