using System.Windows.Controls;
using BankingSystem.UI.ViewModels;

namespace BankingSystem.UI.Views;

public partial class CustomersView : UserControl
{
    public CustomersView(CustomersViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
