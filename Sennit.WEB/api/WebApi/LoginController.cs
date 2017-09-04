using Sennit.Domain.MapEntities.Entities;
using Sennit.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;

namespace Sennit.WEB.api.WebApi
{
    public class LoginController : ApiController
    {
        private readonly RepositoryGeneric _rep = new RepositoryGeneric();


        //[System.Web.Http.Authorize]
        [System.Web.Http.AcceptVerbs("GET")]
        public List<Login> GetAllUsers()
        {
           
            return  _rep._LoginRepository.GetAll().ToList();
        }

        // GET api/<controller>
        //[System.Web.Http.Authorize]
        [System.Web.Http.AcceptVerbs("POST")]
        public Login getAccount(Login Cadastro)
        {           
            var User = _rep._LoginRepository.Get(d => d.User == Cadastro.User || d.Password == Cadastro.Password).SingleOrDefault();
            if(User != null)
            {
               
                return User;
            } else {
                return null; }
             
        }

        [System.Web.Http.Authorize]
        [System.Web.Http.AcceptVerbs("POST")]
        public string Cadastrar(Cliente entity)
        {
            try
            {
                var st = _rep._ClienteRepository.GetAll().Where(d => d.CPF == entity.CPF).SingleOrDefault();
                if (st != null) { return  "CPF JA CADASTRADO"; };
                               
                entity.DataAtualizacao = DateTime.Now;
                entity.DataCriacao = DateTime.Now;

                 _rep._ClienteRepository.Add(entity);
                 _rep._LoginRepository.Add(new Login
                 {
                     User = entity.Email,
                     Password = entity.CPF,
                     DataAtualizacao = DateTime.Now,
                     DataCriacao = DateTime.Now
                 });

                return "OK";
            }
            catch(Exception ex)
            {                
                return "Error Inexperado";
            }

        }
               

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}