using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Logging;
using LootLocker.Result;

namespace LootLocker;

public abstract class API<R>
{
    private static readonly HttpClient client = new();
    private readonly string BaseUrl;

    private readonly static Logger Logger = Logger.Create(typeof(R).Name);

    protected API(string baseUrl)
    {
        BaseUrl = baseUrl;
    }

    protected async Task<T> PostAsync<T>(string endpoint, Dictionary<string, string> headers,
               object? data = null, CancellationToken cancellationToken = default) where T : IResult, new()
    {
        var requestMessage = new HttpRequestMessage
        {
            RequestUri = new Uri(BaseUrl + endpoint),
            Method = HttpMethod.Post,
            Content = JsonContent.Create(data)
        };

        foreach (var header in headers)
        {
            requestMessage.Headers.Add(header.Key.ToString(), header.Value);
        }

        HttpResponseMessage response;

        try
        {
            response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);
            _ = response.EnsureSuccessStatusCode();
            Logger.Log(Logger.Level.Debug, response);
        }
        catch (HttpRequestException httpException)
        {
            Logger.Error("An error occured while making an HTTP request:");
            Logger.Error(httpException.Message);
            return new T();
        }
        catch (Exception exception)
        {
            Logger.Error("Unknown error occured while making an HTTP request:");
            Logger.Error(exception.Message);
            return new T();
        }

        try
        {
            return await response.Content.ReadFromJsonAsync<T>(cancellationToken) ?? new T();
        }
        catch (JsonException jsonException)
        {
            Logger.Error("An error occured while deserializing Http response:");
            Logger.Error(jsonException.Message);
        }

        return new T();
    }

    protected async Task<T> GetAsync<T>(string endpoint, Dictionary<string, string> headers,
               Dictionary<string, string> query, CancellationToken cancellationToken = default) where T : IResult, new()
    {
        var builder = new UriBuilder(BaseUrl + endpoint);
        builder.Port = -1;
        var queryString = HttpUtility.ParseQueryString(builder.Query);

        foreach (var kvp in query)
        {
            queryString[kvp.Key] = kvp.Value;
        }
        builder.Query = query.ToString();

        var requestMessage = new HttpRequestMessage
        {
            RequestUri = builder.Uri,
            Method = HttpMethod.Get,
        };

        foreach (var header in headers)
        {
            requestMessage.Headers.Add(header.Key.ToString(), header.Value);
        }

        HttpResponseMessage response;

        try
        {
            response = await client.SendAsync(requestMessage, cancellationToken);
            _ = response.EnsureSuccessStatusCode();
            Logger.Log(Logger.Level.Debug, response);
        }
        catch (HttpRequestException httpException)
        {
            Logger.Error("An error occured while making an HTTP request:");
            Logger.Error(httpException.Message);
            return new T();
        }
        catch (Exception exception)
        {
            Logger.Error("Unknown error occured while making an HTTP request:");
            Logger.Error(exception.Message);
            return new T();
        }

        try
        {
            return await response.Content.ReadFromJsonAsync<T>(cancellationToken) ?? new T();
        }
        catch (JsonException jsonException)
        {
            Logger.Error("An error occured while deserializing Http response:");
            Logger.Error(jsonException.Message);
        }

        return new T();
    }

    ~API()
    {
        client?.Dispose();
    }
}