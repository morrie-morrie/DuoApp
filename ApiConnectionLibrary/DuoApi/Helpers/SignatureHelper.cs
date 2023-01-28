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

        //requestToSign needs to add the date, requestmethod, apihost, apiendpoint and formattedParams together with linebreaks

        var requestToSign = date.Trim() + "\n" + requestMethod.Values.First().ToUpper().Trim() + "\n" + config.apiHost.ToLower().Trim() + "\n" + apiEndpoint.Trim() + "\n" + formattedParams;
        //Console.WriteLine(requestToSign);

        var requestToSignChar = requestToSign.ToCharArray();
        //Console.WriteLine(requestToSignChar);
        var requestToSignBytes = Encoding.UTF8.GetBytes(requestToSignChar);
        //Console.WriteLine(Encoding.UTF8.GetString(requestToSignBytes));

        // Take the requestToSignBytes and sign it with the apiSecret using HMACSHA1
        var hmacSHA1 = new HMACSHA1(Encoding.UTF8.GetBytes(config.apiSecret));
        var signature = hmacSHA1.ComputeHash(requestToSignBytes);
        var authSignature = BitConverter.ToString(hmacSHA1.Hash).Replace("-", "").ToLower();
        //Console.WriteLine(authSignature);

        var authHeader = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", config.apiKey, authSignature)));

        return (authHeader, date);

    }
}

