using System.Collections;
using System.ComponentModel;

namespace BankingSystem.UI.Models.Customer
{
    public abstract class AbstractDtoValidationBase: INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors = new();

        public bool HasErrors => _errors.Any();
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        => _errors.TryGetValue(propertyName ?? "", out var list) ? list : Enumerable.Empty<string>();

        public void SetErrors(string property, IEnumerable<string> messages)
        {
            _errors[property] = messages.ToList();
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(property));
        }
    }
}