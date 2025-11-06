using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.Lang;
using CustomPortalV2.DataAccessLayer.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class AppLangService : IAppLangService
    {
        IAppLangRepository _appLangRepository;
        string key = "6cf51c44bd8e4f049317242af374b4cb";
        string endpoint = "https://api.cognitive.microsofttranslator.com";

        string location = "westeurope";
        public AppLangService(IAppLangRepository appLangRepository)
        {
            _appLangRepository = appLangRepository;
        }
        public List<AppLang> GetAppLangs()
        {

            return _appLangRepository.GetAppLangs();
        }

        public async Task<TranslateTextReturn> TranslateText(TranslateTextDTO translateText)
        {
            string route = $"/translate?api-version=3.0&from={translateText.SourceLangeuage}&to={translateText.TargetLanguage}";
            object[] body = new object[] { new { Text = translateText.Text } };
            var requestBody = JsonConvert.SerializeObject(body);
            string result = string.Empty;
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                // location required if you're using a multi-service or regional (not global) resource.
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                result = await response.Content.ReadAsStringAsync();

            }
            result = result.Substring(1, result.Length - 2);
            return JsonConvert.DeserializeObject<TranslateTextReturn>(result);
        }
    }
}
