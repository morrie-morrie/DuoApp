namespace ApiConnectionLibrary.DuoApi.Models
{
	public interface IConfigModel
	{
		string? apiHost { get; set; }
		string? apiKey { get; set; }
		string? apiSecret { get; set; }
	}
}