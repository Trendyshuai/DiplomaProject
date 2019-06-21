using MyMovie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.ServiceInterface
{
    public interface ICommentService
    {
        List<CommentEntity> GetPageList(int pageIndex, int pageSize, out int count, string key);

        bool AddEntity(CommentEntity entity);

        List<CommentEntity> GetEntities(int movideId, int count, out int total);

        List<CommentEntity> GetEntities(int movieId);

        List<CommentEntity> GetEntitiesByUserId(int userId);

        CommentEntity GetEntity(int id);

        bool Delete(CommentEntity entity);

        bool Update(CommentEntity entity);
    }
}
