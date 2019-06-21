using MyMovie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.ServiceInterface
{
    public interface IArticleService
    {

        List<ArticleEntity> GetPageList(int pageIndex, int pageSize, out int count, string key);

        bool AddEntity(ArticleEntity entity);

        List<ArticleEntity> GetEntities(int movideId, int count, out int total);

        List<ArticleEntity> GetEntities(int movideId);

        ArticleEntity GetEntity(int id);

        List<ArticleEntity> GetEntitiesByUserId(int userId);

        bool Delete(ArticleEntity entity);

        bool Update(ArticleEntity entity);
    }
}
