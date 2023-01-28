using ApiConnectionLibrary.DuoApi.Helpers;
using ApiConnectionLibrary.DuoApi.Models;
using Newtonsoft.Json;
using RestSharp;

namespace ApiConnectionLibrary.DuoApi.Logic;

public class DuoConnect
{ 	
		public static void DuoClient()
			{
		var config = DuoApiHelper.ReadAppConfig();

		var apiEndpoint = "/accounts/v1/account/list";
		string requestParams = "";

		(var authHeader, var date) = SignatureHelper.GetAuthHeader(config, apiEndpoint, requestParams);

		var client = new RestClient("https://api-2a7c60cd.duosecurity.com");
				var request = new RestRequest("/accounts/v1/account/list", Method.Post);
				request.AddHeader("X-Duo-Date", $"{date}");
				request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
				request.AddHeader("Authorization", $"Basic {authHeader}");

				foreach (var param in request.Parameters)
				{
					//Console.WriteLine(param.Name + ": " + param.Value);
				}

				RestResponse response = client.Execute(request);
				//Console.WriteLine(response.Content);
				if (response.IsSuccessful)
				{
					var jsonString = response.Content;
					var root = JsonConvert.DeserializeObject<TenantRoot>(jsonString);
					var responses = root.Response;
			foreach (var r in responses)
			{
				Console.WriteLine("Account ID: " + r.AccountId);
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Name: " + r.Name);
				Console.ResetColor();
				Console.WriteLine("API Hostname: " + r.ApiHostname);
				Console.WriteLine();

				var usersRequest = new RestRequest("/admin/v1/users", Method.Get);
				usersRequest.AddHeader("X-Duo-Date", $"{date}");
				usersRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
				usersRequest.AddHeader("Authorization", $"Basic {authHeader}");

				var usersResponse = client.Execute(usersRequest);
				if (usersResponse.IsSuccessful)
				{
					var usersJsonString = usersResponse.Content;

					Console.Write(usersJsonString);
				}
			}
				}
				else
				{
					Console.WriteLine("Error: " + response.ErrorMessage);
				}

				

				Console.ReadLine();
			}
		}



