using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.Entities
{
    [Table("Dictionary")]
    public class DictionaryEntity : BaseEntity
    {
        public string Name { get; set; }

        /// <summary>
        /// 简称，缩写
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// 1 OK，0 Deleted
        /// </summary>
        public int DelFlag { get; set; }

        public int ParId { get; set; }


        #region NotMapped
        [NotMapped]
        public List<DictionaryEntity> SubList { get; set; }
        #endregion
    }
}
