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
        
        public async Task<ODResponse> CreateCorrectionsCheckAsync12(ReqCreateCorrectionCheck12 correctionCheck)
        {
            var requestBody = SerializationHelper.Serialize(correctionCheck);
            var signature = ComputeSignature(requestBody);

            return await _orangeService.SendRequestAsync<RespCreateCheck>($"{_apiUrl}/correction12", _cert, HttpMethod.Post, requestBody, signature).ConfigureAwait(false);
        }

        public async Task<ODResponse> GetCheckStateAsync(string INN, string documentId)
        {
            return await _orangeService.SendRequestAsync<RespCheckStatus>($"{_apiUrl}/documents/{INN}/status/{documentId}", _cert, HttpMethod.Get).ConfigureAwait(false);
        }

        public async Task<ODResponse> GetCorrectionCheckStateAsync(string INN, string documentId)
        {
            return await _orangeService.SendRequestAsync<RespCorrectionCheckStatus>($"{_apiUrl}/corrections/{INN}/status/{documentId}", _cert, HttpMethod.Get).ConfigureAwait(false);
        }
        
        public async Task<ODResponse> GetCorrectionCheckStateAsync12(string INN, string documentId)
        {
            return await _orangeService.SendRequestAsync<RespCorrectionCheckStatus12>($"{_apiUrl}/correction12/{INN}/status/{documentId}", _cert, HttpMethod.Get).ConfigureAwait(false);
        }

        public async Task<ODResponse> GetKKTDeviceStateAsync(string INN, string groupName)
        {
            return await _orangeService.SendRequestAsync<RespKKTDevicesStatus>($"{_apiUrl}/devices/status/{INN}/{groupName}", _cert, HttpMethod.Get).ConfigureAwait(false);
        }

        public async Task<ODResponse> GetAccessStateAsync(ReqAccessStatus request)
        {
            var requestBody = SerializationHelper.Serialize(request);
            var signature = ComputeSignature(requestBody);
            return await _orangeService.SendRequestAsync<RespAccessStatus>($"{_apiUrl}/check", _cert, HttpMethod.Post,requestBody,signature).ConfigureAwait(false);
        }

        public async Task<ODResponse> CreateItemCodeCheckAsync(ReqItemCodeCheck request)
        {
            var requestBody = SerializationHelper.Serialize(request);
            Console.WriteLine($"CreateItemCodeCheckAsync request body: {requestBody}");
            var signature = ComputeSignature(requestBody);

            return await _orangeService.SendRequestAsync<ReqItemCodeCheck>($"{_apiUrl}/itemcode", _cert, HttpMethod.Post, requestBody, signature).ConfigureAwait(false);
        }

        public async Task<ODResponse> GetItemCodeStateAsync(string INN, string documentId)
        {
            return await _orangeService.SendRequestAsync<RespItemCodeStatus>($"{_apiUrl}/itemcode/{INN}/status/{documentId}", _cert, HttpMethod.Get).ConfigureAwait(false);
        }

        #endregion Public methods

        #region Helpers

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
