using System.Text.Json.Nodes;
using Data.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Server.Helpers;
using ErrorCodes = Shared.Helpers.ErrorCodes;

namespace Server.Common;

public interface IGisCommon
{
    Task<Coordinates> GetCoordinates(string address, CancellationToken cancellationToken);
}

public class GisCommon : IGisCommon
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<GisCommon> _logger;
    private readonly IErrorMessages _errorMessages;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpClientFactory"></param>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    /// <param name="errorMessages"></param>
    public GisCommon(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        ILogger<GisCommon> logger,
        IErrorMessages errorMessages
    )
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
        _errorMessages = errorMessages;
    }

    /// <summary>
    /// GetCoordinates
    /// </summary>
    /// <param name="address"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Coordinates> GetCoordinates(string address, CancellationToken cancellationToken)
    {
        if (address.IsNullOrEmpty())
        {
            _logger.LogWarning("GetCoordinates address - {Address} cannot by null or empty", address);
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_NO_ADDRESS())
                    .SetCode(ErrorCodes.CODE_NOT_FOUND)
                    .Build()
            );
        }

        string url = _configuration.GetSection("Urls:GisSearch").Value!;
        Dictionary<string, string> parameters = new() { { "q", address }, };

        string uri = QueryHelpers.AddQueryString(url, parameters!);
        HttpClient httpClient = _httpClientFactory.CreateClient();
        HttpResponseMessage response = await httpClient.GetAsync(uri, cancellationToken);
        string responseBody = response.Content.ReadAsStringAsync(cancellationToken).Result;

        bool hasValue = JsonNode.Parse(responseBody)!.AsArray().Count != 0;

        var coordinates = new Coordinates
        {
            Lat = hasValue ? double.Parse(JsonNode.Parse(responseBody)![0]!["lat"]!.ToString()) : null,
            Long = hasValue ? double.Parse(JsonNode.Parse(responseBody)![0]!["lon"]!.ToString()) : null
        };

        return coordinates;
    }
}
