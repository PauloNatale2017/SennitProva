using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sennit.Domain.MapEntities.Entities
{
    public class Cliente : BaseEntity.EntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string telefone { get; set; }
        public string password { get; set; }
        //public List<Cupon> Cupons { get; set; }
        public int?  id_Login { get; set; }
        public string access { get; set; }

        //public Cliente()
        //{
        //    this.Cupons = new List<Cupon>();
        //}

    }
}
