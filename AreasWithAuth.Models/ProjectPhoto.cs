using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreasWithAuth.Models
{
	[Table("ProjectPhotos")]
	public class ProjectPhoto : ModelBase
	{
		public string? FileName { get; set; }
		public Guid? ProjectId { get; set; }
		public virtual Projects Project { get; set; }
		
	}
}