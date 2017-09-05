
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
    [RoutePrefix("api/webapi/Cliente")]
    public class ClienteController : ApiController
    {

        private readonly RepositoryGeneric _rep = new RepositoryGeneric();

        //[System.Web.Http.Authorize]
        [System.Web.Http.AcceptVerbs("GET")]
        public List<Cliente> GetClientes()
        {
            return _rep._ClienteRepository.GetAll().ToList();
        }
    }
}
