using forgeSample.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace forgeSample.Controllers
{
    public static class ForgeAuthUtilities
    {
        public static async Task<ForgeOAuthToken> GetTokenAsync(string code)
        {
            var localVarFormParams = new Dictionary<String, String> {
                {"client_id", "wiIdAHuoFZJYnaCNhxL20pZG4fVkZokz" },
                {"client_secret", "typW3w02FrkmvVKF" },
                {"grant_type", "authorization_code" },
                {"code", code },
                {"redirect_uri", "http://localhost:3000/api/forge/callback/oauth" }
            };
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                client.DefaultRequestHeaders.UserAgent.Clear();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("d3tech-fusion-sdk");
                HttpResponseMessage httpResponseMessage = await client.PostAsync("https://developer.api.autodesk.com/authentication/v1/gettoken",
                    new FormUrlEncodedContent(localVarFormParams));
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    // deserialize response into token
                    var strResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(strResponseBody))
                    {
                        var token = JsonConvert.DeserializeObject<ForgeOAuthToken>(strResponseBody);
                        return token;
                    }
                }
            }
            return new ForgeOAuthToken();
        }
    }
}
