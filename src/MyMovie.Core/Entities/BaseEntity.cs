using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreateDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public DateTime CreateDate { get; private set; }


    }
}
