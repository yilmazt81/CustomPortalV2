using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Utils
{
    public class RestApiHelper
    {
        private string apiEndPoint = "https://localhost:7232/ap'";
        HttpClient client = null;


        public RestApiHelper() {

            client = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            System.Net.Http.Headers.ProductHeaderValue productInfoHeaderValue = new System.Net.Http.Headers.ProductHeaderValue("OfficeAddOn", "7.0.0.1");
            client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue(productInfoHeaderValue));
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("deflate"));

        }

        public void GetFormDefinationGroup(int formDefinationId)
        {

        }


        private T PostRequest<T, K>(string url, K obj)
        {

            var jsonClass = JsonConvert.SerializeObject(obj);
            var content = new StringContent(jsonClass, Encoding.UTF8, "application/json");
            T requestReturn;

            var result = client.PostAsync(url, content).Result;
            if (result.IsSuccessStatusCode)
            {
                var str = result.Content.ReadAsStringAsync().Result;
                requestReturn = JsonConvert.DeserializeObject<T>(str);

                return requestReturn;
            }
            else
            {
                throw new Exception("Request Return Code : " + result.StatusCode);

            }
        }
        private T GetRequest<T>(string url)
        {

            T requestReturn;
            var result = client.GetAsync(url).Result;
            if (result.IsSuccessStatusCode)
            {
                var str = result.Content.ReadAsStringAsync().Result;
                requestReturn = JsonConvert.DeserializeObject<T>(str);

                return requestReturn;
            }
            else
            {
                throw new Exception("Request Return Code : " + result.StatusCode);

            }
        }
    }
}
