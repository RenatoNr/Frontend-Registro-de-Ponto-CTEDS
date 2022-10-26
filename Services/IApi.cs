using Frontend_Registro_de_Ponto_CTEDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Frontend_Registro_de_Ponto_CTEDS.Services
{
    public interface IApi
    {
        public Task<HttpResponseMessage> Get(string url);
        public Task<HttpResponseMessage> Post(User user, string createUserUrl);
        public Task<HttpResponseMessage> CreateClock(Clock url, string clockUrl);
        public Task<HttpResponseMessage> UpdateTime(string url, HttpContent content);
        public Task<HttpResponseMessage> Login(string cpf, string password);
        public Task<HttpResponseMessage> LoginAdm(string cpf, string password);

    }
}
