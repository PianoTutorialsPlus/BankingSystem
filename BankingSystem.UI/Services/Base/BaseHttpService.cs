namespace BankingSystem.UI.Services.Base;

public class BaseHttpService
{
    protected readonly IClient client;

    public BaseHttpService(IClient client)
    {
        this.client = client;
    }

    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
    {
        if (ex is ApiException<ProblemDetails> apiException)
        {
            // testing to get the return value
            var errors = ExtractErrorsFromProblemDetails(apiException.Result);

            return ex.StatusCode switch
            {

                400 => new Response<Guid>
                {
                    Message = "Invalid data was submitted",
                    ValidationErrors = errors,
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

        return new Response<Guid>
        {
            Message = "Something went wrong, please try again later.",
            Success = false
        };

    }

    private static Dictionary<string, string[]> ExtractErrorsFromProblemDetails(ProblemDetails? details)
    {
        if (details?.AdditionalProperties == null)
            return new Dictionary<string, string[]>();

        if (!details.AdditionalProperties.TryGetValue("errors", out var rawErrors) || rawErrors == null)
            return new Dictionary<string, string[]>();

        try
        {
            // When using System.Text.Json, "rawErrors" might be a JsonElement
            if (rawErrors is System.Text.Json.JsonElement jsonElement && jsonElement.ValueKind == System.Text.Json.JsonValueKind.Object)
            {
                var json = jsonElement.GetRawText(); // extract JSON string
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string[]>>(json)
                       ?? new Dictionary<string, string[]>();
            }

            // Otherwise, fall back to JSON serialize/deserialize for robustness
            var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(rawErrors);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string[]>>(serialized)
                   ?? new Dictionary<string, string[]>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ExtractErrorsFromProblemDetails] Failed: {ex.Message}");
            return new Dictionary<string, string[]>();
        }
    }

}
