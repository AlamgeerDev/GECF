using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using GECF.Utility;

namespace GECF.Services
{
	public class HttpServiceClient: System.Net.Http.HttpClient
    {
        public HttpServiceClient()
        {
        }
        private static HttpServiceClient Instance;
        // Returns the HttpServiceClient instance with authentication token appended
        public static async Task<HttpServiceClient> GetInstance()
        {

            string accessToken = await getToken();
            Instance = new HttpServiceClient();
            Instance.Timeout = new TimeSpan(0, 0, IConstants.RequestTimeout);
            Debug.WriteLine("HttpServiceClient/AccessToken: " + accessToken);
            Instance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            Debug.WriteLine("HttpServiceClient/UserName: " + ICUtility.mUser.EmailAddress);
            Instance.DefaultRequestHeaders.Add("X-Infor-MobileConcierge", ICUtility.mUser.EmailAddress);
            return Instance;
        }
        private static async Task<String> getToken()
        {
            bool IsTokenValid = true;
            if (IsTokenValid)
            {
                ICUtility.mUser.AccessToken = Preferences.Get(IConstants.KeyPropertyAccessToken,"");
                return ICUtility.mUser.AccessToken;
            }
            else
            {
                var result = await GECFAPI.Instance.DoLogin(ICUtility.mUser.EmailAddress.Trim(), ((string)ICUtility.mUser.Password).Trim());
                if (result.Item1)
                {
                    return ICUtility.mUser.AccessToken;
                }
            }
            return null;
        }
    }
}

