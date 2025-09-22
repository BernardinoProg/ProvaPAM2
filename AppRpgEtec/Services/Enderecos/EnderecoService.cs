using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using AppRpgEtec.Models;


namespace AppRpgEtec.Services.Enderecos
{
    internal class EnderecoService
    {
        private readonly Request _request;
        private const string apiUrlBase = "https://nominatim.openstreetmap.org/search?format=json&q=";
     

        public EnderecoService() 
        {
            _request = new Request();
        }

        public async Task<Endereco> GetEnderecoCep(string cep)
        {
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage response = await httpClient.GetAsync(apiUrlBase+cep);
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new System.Exception(serialized);
            Endereco result = await Task.Run(() => JsonConvert.DeserializeObject<Endereco>(serialized));
            return result;
        }
    }
}
