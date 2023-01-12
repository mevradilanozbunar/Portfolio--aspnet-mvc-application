using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreasWithAuth.Models
{
    [Table("Skills")]
    public class Skills : ModelBase
    {
       
        public string? name { get; set; }
       
        public int? percentDegree { get; set; }
    }
}
