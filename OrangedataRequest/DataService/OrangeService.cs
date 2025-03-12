using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OrangedataRequest.Helpers;

namespace OrangedataRequest.DataService;

public class OrangeService
{
    private readonly HttpClient _httpClient;

    public OrangeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private async Task<ODResponse> SendRequestAsync<T>(string uri, HttpMethod method, string requestBody = null, string signature = null)
    {
        // if (string.IsNullOrEmpty(uri))
        // {
        //     throw new ArgumentNullException(nameof(uri));
        // }

        // using (var client = new HttpClient(new HttpClientHandler
        //        {
        //            ClientCertificates =
        //            {
        //                _cert,
        //            },
        //            ServerCertificateCustomValidationCallback = (a, b, c, d) => true
        //        })) 
        // {
        _httpClient.DefaultRequestHeaders.
            var request = new HttpRequestMessage(method, uri);
            if (!string.IsNullOrWhiteSpace(signature))
            {
                request.Headers.Add("X-Signature", signature);
            }
            if (!string.IsNullOrEmpty(requestBody))
            {
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            }

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            return await ExtractResponseAsync<T>(response).ConfigureAwait(false);
        // }
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