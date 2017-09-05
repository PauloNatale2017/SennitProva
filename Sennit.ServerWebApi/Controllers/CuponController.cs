
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
                entity.PREMIO_SORTEADO = false;
                _rep._CuponRepository.Add(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public class CUPONCADASTRO
        {
            public string CODIGOCUPON { get; set; }
            public string CPF { get; set; }
        }

        public class Listas
        {
            public List<Cupon> cupons = new List<Cupon>();
            public List<Cliente> Clientes = new List<Cliente>();
        }

        [System.Web.Http.AcceptVerbs("POST")]
        public Listas CadastroCuponPorClienteGet(CUPONCADASTRO entity)
        {
            Listas list = new Listas();
            Cliente cliente = _rep._ClienteRepository.GetAll().Where(d => d.CPF == entity.CPF).SingleOrDefault();
            if(cliente != null)
            {
                list.Clientes.Add(new Cliente
                {
                    Nome = cliente.Nome,
                    Email = cliente.Email,
                    CPF = cliente.CPF,
                    telefone = cliente.telefone,
                    password = cliente.password,
                    id_Login = cliente.id_Login,
                    access = cliente.access,
                    QtdCuponsCadastrados = cliente.QtdCuponsCadastrados
                });
            }
            Cupon Cupons = _rep._CuponRepository.GetAll().Where(d => d.CodigoCupon == entity.CODIGOCUPON).SingleOrDefault();
            if (Cupons != null)
            {
                list.cupons.Add(new Cupon
                {
                    Premio = Cupons.Premio,
                    descricao = Cupons.descricao,
                    Id_usuario = Cupons.Id_usuario,
                    nome_usuario = Cupons.nome_usuario,
                    CodigoCupon = Cupons.CodigoCupon,
                    CUPON_PREMIADO = Cupons.CUPON_PREMIADO,
                    PREMIO_SORTEADO = Cupons.PREMIO_SORTEADO,
                    DataSorteado = Cupons.DataSorteado,
                });
            }
            return list;
        }


        [System.Web.Http.AcceptVerbs("POST")]
        public Cupon CadastroCuponPorCliente(CUPONCADASTRO entity)
        {
            try
            {
                var cliente = _rep._ClienteRepository.GetAll().Where(d => d.CPF == entity.CPF).SingleOrDefault();
                var Cupons = _rep._CuponRepository.GetAll().Where(d => d.CodigoCupon == entity.CODIGOCUPON && d.PREMIO_SORTEADO == false).SingleOrDefault();

                if(cliente.QtdCuponsCadastrados >= 5)
                {
                    return Cupons;
                }
                else if(Cupons != null && cliente.QtdCuponsCadastrados <= 5)
                {
                    Cupons.DataAtualizacao = DateTime.Now;
                    Cupons.nome_usuario = cliente.Nome;
                    Cupons.Id_usuario = cliente.ID;
                    Cupons.PREMIO_SORTEADO = true;
                    Cupons.DataSorteado = DateTime.Now;
                   
                    _rep._CuponRepository.Update(Cupons);
                     cliente.QtdCuponsCadastrados++;
                    _rep._ClienteRepository.Update(cliente);
                    return Cupons;
                }
                else
                {
                    return Cupons;
                }

                return Cupons; 
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }
    }
}
