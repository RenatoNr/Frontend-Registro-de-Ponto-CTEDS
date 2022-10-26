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
           
            api.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<HttpResponseMessage> Get(string uri)
        {
            return await api.GetAsync(uri);
            
        }

        public async Task<HttpResponseMessage> Post(User obj, string url)
        {

            var response = await api.PostAsJsonAsync<User>(url, obj);
            return response;
        }

        public async Task<HttpResponseMessage> CreateClock(Clock clock, string url)
        {
            var response = await api.PostAsJsonAsync<Clock>(url, clock);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateTime(string url, HttpContent content)
        {
            var response = await api.PostAsync(url, content);
            return response;
        }

        public async Task<HttpResponseMessage> Login(string cpf, string password, string? typeLogin)
            
        {
            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
            if(typeLogin == "admin")
            {
                var response = await api.PostAsync($"{uri}/api/User/Login?cpf={cpf}&password={password}", content);
                return response;
            }
            else
            {
                var response = await api.PostAsync($"{uri}/api/Employee/Login?cpf={cpf}&password={password}", content);
                return response;
            }

            
        }
    }
}
