using ApiConnectionLibrary.DuoApi.Helpers;
using ApiConnectionLibrary.DuoApi.Models;
using Newtonsoft.Json;
using RestSharp;

namespace ApiConnectionLibrary.DuoApi.Logic;

public class DuoConnect : DuoEndPoint
{ 	
		public static void DuoClient()
	{
		var config = DuoConfigHelper.ReadAppConfig();
		string apiEndpoint, requestParams;
		string method = "";
		
		DuoEndPoint.GetDuoTenants(out apiEndpoint, out requestParams, out method, out Method requestMethod);

		(var authHeader, var date) = SignatureHelper.GetAuthHeader(config, apiEndpoint, requestParams, method);

		var client = new RestClient($"https://{config.apiHost}");

		var request = new RestRequest(apiEndpoint, requestMethod);
		request.AddHeader("X-Duo-Date", $"{date}");
		request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
		request.AddHeader("Authorization", $"Basic {authHeader}");
		
		RestResponse response = client.Execute(request);
		//Console.WriteLine(response.Content);
		if (response.IsSuccessful)
		{
			var jsonString = response.Content;
			var root = JsonConvert.DeserializeObject<TenantRoot>(jsonString);
			var responses = root.Response;
			foreach (var r in responses)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Name: " + r.Name);
				Console.ResetColor();
				Console.WriteLine("Account ID: " + r.AccountId);
				Console.WriteLine("API Hostname: " + r.ApiHostname);
				Console.WriteLine();
			}
		}
		else
		{
			Console.WriteLine("Error: " + response.ErrorMessage);
		}



		Console.ReadLine();
	}
}



