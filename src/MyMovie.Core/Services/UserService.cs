using MyMovie.Core.Entities;
using MyMovie.Core.RepositoryInterface;
using MyMovie.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public bool AddUser(UserEntity entity)
        {
            return _userRepository.Add(entity);
        }

        public bool UpdateUser(UserEntity user)
        {
            return _userRepository.Update(user);
        }

        public UserEntity GetEntity(string userName)
        {
            return _userRepository.List(u => u.UserName == userName).FirstOrDefault();
        }

        public UserEntity GetEntity(string userName, string pawssword)
        {
            return _userRepository.List(u => u.UserName == userName && u.Password == pawssword).FirstOrDefault();
        }

        public AdminEntity GetAdmin(string userName, string password)
        {
            return _userRepository.GetAdmin(userName, password);
        }
    }
}
