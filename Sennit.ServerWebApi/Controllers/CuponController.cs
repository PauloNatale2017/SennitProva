
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
    [RoutePrefix("api/webapi/Cupon")]
    public class CuponController : ApiController
    {
        private readonly RepositoryGeneric _rep = new RepositoryGeneric();

        [System.Web.Http.AcceptVerbs("GET")]
        public List<Cupon> GetCupons()
        {
            return _rep._CuponRepository.GetAll().ToList();
        }

        [System.Web.Http.AcceptVerbs("POST")]
        public bool CadastroCupon(Cupon entity)
        {
            try
            {
                entity.DataCriacao = DateTime.Now;
                entity.DataAtualizacao = DateTime.Now;
                _rep._CuponRepository.Add(entity);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}
