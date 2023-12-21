using JobPortal.Application.Interfaces;
using JobPortal.Data.Repositories;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Interfaces;

namespace JobPortal.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User update(User user)
        {
            if (user != null)
            {
                return userRepository.update(user);
            }
            return null;
        }

        public User getUserByRefreshToken(string token)
        {
            return userRepository.getUserByRefreshToken(token);
        }

        public User getUserByUsername(string username)
        {
            return userRepository.getUserByUsername(username);
        }

        public User getUserByEmail(string email)
        {
            return userRepository.getUserByEmail(email);
        }

        public User getUserByGoogleId(string GoogleId)
        {
            return userRepository.getUserByGoogleId(GoogleId);
        }

        public User add(User user)
        {
            return userRepository.add(user);
        }

        public bool delete(Guid id)
        {
            return userRepository.delete(id);
        }


        public User get(Guid id)
        {
            return userRepository.get(id);
        }
    }

}
