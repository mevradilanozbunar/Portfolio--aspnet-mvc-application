using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreasWithAuth.Models
{
    [Table("Experiences")]
    public class Experience:ModelBase
    {
       
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
       
        public string? Location { get; set; }
        
        public string? CompanyName { get; set; }
       
        public string? Position { get; set; }
        
        public string? Description { get; set; }
		public virtual ICollection<Profile> Profiles { get; set; }

	}
}
