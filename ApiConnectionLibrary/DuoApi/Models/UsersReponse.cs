using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ApiConnectionLibrary.DuoApi.Models
{
    public class UsersReponse
    {
		[JsonProperty("account_id")]
		[JsonPropertyName("account_id")]
		public string AccountId { get; set; }

		[JsonProperty("email")]
		[JsonPropertyName("email")]
		public string Email { get; set; }

		[JsonProperty("name")]
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonProperty("phone")]
		[JsonPropertyName("phone")]
		public string Phone { get; set; }

		[JsonProperty("user_id")]
		[JsonPropertyName("user_id")]
		public string UserId { get; set; }

		[JsonProperty("username")]
		[JsonPropertyName("username")]
		public string Username { get; set; }

	}
}