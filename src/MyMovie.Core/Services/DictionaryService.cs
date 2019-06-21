using MyMovie.Core.Entities;
using MyMovie.Core.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.Services
{
    public class DictionaryService : ServiceInterface.IDictionaryService
    {
        private readonly IDictionaryRepository _dictionaryRepository;
        public DictionaryService(IDictionaryRepository dictionaryRepository)
        {
            _dictionaryRepository = dictionaryRepository;
        }



        public List<DictionaryEntity> GetParentList()
        {
            return _dictionaryRepository.List(d => d.ParId == 0).ToList();
        }

        public bool AddEntity(DictionaryEntity entity)
        {
            return _dictionaryRepository.Add(entity);
        }


        public bool UpdateEntity(DictionaryEntity entity)
        {
            return _dictionaryRepository.Update(entity);
        }

        public bool Delete(DictionaryEntity entity)
        {
            return _dictionaryRepository.Delete(entity);
        }

        public List<DictionaryEntity> GetPageList(int row, int page, int parId, out int count)
        {
            return _dictionaryRepository.LoadPageEntities(page, row, out count, d => d.ParId == parId, d => d.Id, true).ToList();
        }

        public List<DictionaryEntity> GetEntitiesByparId(int parId)
        {
            return _dictionaryRepository.List(d => d.ParId == parId).ToList();
        }

        public DictionaryEntity GetEntity(int id)
        {
            return _dictionaryRepository.GetById(id);
        }
    }
}
