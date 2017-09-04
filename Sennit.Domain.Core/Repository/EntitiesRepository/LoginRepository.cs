using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sennit.Domain.Core.Repository.EntitiesRepository
{
    public class LoginRepository : Repository<Sennit.Domain.MapEntities.Entities.Login>
    {
        public LoginRepository() : base(new DataAccessLayer.dbContext.dbContext())
        {

        }
        

    }
}
