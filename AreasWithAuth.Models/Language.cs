using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreasWithAuth.Models
{
    [Table("Languages")]
    public class Language : ModelBase
    {
       
        public string? name { get; set; }
       
        public string? level { get; set; }
    }
}
