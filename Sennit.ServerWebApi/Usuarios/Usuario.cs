
namespace Sennit.ServerWebApi.Usuarios
{
    public static class Funcao
    {
        public const string UsuarioNormal = "Usuário Normal";
        public const string Gerente = "Gerente";
        public const string Administrador = "Administrador";
    }

    public class Usuario
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string[] Funcoes { get; set; }
    }
}
