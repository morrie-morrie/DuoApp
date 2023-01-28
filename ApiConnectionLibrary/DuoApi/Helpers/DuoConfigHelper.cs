using ApiConnectionLibrary.DuoApi.Models;
using System.Configuration;

namespace ApiConnectionLibrary.DuoApi.Helpers
{
	public class DuoConfigHelper
	{
		public static ConfigModel ReadAppConfig()
		{
			var config = new ConfigModel();

			try
			{
				config.apiHost = ConfigurationManager.AppSettings["ApiHost"];
				config.apiKey = ConfigurationManager.AppSettings["ApiKey"];
				config.apiSecret = ConfigurationManager.AppSettings["ApiSecret"];
			}
			catch (ConfigurationErrorsException)
			{
				throw;
			}
			
			return config;
		}




	}
}
