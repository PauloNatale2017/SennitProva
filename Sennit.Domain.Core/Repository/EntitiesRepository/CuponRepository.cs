using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sennit.Domain.Core.Repository.EntitiesRepository
{
    public class CuponRepository : Repository<Sennit.Domain.MapEntities.Entities.Cupon>
    {
        public CuponRepository() : base(new DataAccessLayer.dbContext.dbContext())
        {

        }
    }
}
