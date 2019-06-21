using MyMovie.Core.Entities;
using MyMovie.Core.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Infrastructure.Data
{
    public class ArticleRepository : Repository<ArticleEntity>, IArticleRepository
    {
        public ArticleRepository(EfDbContext efDbContext) : base(efDbContext)
        {
        }
    }
}
