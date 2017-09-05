using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sennit.Domain.MapEntities.Entities
{
    public class Cupon : BaseEntity.EntityBase
    {
        public string Premio { get; set; }
        public string descricao { get; set; }
        public int?   Id_usuario { get; set; }
        public string nome_usuario { get; set; }
        public string CodigoCupon { get; set; }
        public bool CUPON_PREMIADO { get; set; }
        public bool PREMIO_SORTEADO { get; set; }
        public DateTime? DataSorteado { get; set; }
    }
}
