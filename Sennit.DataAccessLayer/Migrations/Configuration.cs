namespace Sennit.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sennit.DataAccessLayer.dbContext.dbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Sennit.DataAccessLayer.dbContext.dbContext context)
        {
            context._Cliente.AddOrUpdate(x => x.ID, new Domain.MapEntities.Entities.Cliente
                      {
                        id_Login = null,
                        Nome = "Admin",
                        Email = "sennit@sennit.com",
                        CPF = "12345",
                        telefone = "44121939",
                        password = "",
                        access = "Admin",
                        DataCriacao = DateTime.Now,
                        DataAtualizacao = DateTime.Now,                       
                        QtdCuponsCadastrados = 0});

            context._Login.AddOrUpdate(x => x.ID, new Domain.MapEntities.Entities.Login
            {
                User = "Admin",
                Password = "Admin",
                access = "Admin",
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now               
            });
        }
    }
}
