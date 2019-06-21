using MyMovie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Infrastructure.Data
{
    public class EfDbContext : DbContext
    {

        private readonly static string connStr = ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString;

        /// <summary>
        /// 重写构造时添加连接
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        public EfDbContext() : base(connStr)
        {
            //"server=127.0.0.1;uid=sa;pwd=Kk123456;database=LeeMovie"
        }


        public DbSet<ArticleEntity> ArticleEntities { get; set; }

        public DbSet<CommentEntity> CommentEntities { get; set; }

        public DbSet<DictionaryEntity> DictionaryEntities { get; set; }

        public DbSet<MovieEntity> MovieEntities { get; set; }

        public DbSet<MovieTypeEntity> MovieTypeEntities { get; set; }

        public DbSet<TypeEntity> TypeEntities { get; set; }

        public DbSet<UserEntity> UserEntities { get; set; }

        public DbSet<AdminEntity> AdminEntities { get; set; }
    }
}
