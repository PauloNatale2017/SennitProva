using Sennit.Domain.MapEntities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sennit.Domain.MapEntities.EntitiesViewModel
{
    [Table("Cliente")]
    public class ClienteViewModelcs
    {
        [Required]
        [Display(Name = "Nome")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [Required]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string telefone { get; set; }

     
        public string access { get; set; }
        //[Required]
        //[Display(Name = "Cupons")]
        //[DataType(DataType.MultilineText)]
        //public List<Cupon> Cupons { get; set; }

        //public int id_Login { get; set; }
    }
}
