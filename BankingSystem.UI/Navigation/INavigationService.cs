namespace BankingSystem.UI.Navigation;

public interface INavigationService
{
    void Register(string key, Func<object> viewFactory);
    void NavigateTo(string key);
    object? GetViewFor(string key);
}
