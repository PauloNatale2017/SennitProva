using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sennit.Domain.MapEntities.EntitiesViewModel
{
    [Table("Cupon")]
    public class CuponViewModel
    {

        [Required]
        [Display(Name = "Premio")]
        [DataType(DataType.Text)]
        public string Premio { get; set; }

        [Required]
        [Display(Name = "descricao")]
        [DataType(DataType.MultilineText)]
        public string descricao { get; set; }

        public int? Id_usuario { get; set; }

        [Required]
        [Display(Name = "Nome Usuario")]
        [DataType(DataType.Text)]
        public string nome_usuario { get; set; }


        [Required]
        [Display(Name = "Codigo do Cupon")]
        [DataType(DataType.Text)]
        public string CodigoCupon { get; set; }

        [Required]
        [Display(Name = "Cupon Premiado")]
        public bool CUPON_PREMIADO { get; set; }


    }
}
