using ApiConnectionLibrary.DuoApi.Models;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;

namespace ApiConnectionLibrary.DuoApi.Helpers;

public class SignatureHelper
{
    public static (string authHeader, string date) GetAuthHeader(ConfigModel config, string apiEndpoint, string requestParams, string method)
    {
        Dictionary<string, string> requestMethod = new Dictionary<string, string>();
        requestMethod.Add("requestMethod", $"{method}");

        var date = DateTime.UtcNow.ToString("ddd, dd MMM yyyy HH:mm:ss -0000");
        var formattedParams = requestParams.Trim();
        var requestToSign = date.Trim() + "\n" + requestMethod.Values.First().ToUpper().Trim() + "\n" + config.apiHost.ToLower().Trim() + "\n" + apiEndpoint.Trim() + "\n" + formattedParams;
        var requestToSignChar = requestToSign.ToCharArray();
        var requestToSignBytes = Encoding.UTF8.GetBytes(requestToSignChar);
        var hmacSHA1 = new HMACSHA1(Encoding.UTF8.GetBytes(config.apiSecret));
        var signature = hmacSHA1.ComputeHash(requestToSignBytes);
        var authSignature = BitConverter.ToString(hmacSHA1.Hash).Replace("-", "").ToLower();
        var authHeader = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", config.apiKey, authSignature)));

        return (authHeader, date);
    }
}