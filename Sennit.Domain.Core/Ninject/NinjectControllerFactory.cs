using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Mvc;

namespace Sennit.Domain.Core.Ninject
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernnel;

        public NinjectControllerFactory()
        {
            ninjectKernnel = new StandardKernel();
            AdBinding();
        }

        private void AdBinding()
        {
            ninjectKernnel.Bind<Sennit.Domain.Core.IRepository.IRepository<Sennit.Domain.MapEntities.Entities.Login>>().To<Sennit.Domain.Core.Repository.EntitiesRepository.LoginRepository>();
            ninjectKernnel.Bind<Sennit.Domain.Core.IRepository.IRepository<Sennit.Domain.MapEntities.Entities.Cliente>>().To<Sennit.Domain.Core.Repository.EntitiesRepository.ClienteRepository>();
            ninjectKernnel.Bind<Sennit.Domain.Core.IRepository.IRepository<Sennit.Domain.MapEntities.Entities.Cupon>>().To<Sennit.Domain.Core.Repository.EntitiesRepository.CuponRepository>();
        }
    }
}
