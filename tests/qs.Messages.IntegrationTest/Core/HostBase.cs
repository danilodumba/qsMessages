using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;

namespace qs.Messages.IntegrationTest.Core
{
    public abstract class HostBase
    {
        protected readonly HttpClient _client;
        private readonly string _api;
        const string ENVIRONMENT = "Development";
        protected HostBase(string api)
        {
             var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{ENVIRONMENT}.json", optional: true)
                .AddEnvironmentVariables();
                
            var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => {
                       
                    builder.UseConfiguration(configurationBuilder.Build());

                    //Caso queira usar o teste em um banco de dados real, basta comentar o trecho de codigo abaixo e setar o Environment para o ambiente desejado.
                    builder.ConfigureServices(services =>
                    {
                        // services.AddScoped<ILogQueryRepository, LogQueryRepositoryInMemoryMock>();

                        // var context = services.SingleOrDefault(
                        //     d => d.ServiceType ==
                        //         typeof(DbContextOptions<LogContext>));

                        // services.Remove(context);

                        // services.AddDbContext<LogContext>(options =>
                        // {
                        //     options.UseInMemoryDatabase("InMemoryDbForTesting");
                        // });
                    });
                });

            _client = factory.CreateClient();
            _api = api;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caminho">Informe o caminho completo (/api/controller/.)</param>
        /// <returns></returns>
        protected async Task<HttpResponseMessage> Get(string caminho)
        {
            var api = string.IsNullOrEmpty(caminho) ? _api : caminho;
            return await _client.GetAsync(api);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caminho">Informe o caminho completo (/api/controller/.)</param>
        /// <param name="objeto">Objeto nao serializado</param>
        /// <returns></returns>
        protected async Task<HttpResponseMessage> Post(string caminho, object objeto)
        {
            var api = string.IsNullOrEmpty(caminho) ? _api : caminho;
            var json =  JsonConvert.SerializeObject(objeto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _client.PostAsync(api, content);
        }

        protected async Task<HttpResponseMessage> Post(object objeto)
        {
            return await this.Post("", objeto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caminho">Informe o caminho completo (/api/controller/.)</param>
        /// <param name="objeto"></param>
        /// <returns></returns>
        protected async Task<HttpResponseMessage> Put(string caminho, object objeto)
        {
            var api = string.IsNullOrEmpty(caminho) ? _api : caminho;
            var json =  JsonConvert.SerializeObject(objeto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _client.PutAsync(api, content);
        }

         protected async Task<HttpResponseMessage> Put(object objeto)
        {
            return await this.Put("", objeto);
        }

        protected async Task<T> ResultResponse<T>(HttpResponseMessage response)
        {
            var result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            return result;
        }

        // protected async Task Logar(LoginModel model)
        // {
        //     var json =  JsonConvert.SerializeObject(model);
        //     var content = new StringContent(json, Encoding.UTF8, "application/json");
        //     var response = await _client.PostAsync("api/auth", content);

        //     response.EnsureSuccessStatusCode();

        //     var usuario = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
        //     string token = usuario.token; 
        //     _client.DefaultRequestHeaders.Clear();
        //     _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        // }

        // private async Task GerarCabecalhoBearerToken()
        // {
        //     var token = await Logar();
        //     _client.DefaultRequestHeaders.Clear();
        //     _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        // }

        // private async Task<string> Logar()
        // {
        //     await ValidarUsuarioAdmin();

        //     var loginModel = new LoginModel
        //     {
        //         UserName = "admin",
        //         Password = "admin"
        //     };

        //     var json =  JsonConvert.SerializeObject(loginModel);
        //     var content = new StringContent(json, Encoding.UTF8, "application/json");
        //     var response = await _client.PostAsync("api/auth", content);

        //     response.EnsureSuccessStatusCode();

        //     var usuario = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

        //     return usuario.token;
        // }
        
        // private async Task ValidarUsuarioAdmin()
        // {
        //     var api = "api/user/admin";
        //     var response = await _client.GetAsync(api);
        //     response.EnsureSuccessStatusCode();
        // }
    }
}