using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreasWithAuth.Models
{
    [Table("Educations")]
    public class Education : ModelBase
    {
       
        public DateTime? StartDate { get; set; } 
       
        public DateTime? EndDate { get; set; } 
      
        public string? Location { get; set; } 
     
        public string? UniversityName { get; set; }
     
        public string? Department { get; set; }
       
        public string? Description { get; set; }
		
	}
}
