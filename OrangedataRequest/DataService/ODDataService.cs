using OrangedataRequest.Helpers;
using System;
using System.IO;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace OrangedataRequest.DataService
{
    internal sealed class ODDataService
    {
        private const string JsonContentType = "application/json";

        private readonly IOrangeDataApi _api;

        public ODDataService(string keyText, string certPath, string certPassword, string apiUrl = "https://apip.orangedata.ru:2443/api/v2")
        {
            _keyText = keyText;
            _cert = X509CertificateLoader.LoadPkcs12(File.ReadAllBytes(certPath), certPassword);
            _api = RestService.For<IOrangeDataApi>(CreateHttpClient(apiUrl));
        }

        public ODDataService(string keyText, string certPath, string certPassword, string apiUrl, DelegatingHandler outerHandler)
        {
            _keyText = keyText;
            _cert = X509CertificateLoader.LoadPkcs12(File.ReadAllBytes(certPath), certPassword);
            _api = RestService.For<IOrangeDataApi>(CreateHttpClient(apiUrl, outerHandler));
        }

        private readonly string _keyText;
        private readonly X509Certificate2 _cert;

        #region Public methods

        public async Task<ODResponse> SendCheckAsync(ReqCreateCheck check)
        {
            var requestBody = SerializationHelper.Serialize(check);
            var signature = ComputeSignature(requestBody);

            return await ExtractResponseAsync<RespCreateCheck>(await _api.CreateCheckAsync(requestBody, signature, JsonContentType).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public async Task<ODResponse> CreateCorrectionsCheckAsync(ReqCreateCorrectionCheck correctionCheck)
        {
            var requestBody = SerializationHelper.Serialize(correctionCheck);
            var signature = ComputeSignature(requestBody);

            return await ExtractResponseAsync<RespCreateCheck>(await _api.CreateCorrectionAsync(requestBody, signature, JsonContentType).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public async Task<ODResponse> CreateCorrectionsCheckAsync12(ReqCreateCorrectionCheck12 correctionCheck)
        {
            var requestBody = SerializationHelper.Serialize(correctionCheck);
            var signature = ComputeSignature(requestBody);

            return await ExtractResponseAsync<RespCreateCheck>(await _api.CreateCorrection12Async(requestBody, signature, JsonContentType).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public async Task<ODResponse> GetCheckStateAsync(string INN, string documentId)
        {
            return await ExtractResponseAsync<RespCheckStatus>(await _api.GetCheckStatusAsync(INN, documentId).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public async Task<ODResponse> GetCorrectionCheckStateAsync(string INN, string documentId)
        {
            return await ExtractResponseAsync<RespCorrectionCheckStatus>(await _api.GetCorrectionStatusAsync(INN, documentId).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public async Task<ODResponse> GetCorrectionCheckStateAsync12(string INN, string documentId)
        {
            return await ExtractResponseAsync<RespCorrectionCheckStatus12>(await _api.GetCorrectionStatus12Async(INN, documentId).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public async Task<ODResponse> GetKKTDeviceStateAsync(string INN, string groupName)
        {
            return await ExtractResponseAsync<RespKKTDevicesStatus>(await _api.GetDeviceStatusAsync(INN, groupName).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public async Task<ODResponse> GetAccessStateAsync(ReqAccessStatus request)
        {
            var requestBody = SerializationHelper.Serialize(request);
            var signature = ComputeSignature(requestBody);

            return await ExtractResponseAsync<RespAccessStatus>(await _api.CheckAccessAsync(requestBody, signature, JsonContentType).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public async Task<ODResponse> CreateItemCodeCheckAsync(ReqItemCodeCheck request)
        {
            var requestBody = SerializationHelper.Serialize(request);
            var signature = ComputeSignature(requestBody);

            return await ExtractResponseAsync<RespItemCodeCheck>(await _api.CreateItemCodeAsync(requestBody, signature, JsonContentType).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public async Task<ODResponse> GetItemCodeStateAsync(string INN, string documentId)
        {
            return await ExtractResponseAsync<RespItemCodeStatus>(await _api.GetItemCodeStatusAsync(INN, documentId).ConfigureAwait(false)).ConfigureAwait(false);
        }

        #endregion Public methods

        #region Helpers

        private HttpClient CreateHttpClient(string apiUrl, DelegatingHandler outerHandler = null)
        {
            var handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(2),
                SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                {
                    ClientCertificates = new X509Certificate2Collection(_cert),
                    EnabledSslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13
                }
            };

            // Путь запроса склеивается в режиме RefitLegacy: BaseAddress.TrimEnd('/') + "/documents"
            if (outerHandler != null)
            {
                outerHandler.InnerHandler = handler;
                return new HttpClient(outerHandler) { BaseAddress = new Uri(apiUrl) };
            }
            return new HttpClient(handler) { BaseAddress = new Uri(apiUrl) };
        }

        private string ComputeSignature(string document)
        {
            var data = Encoding.UTF8.GetBytes(document);

            using (var rsa = RSA.Create())
            {
                rsa.ImportFromXml(_keyText);
                return Convert.ToBase64String(rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            }
        }

        private static async Task<ODResponse> ExtractResponseAsync<T>(HttpResponseMessage response)
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
                catch (Exception ex)
                {
                    res.ResponseObject = ex;
                }
            }
            return res;
        }

        #endregion Helpers
    }
}
