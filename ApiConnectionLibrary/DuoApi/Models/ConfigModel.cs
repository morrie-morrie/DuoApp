namespace ApiConnectionLibrary.DuoApi.Models
{
	public class ConfigModel : IConfigModel
	{
		public string? apiHost { get; set; }
		public string? apiKey { get; set; }
		public string? apiSecret { get; set; }
	}
}