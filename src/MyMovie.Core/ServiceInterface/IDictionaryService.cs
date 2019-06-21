using MyMovie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.ServiceInterface
{
    public interface IDictionaryService
    {

        List<DictionaryEntity> GetParentList();

        bool AddEntity(DictionaryEntity entity);

        bool UpdateEntity(DictionaryEntity entity);

        bool Delete(DictionaryEntity entity);

        List<DictionaryEntity> GetPageList(int row, int page, int parId, out int count);

        List<DictionaryEntity> GetEntitiesByparId(int parId);

        DictionaryEntity GetEntity(int id);
    }
}
