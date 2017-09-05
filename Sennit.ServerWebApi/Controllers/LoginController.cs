
using Sennit.Domain.MapEntities.Entities;
using Sennit.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;



namespace Sennit.ServerWebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/webapi/login")]
    public class LoginController : ApiController
    {

        private readonly RepositoryGeneric _rep = new RepositoryGeneric();

        // POST api/User/Login
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<HttpResponseMessage> LoginUser(Login model)
        {
            // Invoke the "token" OWIN service to perform the login: /api/token
            // Ugly hack: I use a server-side HTTP POST because I cannot directly invoke the service (it is deeply hidden in the OAuthAuthorizationServerHandler class)
            var request = HttpContext.Current.Request;
            var tokenServiceUrl = request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath + "/api/Token";
            using (var client = new HttpClient())
            {
                var requestParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", model.User),
                    new KeyValuePair<string, string>("password", model.Password)
                };

                var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
                var tokenServiceResponse = await client.PostAsync(tokenServiceUrl, requestParamsFormUrlEncoded);
                var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
                var responseCode = tokenServiceResponse.StatusCode;

                var responseMsg = new HttpResponseMessage(responseCode)
                {
                    Content = new StringContent(responseString, Encoding.UTF8, "application/json")
                };

                return responseMsg;
            }
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public List<Login> LogIn()
        {
            return _rep._LoginRepository.GetAll().ToList();
        }


        [System.Web.Http.AcceptVerbs("GET")]
        public List<Login> GetAllUsers()
        {
            return _rep._LoginRepository.GetAll().ToList();
        }

       
        // GET api/<controller>
        //[System.Web.Http.Authorize]
        [System.Web.Http.AcceptVerbs("POST")]
        public Login getAccount(Login Cadastro)
        {           
            if (Cadastro.User != null && Cadastro.Password != null)
            {
                var  User = _rep._LoginRepository.Get(d => d.User == Cadastro.User || d.Password == Cadastro.Password).SingleOrDefault();
                if (User != null)
                {
                    return User;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        //[System.Web.Http.Authorize]
        [System.Web.Http.AcceptVerbs("POST")]
        public string Cadastrar(Cliente entity)
        {
            try
            {
                var st = _rep._ClienteRepository.GetAll().Where(d => d.CPF == entity.CPF).SingleOrDefault();
                if (st != null) { return "CPF JA CADASTRADO"; };

                entity.DataAtualizacao = DateTime.Now;
                entity.DataCriacao = DateTime.Now;
                entity.access = "user";
                entity.QtdCuponsCadastrados = 0;

                _rep._ClienteRepository.Add(entity);
                _rep._LoginRepository.Add(new Login
                {
                    User = entity.Nome,
                    Password = entity.CPF,
                    access = "user",
                    DataAtualizacao = DateTime.Now,
                    DataCriacao = DateTime.Now
                });

                return "OK";
            }
            catch (Exception ex)
            {
                return "Error Inexperado";
            }

        }

        //[System.Web.Http.Authorize]
        [System.Web.Http.AcceptVerbs("GET")]
        public IHttpActionResult Get()
        {
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            var claims = claimsIdentity.Claims.Select(x => new { type = x.Type, value = x.Value });
            return Ok(claims);
        }
    }
}
