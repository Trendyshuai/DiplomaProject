using MyMovie.Core.Entities;
using MyMovie.Core.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Infrastructure.Data
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(EfDbContext efDbContext) : base(efDbContext)
        {
        }



        public AdminEntity GetAdmin(string userName, string password)
        {
            return _dbContext.AdminEntities.Where(a => a.UserName == userName && a.Password == password).FirstOrDefault();
        }

    }
}
