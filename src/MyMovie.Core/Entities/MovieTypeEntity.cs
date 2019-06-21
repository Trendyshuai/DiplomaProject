using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.Entities
{
    [Table("Movie_Type")]
    public class MovieTypeEntity:BaseEntity
    {
        public int MovieId { get; set; }

        public int TypeId { get; set; }
    }
}
