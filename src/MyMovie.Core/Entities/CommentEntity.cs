using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.Entities
{
    [Table("Comment")]
    public class CommentEntity : BaseEntity
    {

        public string Conetnt { get; set; }

        public int MovieId { get; set; }

        public int UserId { get; set; }

        public int ParId { get; set; }


        #region NotMapped
        [NotMapped]
        public string NickName { get; set; }

        [NotMapped]
        public string MovieName { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public string ConvertDate { get; set; }

        [NotMapped]
        public MovieEntity Movie { get; set; }
        #endregion

    }
}
