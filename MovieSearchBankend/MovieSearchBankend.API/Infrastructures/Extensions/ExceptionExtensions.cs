namespace MovieSearchBankend.API.Infrastructures.Extensions;

public static class ExceptionExtensions
{
    public static string GetDetails(this Exception exception)
    {
        if (exception is null)
            return "";
        if (string.IsNullOrWhiteSpace(exception.Message) && exception.InnerException != null)
            return GetDetails(exception.InnerException);

        if (exception.Message.ToLower().Contains("inner exception") && exception.InnerException is not null)
            return GetDetails(exception.InnerException);

        return exception.Message;
    }
}
