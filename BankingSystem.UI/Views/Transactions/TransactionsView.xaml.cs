using System.Windows.Controls;
using BankingSystem.UI.ViewModels;

namespace BankingSystem.UI.Views;

public partial class TransactionsView : UserControl
{
    public TransactionsView(TransactionsViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
