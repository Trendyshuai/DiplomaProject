using MyMovie.Core.Entities;
using MyMovie.Core.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Infrastructure.Data
{
    public class DictionaryRepository : Repository<DictionaryEntity>, IDictionaryRepository
    {
        public DictionaryRepository(EfDbContext efDbContext) : base(efDbContext)
        {
        }
    }
}
