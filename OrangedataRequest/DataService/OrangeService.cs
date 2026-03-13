using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OrangedataRequest.Helpers;
// using Polly;

namespace OrangedataRequest.DataService;

public class OrangeService
{
    // private readonly HttpClient _httpClient;
    //
    // public OrangeService(HttpClient httpClient)
    // {
    //     _httpClient = httpClient;
    // }
    public async Task<ODResponse> SendRequestAsync<T>(string uri, X509Certificate2 cert,  HttpMethod method, string requestBody = null, string signature = null)
    {
        if (string.IsNullOrEmpty(uri))
        {
            throw new ArgumentNullException(nameof(uri));
        }
        // var httpPolicy = Policy.Handle<TimeoutException>().WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(5));

        // var client = new HttpClient(new HttpClientHandler
        // {
        //     ClientCertificates =
        //     {
        //         cert,
        //     },
        //     ServerCertificateCustomValidationCallback = (a, b, c, d) => true
        // });
        
        var client = new HttpClient(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(2),
            SslOptions = new System.Net.Security.SslClientAuthenticationOptions
            {
                ClientCertificates = new X509Certificate2Collection(cert),
                EnabledSslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13
            }
        });
        
        var request = new HttpRequestMessage(method, uri);
        if (!string.IsNullOrWhiteSpace(signature))
        {
            request.Headers.Add("X-Signature", signature);
        }
        if (!string.IsNullOrEmpty(requestBody))
        {
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        }

        // using var response = await httpPolicy.ExecuteAsync(async () => await client.SendAsync(request).ConfigureAwait(false)).ConfigureAwait(false);
        using var response = await client.SendAsync(request).ConfigureAwait(false);
        return await ExtractResponseAsync<T>(response).ConfigureAwait(false);
    }
    
    private async Task<ODResponse> ExtractResponseAsync<T>(HttpResponseMessage response)
    {
        var res = new ODResponse();
        using (response)
        {
            res.StatusCode = response.StatusCode;
            var text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            res.Response = text;
            try
            {
                res.ResponseObject = SerializationHelper.Deserealize<T>(text);
            }
            catch(Exception ex)
            {
                res.ResponseObject = ex;
            }
        }
        return res;
    }
}