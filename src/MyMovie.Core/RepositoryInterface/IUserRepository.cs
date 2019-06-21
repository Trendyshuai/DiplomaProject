using MyMovie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.RepositoryInterface
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        AdminEntity GetAdmin(string userName, string password);
    }
}
