using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Json;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;

namespace ApiClient
{
    public class DuoApiClient
    {
        private TDB.Crypto Crypto;
        private CompanyAccounting Account;

        public DuoApiClient(TDB.Crypto crypto)
        {
            Crypto = crypto;
            Account = new CompanyAccounting(CompanyAccounting.DUO);
        }

        public DuoUsers GetUsers()
        {
            DuoResult result = DuoInterface.GetUsers(Account, Crypto);

            if (result != null && result.response.Count > 0)
            {
                DuoUsers users = result.response;
                return users;
            }

            return null;
        }

        public DuoCheck GetIntegrationStatus()
        {
            DuoCheck result = DuoInterface.GetIntegrationStatus(Account, Crypto);

            if (result != null) return result;

            return null;
        }

    }

    public class DuoCheck
    {
        public string stat { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public string message_detail { get; set; }
        public DuoSummary response = new DuoSummary();
    }

    public class DuoSummary
    {
        public int admin_count { get; set; }
        public int integration_count { get; set; }
        public int telephony_credits_remaining { get; set; }
        public int user_count { get; set; }
        public int user_pending_deletion_count { get; set; }
    }

    public class DuoUsers : List<DuoUser> { }

    public class DuoResult
    {
        public string stat { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public string message_detail { get; set; }
        public string RawResult = string.Empty;
        public DuoMetadata metadata = new DuoMetadata();
        public DuoUsers response = new DuoUsers();
    }

    public class DuoUser
    {
        public string alias1 { get; set; }
        public string alias2 { get; set; }
        public string alias3 { get; set; }
        public string alias4 { get; set; }
        public double created { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public List<string> groups = new List<string>();
        public bool is_enrolled { get; set; }
        public double? last_directory_sync { get; set; }
        public double? last_login { get; set; }
        public string lastname { get; set; }
        public string notes { get; set; }
        public List<string> phone = new List<string>();
        public string realname { get; set; }
        public string status { get; set; }
        public List<string> tokens = new List<string>();
        public List<string> u2ftokens = new List<string>();
        public string user_id { get; set; }
        public string username { get; set; }
        public List<string> webauthncredentials = new List<string>();
    }

    public class DuoMetadata
    {
        public int total_objects { get; set; }
    }

    public class DuoGateway
    {
        private CompanyAccounting Account;
        private TDB.Crypto Crypto;
        public string AccessToken;
        public Auth AuthType;
        private string Path;

        public enum Mode
        {
            GET, POST
        }

        public enum Auth
        {
            BASIC
        }

        public DuoGateway(CompanyAccounting account, TDB.Crypto crypto, string path)
        {
            Account = account;
            Crypto = crypto;
            AuthType = Auth.BASIC;
            Path = path;
        }

        public string Get(string apiUrl)
        {
            return Send(Mode.GET, apiUrl, new StringBuilder());
        }

        public string Post(string apiUrl, StringBuilder content)
        {
            return Send(Mode.POST, apiUrl, content);
        }

        public string Send(Mode mode, string apiUrl, StringBuilder content)
        {
            System.Net.WebClient manualWebClient = new System.Net.WebClient();
            string date_string = DateToRFC822(DateTime.UtcNow);

            if (AuthType == Auth.BASIC)
            {
                manualWebClient.Headers.Add("Authorization", string.Format("Basic {0}", GetSign(mode.ToString(), Path, string.Empty, date_string)));
                manualWebClient.Headers.Add("X-Duo-Date", date_string);
                manualWebClient.Headers.Add("User-Agent", "DuoAPICSharp/1.0 (Microsoft Windows NT 6.2.9200.0; .NET 4.0.30319.42000)");
                manualWebClient.Headers.Add("Accept", "application/json");
            }
            else
                return null;

            byte[] bytRetData = new byte[] { };
            switch (mode)
            {
                case Mode.POST:
                    byte[] bytArgA = System.Text.Encoding.UTF8.GetBytes(content.ToString());
                    bytRetData = manualWebClient.UploadData(apiUrl, "POST", bytArgA);
                    break;
                case Mode.GET:
                    bytRetData = manualWebClient.DownloadData(apiUrl);
                    break;
            }

            return System.Text.Encoding.UTF8.GetString(bytRetData);
        }

        private string DateToRFC822(DateTime date)
        {
            // Can't use the "zzzz" format because it adds a ":"
            // between the offset's hours and minutes.
            string date_string = date.ToString(
                "ddd, dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            int offset = 0;
            // set offset if input date is not UTC time.
            if (date.Kind != DateTimeKind.Utc)
            {
                offset = TimeZoneInfo.Local.GetUtcOffset(date).Hours;
            }
            string zone;
            // + or -, then 0-pad, then offset, then more 0-padding.
            if (offset < 0)
            {
                offset *= -1;
                zone = "-";
            }
            else
            {
                zone = "+";
            }
            zone += offset.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
            date_string += " " + zone.PadRight(5, '0');
            return date_string;
        }

        private string GetSign(string method, string path, string canon_params, string date)
        {
            string canon = this.CanonicalizeRequest(method,
                                                    path,
                                                    canon_params,
                                                    date);
            string sig = this.HmacSign(canon);
            string auth = Account.ClientID + ':' + sig;

            return Encode64(auth);
        }

        private string HmacSign(string data)
        {
            string test = Account.SecretKey_Get(Crypto);
            byte[] key_bytes = ASCIIEncoding.ASCII.GetBytes(Account.SecretKey_Get(Crypto));
            HMACSHA512 hmac = new HMACSHA512(key_bytes);

            byte[] data_bytes = ASCIIEncoding.ASCII.GetBytes(data);
            hmac.ComputeHash(data_bytes);

            string hex = BitConverter.ToString(hmac.Hash);
            return hex.Replace("-", "").ToLower();
        }

        private string CanonicalizeRequest(string method,
                                             string path,
                                             string canon_params,
                                             string date)
        {
            string[] lines = {
                date,
                method.ToUpperInvariant(),
                Account.Endpoint.ToLower(),
                path,
                canon_params,
            };
            string canon = String.Join("\n",
                                       lines);
            return canon;
        }

        private static string Encode64(string plaintext)
        {
            byte[] plaintext_bytes = ASCIIEncoding.ASCII.GetBytes(plaintext);
            string encoded = System.Convert.ToBase64String(plaintext_bytes);
            return encoded;
        }
    }

    public class DuoInterface
    {
        public static DuoResult GetUsers(CompanyAccounting account, TDB.Crypto crypto)
        {
            if (account == null) return null;
            
            string DuoBaseApiUrl = String.Format("https://{0}", account.Endpoint);
            string apiPath = "/admin/v1/users";
            string apiUrl = string.Concat(DuoBaseApiUrl, apiPath);
            DuoGateway gateway = new DuoGateway(account, crypto, apiPath);
            string resultJson = gateway.Get(apiUrl);

            DuoResult result = Newtonsoft.Json.JsonConvert.DeserializeObject<DuoResult>(resultJson);
            return result;
        }

        public static DuoCheck GetIntegrationStatus(CompanyAccounting account, TDB.Crypto crypto)
        {
            if (account == null) return null;

            string DuoBaseApiUrl = String.Format("https://{0}", account.Endpoint);
            string apiPath = "/admin/v1/info/summary";
            string apiUrl = string.Concat(DuoBaseApiUrl, apiPath);

            DuoGateway gateway = new DuoGateway(account, crypto, apiPath);
            string resultJson = gateway.Get(apiUrl);

            DuoCheck result = Newtonsoft.Json.JsonConvert.DeserializeObject<DuoCheck>(resultJson);
            return result;
        }
    }
}
