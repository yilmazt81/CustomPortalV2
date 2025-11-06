using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.Model;
using CustomPortalV2.Model.DTO;
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
        private string apiEndPoint = "https://localhost:7232";
        private string token = string.Empty;
        HttpClient client = null;

        public string Token
        {
            get
            {

                return token;
            }
            set
            {
                token = value;

                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
        }


        public RestApiHelper()
        {

            client = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            System.Net.Http.Headers.ProductHeaderValue productInfoHeaderValue = new System.Net.Http.Headers.ProductHeaderValue("OfficeAddOn", "7.0.0.1");
            client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue(productInfoHeaderValue));
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("deflate"));

        }

        public LoginReturn LoginUser(string username, string password, string companyCode)
        {
            var url = apiEndPoint + "/api/Login";
            var loginObj = new LoginRequest
            {
                UserName = username,
                password = password,
                CompanyCode = companyCode,
            };
            var result = PostRequest<LoginReturn, LoginRequest>(url, loginObj);
            this.token = result.token;

            return result;
        }

        public DefaultReturn<List<FormDefination>> GetFormDefination()
        {
            var url = apiEndPoint + "/api/FormDefination";
            return GetRequest<DefaultReturn<List<FormDefination>>>(url);
        }

        public DefaultReturn<List<FormGroup>> GetFormDefinationGroup(int formDefinationId)
        {
            var url = apiEndPoint + $"/api/FormDefination/GetFormGroups?formDefinationId={formDefinationId}";

            return GetRequest<DefaultReturn<List<FormGroup>>>(url);
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

        public DefaultReturn<List<FormDefinationField>> GetFormGroupDefinationField(int formGroupId)
        {
            var url = apiEndPoint + $"/api/FormDefination/GetGroupFields/{formGroupId}";

            return GetRequest<DefaultReturn<List<FormDefinationField>>>(url);
        }

        public DefaultReturn<List<ComboBoxItem>> GetComboBoxItems(string tagName)
        {
            var url = apiEndPoint + $"/api/FormDefination/GetComboBoxItems/{tagName}";

            return GetRequest<DefaultReturn<List<ComboBoxItem>>>(url);
        }

        public DefaultReturn<List<FormDefinationField>> GetFormDefinationAllField(int formdefinationId)
        {
            var url = apiEndPoint + $"/api/FormDefination/GetFormDefinationAllField/{formdefinationId}";

            return GetRequest<DefaultReturn<List<FormDefinationField>>>(url);
        }
    }
}
