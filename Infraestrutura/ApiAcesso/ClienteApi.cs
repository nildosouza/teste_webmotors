using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Policy;

namespace ApiAcesso
{
    public class ClienteApi
    {
        private HttpClient _httpClient;
        
        public ClienteApi(string strUri)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(strUri);            
        }

       public string getDados(string strUrlMetodo)
        {
            try
            {
                HttpResponseMessage responseMessage = _httpClient.GetAsync(strUrlMetodo).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content.ReadAsStringAsync().Result;
                }

                return "";
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
