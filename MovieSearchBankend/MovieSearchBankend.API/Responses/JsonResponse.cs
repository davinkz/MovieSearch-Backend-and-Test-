using System.Net;

namespace MovieSearchBankend.API.Responses;

public record JsonResponse(HttpStatusCode StatusCode, string Data);