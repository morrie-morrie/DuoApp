using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ApiConnectionLibrary.DuoApi.Models
{
	public class Aliases
	{
	}

	public class Group
	{
		[JsonProperty("desc")]
		[JsonPropertyName("desc")]
		public string Desc { get; set; }

		[JsonProperty("group_id")]
		[JsonPropertyName("group_id")]
		public string GroupId { get; set; }

		[JsonProperty("mobile_otp_enabled")]
		[JsonPropertyName("mobile_otp_enabled")]
		public bool MobileOtpEnabled { get; set; }

		[JsonProperty("name")]
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonProperty("push_enabled")]
		[JsonPropertyName("push_enabled")]
		public bool PushEnabled { get; set; }

		[JsonProperty("sms_enabled")]
		[JsonPropertyName("sms_enabled")]
		public bool SmsEnabled { get; set; }

		[JsonProperty("status")]
		[JsonPropertyName("status")]
		public string Status { get; set; }

		[JsonProperty("voice_enabled")]
		[JsonPropertyName("voice_enabled")]
		public bool VoiceEnabled { get; set; }
	}

	public class Metadata
	{
		[JsonProperty("total_objects")]
		[JsonPropertyName("total_objects")]
		public int TotalObjects { get; set; }
	}

	public class Phone
	{
		[JsonProperty("activated")]
		[JsonPropertyName("activated")]
		public bool Activated { get; set; }

		[JsonProperty("capabilities")]
		[JsonPropertyName("capabilities")]
		public List<string> Capabilities { get; set; }

		[JsonProperty("extension")]
		[JsonPropertyName("extension")]
		public string Extension { get; set; }

		[JsonProperty("last_seen")]
		[JsonPropertyName("last_seen")]
		public object LastSeen { get; set; }

		[JsonProperty("model")]
		[JsonPropertyName("model")]
		public string Model { get; set; }

		[JsonProperty("name")]
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonProperty("number")]
		[JsonPropertyName("number")]
		public string Number { get; set; }

		[JsonProperty("phone_id")]
		[JsonPropertyName("phone_id")]
		public string PhoneId { get; set; }

		[JsonProperty("platform")]
		[JsonPropertyName("platform")]
		public string Platform { get; set; }

		[JsonProperty("postdelay")]
		[JsonPropertyName("postdelay")]
		public string Postdelay { get; set; }

		[JsonProperty("predelay")]
		[JsonPropertyName("predelay")]
		public string Predelay { get; set; }

		[JsonProperty("sms_passcodes_sent")]
		[JsonPropertyName("sms_passcodes_sent")]
		public bool SmsPasscodesSent { get; set; }

		[JsonProperty("type")]
		[JsonPropertyName("type")]
		public string Type { get; set; }
	}

	public class Response
	{
		[JsonProperty("alias1")]
		[JsonPropertyName("alias1")]
		public object Alias1 { get; set; }

		[JsonProperty("alias2")]
		[JsonPropertyName("alias2")]
		public object Alias2 { get; set; }

		[JsonProperty("alias3")]
		[JsonPropertyName("alias3")]
		public object Alias3 { get; set; }

		[JsonProperty("alias4")]
		[JsonPropertyName("alias4")]
		public object Alias4 { get; set; }

		[JsonProperty("aliases")]
		[JsonPropertyName("aliases")]
		public Aliases Aliases { get; set; }

		[JsonProperty("created")]
		[JsonPropertyName("created")]
		public int Created { get; set; }

		[JsonProperty("desktoptokens")]
		[JsonPropertyName("desktoptokens")]
		public List<object> Desktoptokens { get; set; }

		[JsonProperty("email")]
		[JsonPropertyName("email")]
		public string Email { get; set; }

		[JsonProperty("firstname")]
		[JsonPropertyName("firstname")]
		public string Firstname { get; set; }

		[JsonProperty("groups")]
		[JsonPropertyName("groups")]
		public List<Group> Groups { get; set; }

		[JsonProperty("is_enrolled")]
		[JsonPropertyName("is_enrolled")]
		public bool IsEnrolled { get; set; }

		[JsonProperty("last_directory_sync")]
		[JsonPropertyName("last_directory_sync")]
		public int LastDirectorySync { get; set; }

		[JsonProperty("last_login")]
		[JsonPropertyName("last_login")]
		public int? LastLogin { get; set; }

		[JsonProperty("lastname")]
		[JsonPropertyName("lastname")]
		public string Lastname { get; set; }

		[JsonProperty("notes")]
		[JsonPropertyName("notes")]
		public string Notes { get; set; }

		[JsonProperty("phones")]
		[JsonPropertyName("phones")]
		public List<Phone> Phones { get; set; }

		[JsonProperty("realname")]
		[JsonPropertyName("realname")]
		public string Realname { get; set; }

		[JsonProperty("status")]
		[JsonPropertyName("status")]
		public string Status { get; set; }

		[JsonProperty("tokens")]
		[JsonPropertyName("tokens")]
		public List<object> Tokens { get; set; }

		[JsonProperty("u2ftokens")]
		[JsonPropertyName("u2ftokens")]
		public List<object> U2ftokens { get; set; }

		[JsonProperty("user_id")]
		[JsonPropertyName("user_id")]
		public string UserId { get; set; }

		[JsonProperty("username")]
		[JsonPropertyName("username")]
		public string Username { get; set; }

		[JsonProperty("webauthncredentials")]
		[JsonPropertyName("webauthncredentials")]
		public List<object> Webauthncredentials { get; set; }
	}

	public class Root
	{
		[JsonProperty("metadata")]
		[JsonPropertyName("metadata")]
		public Metadata Metadata { get; set; }

		[JsonProperty("response")]
		[JsonPropertyName("response")]
		public List<Response> Response { get; set; }

		[JsonProperty("stat")]
		[JsonPropertyName("stat")]
		public string Stat { get; set; }
	}


}