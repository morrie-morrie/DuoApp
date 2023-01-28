using RestSharp;

namespace ApiConnectionLibrary.DuoApi.Helpers
{
    public class DuoEndPoint
    {

        public static void GetDuoTenants(out string apiEndpoint, out string requestParams, out Method requestMethod)
        {
            apiEndpoint = "/accounts/v1/account/list";
            requestParams = "";
            requestMethod = Method.Post;
		}
    }
}