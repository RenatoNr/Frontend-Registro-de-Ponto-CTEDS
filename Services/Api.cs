using Frontend_Registro_de_Ponto_CTEDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Frontend_Registro_de_Ponto_CTEDS.Services
{
    public class Api
    {
        private static HttpClient api = new HttpClient();
        private string uri = "https://localhost:7222";


        public Api()
        {
            api.BaseAddress = new Uri(uri);
            // api.DefaultRequestHeaders.Accept
          //    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
          //  api.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");
           // api.DefaultRequestHeaders.Add("X-Requested-By", "AM-Request");
            //api.DefaultRequestHeaders.
            api.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<HttpResponseMessage> Get(string uri)
        {
            return await api.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> Post(User obj, string url)
        {

            //var us = new User()
            //{
            //    Id = 121212,
            //    Name = "Teste",
            //    Cpf = "1221",
            //    IsAdmin = false,
            //    Password = "11221",
            //    Photo = "121211"
            //};

            var formContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("id", "1231"),
                new KeyValuePair<string, string>("name","Name"),
                new KeyValuePair<string, string>("cpf","9817"),
                new KeyValuePair<string, string>("employeePost","CEO"),
                new KeyValuePair<string, string>("password","Senha"),
               // new KeyValuePair<string, string>("Photo","Teste"),
                //new KeyValuePair<string, string>("photo",$"as")
            });
            var response = await api.PostAsJsonAsync<User>(url, obj);
            return  response;
        }
    }
}
