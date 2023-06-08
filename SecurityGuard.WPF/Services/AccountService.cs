using SecurityGuard.Domain.Abstractions;
using SecurityGuard.Domain.Dtos;
using SecurityGuard.Domain.Models;
using SecurityGuard.Domain.Repositories;
using SecurityGuard.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        public AccountService(IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }
        public async Task<UserDto> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            var passwordIsValid = _passwordHasher.HashesMatch(user.HashedPassword, password);

            if (!passwordIsValid)
                throw new Exception();

            return new UserDto
            {
                UsertName = username,
                Password = password,
            };
        }

        public async Task<UserDto> RegisterAsunc(string username, string password)
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
