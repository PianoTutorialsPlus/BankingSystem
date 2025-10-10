using System.Collections.Concurrent;

namespace BankingSystem.UI.Navigation;

public class NavigationService : INavigationService
{
    private readonly ConcurrentDictionary<string, Func<object>> _map = new();
    private string? _currentKey;

    public void Register(string key, Func<object> viewFactory) => _map[key] = viewFactory;

    public void NavigateTo(string key)
    {
        if (!_map.ContainsKey(key)) return;
        _currentKey = key;
    }

    public object? GetViewFor(string key) => _map.TryGetValue(key, out var f) ? f() : null;
}

