using Microsoft.AspNetCore.Components;

namespace BankingSystem.WebUI.Pages
{
    public partial class Home
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private void GotToCustomers()
        {
            NavigationManager.NavigateTo("customers/");
        }
    }
}