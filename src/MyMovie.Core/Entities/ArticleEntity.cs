using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.Entities
{
    [Table("Article")]
    public class ArticleEntity : BaseEntity
    {

        public string Title { get; set; }

        public string Content { get; set; }

        public int MovieId { get; set; }

        public int UserId { get; set; }

        public string Text { get; set; }

        /// <summary>
        /// 匿名
        /// </summary>
        public int IsAnonymous { get; set; }


        #region NotMapped
        [NotMapped]
        public string NickName { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public string MovieName { get; set; }

        [NotMapped]
        public string ConvertDate { get; set; }

        [NotMapped]
        public MovieEntity Movie { get; set; }
        #endregion
    }
}
