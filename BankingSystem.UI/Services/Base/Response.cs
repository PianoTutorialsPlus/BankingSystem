using System.Collections;

namespace BankingSystem.UI.Services.Base
{
    public class Response<T>
    {
        public string Message { get; set; }
        public Dictionary<string, string[]> ValidationErrors { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}