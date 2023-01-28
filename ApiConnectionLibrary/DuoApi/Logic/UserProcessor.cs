//using RestSharp;

//namespace ApiConnectionLibrary.DuoApi.Logic
//{
//	public class UserProcessor
//	{
//		public static (string authHeader, string date) GetDuUserList()
//		{
//			var client = new RestClient("https://api-2a7c60cd.duosecurity.com");
//			var usersRequest = new RestRequest("/admin/v1/users", Method.Get);
//			usersRequest.AddHeader("X-Duo-Date", $"{date}");
//				usersRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
//				usersRequest.AddHeader("Authorization", $"Basic {authHeader}");

//				var usersResponse = client.Execute(usersRequest);
//				if (usersResponse.IsSuccessful)
//				{
//					var usersJsonString = usersResponse.Content;

//		Console.Write(usersJsonString);
//				}
//			return usersJsonString;
//		}
//	}
//}