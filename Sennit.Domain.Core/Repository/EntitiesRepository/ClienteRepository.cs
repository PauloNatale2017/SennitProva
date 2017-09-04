using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sennit.Domain.Core.Repository.EntitiesRepository
{
    public class ClienteRepository : Repository<Sennit.Domain.MapEntities.Entities.Cliente>
    {
        public ClienteRepository() : base(new DataAccessLayer.dbContext.dbContext())
        {

        }
    }
}
