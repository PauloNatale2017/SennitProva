using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sennit.Domain.MapEntities.Entities
{
    public class Login : BaseEntity.EntityBase
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string access { get; set; }

        //enum AuthenticationResources
        //{
        //   InvalidUsername,
        //   InvalidPassword
        //}

        //public static class RegexSettingsHelper
        //{
        //    public const string REGEX_USERNAME = "^[a-z0-9_-]{3,16}$";
        //    public const string REGEX_PASSWORD = "^[a-z0-9_-]{6,18}$";
        //}

        //public Login(string User, string Password, DateTime DataAtualizacao, DateTime DataCriacao)
        //{
        //    Contract.Requires<InvalidCastException>(Regex.IsMatch(User, RegexSettingsHelper.REGEX_USERNAME), AuthenticationResources.InvalidUsername.ToString());
        //    Contract.Requires<InvalidCastException>(Regex.IsMatch(Password, RegexSettingsHelper.REGEX_PASSWORD), AuthenticationResources.InvalidPassword.ToString());

        //    this.User = User;
        //    this.Password = Password;
        //}
    }
}
