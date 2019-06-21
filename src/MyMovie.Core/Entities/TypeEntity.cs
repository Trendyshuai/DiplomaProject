using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.Entities
{
    [Table("Types")]
    public class TypeEntity : BaseEntity
    {

        public string TypeName { get; set; }

        public string DelFlag { get; set; }

        public int Sort { get; set; }
    }
}
