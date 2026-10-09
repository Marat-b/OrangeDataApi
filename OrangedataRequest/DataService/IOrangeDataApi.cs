using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace OrangedataRequest.DataService;

internal interface IOrangeDataApi
{
    [Post("/documents")]
    Task<HttpResponseMessage> CreateCheckAsync([Body] string body, [Header("X-Signature")] string signature, [Header("Content-Type")] string contentType);

    [Post("/corrections")]
    Task<HttpResponseMessage> CreateCorrectionAsync([Body] string body, [Header("X-Signature")] string signature, [Header("Content-Type")] string contentType);

    [Post("/correction12")]
    Task<HttpResponseMessage> CreateCorrection12Async([Body] string body, [Header("X-Signature")] string signature, [Header("Content-Type")] string contentType);

    [Get("/documents/{INN}/status/{documentId}")]
    Task<HttpResponseMessage> GetCheckStatusAsync(string INN, string documentId);

    [Get("/corrections/{INN}/status/{documentId}")]
    Task<HttpResponseMessage> GetCorrectionStatusAsync(string INN, string documentId);

    [Get("/correction12/{INN}/status/{documentId}")]
    Task<HttpResponseMessage> GetCorrectionStatus12Async(string INN, string documentId);

    [Get("/devices/status/{INN}/{groupName}")]
    Task<HttpResponseMessage> GetDeviceStatusAsync(string INN, string groupName);

    [Post("/check")]
    Task<HttpResponseMessage> CheckAccessAsync([Body] string body, [Header("X-Signature")] string signature, [Header("Content-Type")] string contentType);

    [Post("/itemcode")]
    Task<HttpResponseMessage> CreateItemCodeAsync([Body] string body, [Header("X-Signature")] string signature, [Header("Content-Type")] string contentType);

    [Get("/itemcode/{INN}/status/{documentId}")]
    Task<HttpResponseMessage> GetItemCodeStatusAsync(string INN, string documentId);
}
