using MyMovie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.ServiceInterface
{
    public interface IUserService
    {

        bool AddUser(UserEntity entity);

        bool UpdateUser(UserEntity user);

        UserEntity GetEntity(string userName);

        UserEntity GetEntity(string userName, string pawssword);

        AdminEntity GetAdmin(string userName, string password);
    }
}
