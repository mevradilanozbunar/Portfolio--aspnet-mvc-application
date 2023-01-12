using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreasWithAuth.Models
{
    [Table("AppUsers")]
    public class AppUser : ModelBase
    {
		
		[StringLength(50)]
         
		public string? UserName { get; set; }
		[StringLength(50)]
       
		public string? Password { get; set; }
		[StringLength(50)]
        
		public string? Email { get; set; }
        public Guid AppUserRoleId { get; set; }
        public virtual AppUserRole AppUserRole { get; set; }
		[StringLength(50)]
       
		public string? Name { get; set; }

		[StringLength(50)]
        
		public string? Surname{ get; set; }
		
	
	}
}
