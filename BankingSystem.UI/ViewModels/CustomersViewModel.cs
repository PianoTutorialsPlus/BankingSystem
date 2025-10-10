using BankingSystem.Application.DTOs;
using BankingSystem.UI.Services;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BankingSystem.UI.ViewModels;

public class CustomersViewModel : INotifyPropertyChanged
{
    private readonly IApiService _api;

    public DataSet CustomersDataSet { get; } = new();
    public DataView? CustomersView => CustomersDataSet.Tables.Contains("Customers") ? CustomersDataSet.Tables["Customers"].DefaultView : null;

    public ICommand LoadCommand { get; }
    public ICommand SaveCommand { get; }

    public CustomersViewModel(IApiService api)
    {
        _api = api;
        LoadCommand = new RelayCommand(async _ => await LoadAsync());
        SaveCommand = new RelayCommand(async _ => await SaveAsync());
    }

    public async Task LoadAsync()
    {
        var list = await _api.GetCustomersAsync();
        var dt = Utils.DataTableConverter.ToDataTable(list);
        dt.TableName = "Customers";
        CustomersDataSet.Tables.Clear();
        CustomersDataSet.Tables.Add(dt);
        OnPropertyChanged(nameof(CustomersView));
    }

    public async Task SaveAsync()
    {
        if (!CustomersDataSet.Tables.Contains("Customers")) return;
        var dt = CustomersDataSet.Tables["Customers"];
        foreach (DataRow r in dt.Rows)
        {
            if (r.RowState == DataRowState.Added)
            {
                var dto = new CustomerDto
                {
                    FirstName = r.Field<string>("FirstName") ?? string.Empty,
                    LastName = r.Field<string>("LastName") ?? string.Empty,
                    Street = r.Field<string>("Street") ?? string.Empty,
                    HouseNumber = r.Field<string>("HouseNumber") ?? string.Empty,
                    ZipCode = r.Field<string>("ZipCode") ?? string.Empty,
                    City = r.Field<string>("City") ?? string.Empty,
                    PhoneNumber = r.Field<string>("PhoneNumber") ?? string.Empty,
                    EmailAddress = r.Field<string>("EmailAddress") ?? string.Empty
                };
                await _api.CreateCustomerAsync(dto);
            }
            else if (r.RowState == DataRowState.Modified)
            {
                var id = r.Field<int>("Id");
                var dto = new CustomerDto
                {
                    Id = id,
                    FirstName = r.Field<string>("FirstName") ?? string.Empty,
                    LastName = r.Field<string>("LastName") ?? string.Empty,
                    Street = r.Field<string>("Street") ?? string.Empty,
                    HouseNumber = r.Field<string>("HouseNumber") ?? string.Empty,
                    ZipCode = r.Field<string>("ZipCode") ?? string.Empty,
                    City = r.Field<string>("City") ?? string.Empty,
                    PhoneNumber = r.Field<string>("PhoneNumber") ?? string.Empty,
                    EmailAddress = r.Field<string>("EmailAddress") ?? string.Empty
                };
                await _api.UpdateCustomerAsync(id, dto);
            }
            else if (r.RowState == DataRowState.Deleted)
            {
                var id = (int)r["Id", DataRowVersion.Original];
                await _api.DeleteCustomerAsync(id);
            }
        }

        await LoadAsync();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
}

