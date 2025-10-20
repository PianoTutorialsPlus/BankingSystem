namespace BankingSystem.UI.Navigation;

public interface INavigationService
{
    void OpenWindow<TWindow>() where TWindow : class;
    void OpenWindow<TWindow, TParam>(TParam parameter) where TWindow : class;
    T Get<T>() where T : class;
}
