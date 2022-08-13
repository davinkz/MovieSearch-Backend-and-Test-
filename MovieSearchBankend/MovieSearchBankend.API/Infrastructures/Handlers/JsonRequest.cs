namespace MovieSearchBankend.API.Infrastructures.Handlers;

public class JsonRequest
{
    private RequestType _requestType;
    private string _url;
    private object _data;
    private string _contentType = "application/json";
    private Dictionary<string, string> _headers;

    public enum RequestType
    {
        GET,
        POST,
        PUT,
        DELETE,
    }

    public JsonRequest SetUrl(string url)
    {
        _url = url;
        return this;
    }

    public JsonRequest SetData(object data)
    {
        _data = data;
        return this;
    }

    public JsonRequest SetHeaders(Dictionary<string, string> headers)
    {
        _headers = headers;
        return this;
    }

    public JsonRequest SetContentType(string contentType)
    {
        _contentType = contentType;
        return this;
    }

    public JsonRequest SetRequestType(RequestType requestType)
    {
        _requestType = requestType;
        return this;
    }

    public async Task<JsonResponse> Execute()
    {
        try
        {
            var webRequest = new HttpClient();

            webRequest.DefaultRequestHeaders.Clear();
            webRequest.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", _contentType);

            if (_headers != null && _headers.Count > 0)
            {
                foreach (var header in _headers)
                {
                    webRequest.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            HttpResponseMessage webResponse = null;
            var data = _data == null ? null : new StringContent(JsonSerializer.Serialize(_data), Encoding.UTF8, "application/json");

            switch (_requestType)
            {
                case RequestType.GET:
                    webResponse = await webRequest.GetAsync(_url);
                    break;
                case RequestType.POST:
                    webResponse = await webRequest.PostAsync(_url, data);
                    break;
                case RequestType.PUT:
                    webResponse = await webRequest.PutAsync(_url, data);
                    break;
                case RequestType.DELETE:
                    webResponse = await webRequest.DeleteAsync(_url);
                    break;
                default:
                    break;
            }
            if (webResponse is null) return new(HttpStatusCode.NotImplemented, "The request type was not implemented.");

            var result = await webResponse!.Content.ReadAsStringAsync();

            return new(webResponse.StatusCode, result);
        }
        catch (Exception ex)
        {
            return new(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<JsonResponse> Execute(RequestType requestType, string url, object data, string contentType = "application/json", Dictionary<string, string> headers = null)
    {
        return
            await SetRequestType(requestType)
                .SetContentType(contentType)
                .SetUrl(url)
                .SetHeaders(headers)
                .SetData(data)
                .Execute();
    }
}