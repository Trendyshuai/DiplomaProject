using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.Entities
{
    [Table("Movie_Info")]
    public class MovieEntity : BaseEntity
    {
        public string Name { get; set; }

        /// <summary>
        /// 导演
        /// </summary>
        public string Director { get; set; }

        public string Language { get; set; }

        /// <summary>
        /// 上映时间
        /// </summary>
        public string ReleaseDate { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }


        public string Description { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public float Rate { get; set; }

        /// <summary>
        /// 海报地址
        /// </summary>
        public string PosterURL { get; set; }

        /// <summary>
        /// 删除标识，1上架，2移除
        /// </summary>
        public int DelFlag { get; set; }
    }
}
