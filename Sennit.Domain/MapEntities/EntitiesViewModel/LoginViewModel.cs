using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sennit.Domain.MapEntities.EntitiesViewModel
{
    [Table("Login")]
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Nome")]
        public String User { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

           
        public string access { get; set; }

    }
}
