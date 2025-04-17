using System.Net;
using System.Net.Http;

public static class HttpClientManager
{
   
    private static readonly CookieContainer _cookieContainer = new CookieContainer();

    
    private static readonly HttpClientHandler _handler = new HttpClientHandler
    {
        CookieContainer = _cookieContainer,
        UseCookies = true 
    };

   
    private static readonly HttpClient _client = new HttpClient(_handler);

   
    public static HttpClient Client => _client;
}
