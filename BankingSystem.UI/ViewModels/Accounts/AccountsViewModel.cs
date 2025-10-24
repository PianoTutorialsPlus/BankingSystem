using BankingSystem.UI.Models.Account;
using BankingSystem.UI.Navigation;
using BankingSystem.UI.Services.Account;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace BankingSystem.UI.ViewModels.Accounts
{
    public class AccountsViewModel : INotifyPropertyChanged
    {
        private readonly IAccountService accountService;
        private readonly INavigationService navigationService;

        public ObservableCollection<AccountVM> Accounts { get; } = new();

        public ICommand CreateAccountCommand { get; }
        public ICommand DeleteAccountCommand { get; }

        public AccountsViewModel(IAccountService accountService, INavigationService navigationService)
        {
            this.accountService = accountService;
            this.navigationService = navigationService;

            CreateAccountCommand = new RelayCommand(async _ => await CreateAccountAsync());
            DeleteAccountCommand = new RelayCommand(async _ => await DeleteAccountAsync(), _ => SelectedAccount != null);

            _ = LoadAccountsAsync();
        }

        private AccountVM? selectedAccount;
        public AccountVM? SelectedAccount
        {
            get => selectedAccount;
            set
            {
                if (selectedAccount != value)
                {
                    selectedAccount = value;
                    OnPropertyChanged();
                    ((RelayCommand)DeleteAccountCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private async Task LoadAccountsAsync()
        {
            var accounts = await accountService.GetAccounts();

            Accounts.Clear();
            foreach (var a in accounts)
                Accounts.Add(a);

            OnPropertyChanged(nameof(Accounts));
            SelectedAccount = null;
        }

        private async Task CreateAccountAsync()
        {
            navigationService.OpenWindow<CreateAccountViewModel>();
            await LoadAccountsAsync();
        }

        private async Task DeleteAccountAsync()
        {
            if (SelectedAccount is null)
                return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete account {SelectedAccount.AccountNumber}?",
                "Confirm Deletion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            var response = await accountService.DeleteAccount(SelectedAccount.Id);

            if (!response.Success)
            {
                MessageBox.Show(response.Message ?? "Failed to delete account.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            await LoadAccountsAsync();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
