using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ApiConnectionLibrary.DuoApi.Models
{
	public class TenantResponse
	{
		[JsonProperty("account_id")]
		[JsonPropertyName("account_id")]
		public string AccountId { get; set; }

		[JsonProperty("api_hostname")]
		[JsonPropertyName("api_hostname")]
		public string ApiHostname { get; set; }

		[JsonProperty("name")]
		[JsonPropertyName("name")]
		public string Name { get; set; }
	}
	public class TenantRoot
	{
		[JsonProperty("response")]
		[JsonPropertyName("response")]
		public List<TenantResponse> Response { get; set; }

		[JsonProperty("stat")]
		[JsonPropertyName("stat")]
		public string Stat { get; set; }
	}
}
