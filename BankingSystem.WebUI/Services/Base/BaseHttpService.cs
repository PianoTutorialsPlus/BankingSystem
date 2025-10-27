using Blazored.LocalStorage;

namespace BankingSystem.WebUI.Services.Base;

public class BaseHttpService
{
    protected readonly IClient client;
    protected readonly ILocalStorageService localStorageService;

    public BaseHttpService(IClient client, ILocalStorageService localStorageService)
    {
        this.client = client;
        this.localStorageService = localStorageService;
    }

    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
    {
        return ex.StatusCode switch
        {

            400 => new Response<Guid>
            {
                Message = "Invalid data was submitted",
                ValidationErrors = ex.Response,
                Success = false
            },
            404 => new Response<Guid>
            {
                Message = "The record was not found.",
                Success = false
            },
            _ => new Response<Guid>
            {
                Message = "Something went wrong, please try again later.",
                Success = false
            },
        };
    }
}
