using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreasWithAuth.Models
{
    public class ModelBase
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public DateTime DateCreated { get; set; }= DateTime.Now;
        public DateTime DateModified { get; set; }=DateTime.Now;
        public bool IsActive { get; set; }= true;
        public bool IsDeleted { get; set; }=false;
    }
}
