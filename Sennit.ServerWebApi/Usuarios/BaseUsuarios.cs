using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sennit.ServerWebApi.Usuarios
{
    public static class BaseUsuarios
    {
        public static IEnumerable<Usuario> Usuarios()
        {
            return new List<Usuario>
            {
                new Usuario { Nome = "Fulano", Senha = "1234",
                    Funcoes = new string[] { Funcao.UsuarioNormal } },
                new Usuario { Nome = "Beltrano", Senha = "5678",
                    Funcoes = new string[] { Funcao.Gerente } },
                new Usuario { Nome = "Sicrano", Senha = "0912",
                    Funcoes = new string[] { Funcao.Administrador,
                                                Funcao.Gerente } },
            };
        }
    }
}
