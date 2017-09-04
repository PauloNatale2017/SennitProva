using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.Entity.ModelConfiguration.Conventions;

namespace Sennit.DataAccessLayer.dbContext
{
    public  class dbContext : DbContext
    {
        public dbContext() : base("name=dbLocal") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Sennit.Domain.MapEntities.Entities.Login> _Login { get; set; }
        public DbSet<Sennit.Domain.MapEntities.Entities.Cliente> _Cliente { get; set; }
        public DbSet<Sennit.Domain.MapEntities.Entities.Cupon> _Cupon { get; set; }
    }
}
