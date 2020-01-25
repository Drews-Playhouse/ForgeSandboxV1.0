using Newtonsoft.Json;
using System;

namespace forgeSample.Controllers
{
    public class ForgeOAuthToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        private int _expiresin;
        [JsonProperty("expires_in")]
        public int ExpiresIn
        {
            get
            {
                return _expiresin;
            }
            set
            {
                _expiresin = value;
                ExpiresOn = DateTimeOffset.Now.AddSeconds(value);
            }
        }
        [JsonProperty("expires_on")]
        public DateTimeOffset ExpiresOn { get; private set; }
        public bool IsValid()
        {
            if (ExpiresOn > DateTimeOffset.Now)
                return true;
            return false;
        }
    }
}