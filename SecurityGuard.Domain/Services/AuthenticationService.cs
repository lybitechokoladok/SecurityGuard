using SecurityGuard.Domain.Abstractions;
using SecurityGuard.Domain.Dtos;
using SecurityGuard.Domain.Models;
using SecurityGuard.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Services
{
    public class AuthenticationService : IAccountService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        public AuthenticationService(IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }
        public async Task<UserDto> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                throw new Exception();
            }
            else
            {
                var passwordIsValid = _passwordHasher.HashesMatch(user.HashedPassword, password);

                if (!passwordIsValid)
                {
                    throw new Exception();
                }
            }


            var currentUser = new UserDto
            {
                Username = username,
                Password = password,
                Role = user.JobTitle.Id
            };
            return currentUser;

        }

        public async Task<UserDto> RegisterAsync(string username, string password)
        {
            var passwordHash = _passwordHasher.Hash(password);

            var user = new User()
            {

            };
            await _userRepository.AddAsync(user);

            return new UserDto
            {

            };

        }
    }
}
