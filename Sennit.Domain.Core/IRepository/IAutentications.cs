using Sennit.Domain.MapEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sennit.Domain.Core.IRepository
{
   
    public interface IAutentications : IRepository<Login>
    {
        Login Authenticate(string User, string Password);
    }
}
