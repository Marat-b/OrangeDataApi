using OrangedataRequest.Helpers;
using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OrangedataRequest.DataService
{
    internal sealed class ODDataService
    {
        private readonly OrangeService _orangeService;
        public ODDataService(string keyText, string certPath, string certPassword, string apiUrl = "https://apip.orangedata.ru:2443/api/v2")
        {
            // _keyPath = keyPath;
            _keyText = keyText;
            // _cert = new X509Certificate2(certPath, certPassword);
            // X509KeyStorageFlags flags = X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserKeySet;
            // Pkcs12LoaderLimits limits = new Pkcs12LoaderLimits
            // {
            //     PreserveStorageProvider = true
            // };
            _cert = X509CertificateLoader.LoadPkcs12(File.ReadAllBytes(certPath), certPassword);
            _apiUrl = apiUrl;
            _orangeService = new();
        }

        private string _apiUrl = "https://apip.orangedata.ru:2443/api/v2";

        private readonly string _keyText;
        private readonly X509Certificate2 _cert;
        private readonly Encoding _encoding = Encoding.UTF8;

        #region Public methods

        public async Task<ODResponse> SendCheckAsync(ReqCreateCheck check)
        {
            var requestBody = SerializationHelper.Serialize(check);
            var signature = ComputeSignature(requestBody);

            return await _orangeService.SendRequestAsync<RespCreateCheck>($"{_apiUrl}/documents", _cert, HttpMethod.Post, requestBody, signature).ConfigureAwait(false);
        }

        public async Task<ODResponse> CreateCorrectionsCheckAsync(ReqCreateCorrectionCheck correctionCheck)
        {
            var requestBody = SerializationHelper.Serialize(correctionCheck);
            var signature = ComputeSignature(requestBody);

            return await _orangeService.SendRequestAsync<RespCreateCheck>($"{_apiUrl}/corrections", _cert, HttpMethod.Post, requestBody, signature).ConfigureAwait(false);
        }

        public async Task<ODResponse> GetCheckStateAsync(string INN, string documentId)
        {
            return await _orangeService.SendRequestAsync<RespCheckStatus>($"{_apiUrl}/documents/{INN}/status/{documentId}", _cert, HttpMethod.Get).ConfigureAwait(false);
        }

        public async Task<ODResponse> GetCorrectionCheckStateAsync(string INN, string documentId)
        {
            return await _orangeService.SendRequestAsync<RespCorrectionCheckStatus>($"{_apiUrl}/corrections/{INN}/status/{documentId}", _cert, HttpMethod.Get).ConfigureAwait(false);
        }

        #endregion Public methods

        #region Helpers

        // private async Task<ODResponse> SendRequestAsync<T>(string uri, HttpMethod method, string requestBody = null, string signature = null)
        // {
        //     if (string.IsNullOrEmpty(uri))
        //     {
        //         throw new ArgumentNullException(nameof(uri));
        //     }
        //
        //     using (var client = new HttpClient(new HttpClientHandler
        //     {
        //         ClientCertificates =
        //         {
        //             _cert,
        //         },
        //         ServerCertificateCustomValidationCallback = (a, b, c, d) => true
        //     })) 
        //     {
        //         var request = new HttpRequestMessage(method, uri);
        //         if (!string.IsNullOrWhiteSpace(signature))
        //         {
        //             request.Headers.Add("X-Signature", signature);
        //         }
        //         //httpRequest.KeepAlive = true;
        //         //httpRequest.UserAgent = "OrangeDataClient";
        //         //httpRequest.PreAuthenticate = true;
        //         if (!string.IsNullOrEmpty(requestBody))
        //         {
        //             request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        //         }
        //
        //         var response = await client.SendAsync(request);
        //         return await ExtractResponseAsync<T>(response);
        //     }
        // }
        //
        // private async Task<ODResponse> ExtractResponseAsync<T>(HttpResponseMessage response)
        // {
        //     var res = new ODResponse();
        //     using (response)
        //     {
        //         res.StatusCode = response.StatusCode;
        //         var text = await response.Content.ReadAsStringAsync();
        //         res.Response = text;
        //         try
        //         {
        //             res.ResponseObject = SerializationHelper.Deserealize<T>(text);
        //         }
        //         catch(Exception ex)
        //         {
        //             res.ResponseObject = ex;
        //         }
        //     }
        //     return res;
        // }

        private string ComputeSignature(string document)
        {
            var data = Encoding.UTF8.GetBytes(document);

            using (var rsa = RSA.Create())
            {
                // rsa.ImportFromXml(File.ReadAllText(_keyPath));
                rsa.ImportFromXml(_keyText);
                return Convert.ToBase64String(rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            }
        }

        #endregion Helpers
    }
}
