using Autofac;
using System.Collections.Concurrent;
using System.Reflection;
using System.Windows;

namespace BankingSystem.UI.Navigation;

public class NavigationService : INavigationService
{
    private readonly ILifetimeScope scope;
    private readonly Assembly viewAssembly;
    private readonly HashSet<Type> typesCache;

    public NavigationService(ILifetimeScope scope)
    {
        this.scope = scope;
        viewAssembly = typeof(App).Assembly;
        typesCache = new HashSet<Type>(viewAssembly.GetTypes().Where(x => x.Name.EndsWith("View")));
    }

    private static readonly ConcurrentDictionary<Type, Type> viewCache = new();

    private readonly ConcurrentDictionary<string, Func<object>> _map = new();
    private string? _currentKey;

    public void OpenWindow<T>() where T : class
    {
        var viewModelType = typeof(T);
        if (!viewCache.TryGetValue(viewModelType, out var viewType))
        {
            var namespac = viewModelType.Namespace;
            var viewName = viewModelType.Name.Replace("ViewModel", "View");
            viewType = typesCache.Where(x => x.Name == viewName).FirstOrDefault();

            viewCache[viewModelType] = viewType!;
        }

        using var lifetimeScope = scope.BeginLifetimeScope();
        var view = (Window)Activator.CreateInstance(viewType!)!;
        var viewModel = lifetimeScope.Resolve<T>();

        view.DataContext = viewModel;
        view.ShowDialog();
    }
    public void OpenWindow<TWindow, TParam>(TParam parameter) where TWindow : class
    {
        var viewModelType = typeof(TWindow);
        if (!viewCache.TryGetValue(viewModelType, out var viewType))
        {
            var namespac = viewModelType.Namespace;
            var viewName = viewModelType.Name.Replace("ViewModel", "View");
            viewType = typesCache.Where(x => x.Name == viewName).FirstOrDefault();

            viewCache[viewModelType] = viewType!;
        }

        using var lifetimeScope = scope.BeginLifetimeScope();

        var view = (Window)Activator.CreateInstance(viewType!)!;
        var viewModel = lifetimeScope.Resolve(viewModelType, new TypedParameter(typeof(TParam), parameter));

        view.DataContext = viewModel;
        view.ShowDialog();
    }

    public T Get<T>() where T : class
    {
        using var lifetimeScope = scope.BeginLifetimeScope();
        return lifetimeScope.Resolve<T>();
    }

}

