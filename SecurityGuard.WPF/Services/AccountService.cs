using SecurityGuard.Domain.Abstractions;
using SecurityGuard.Domain.Dtos;
using SecurityGuard.Domain.Models;
using SecurityGuard.Domain.Repositories;
using SecurityGuard.Domain.Services;
using SecurityGuard.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SecurityGuard.WPF.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly AccountStore _accountStore;
        public AccountService(IPasswordHasher passwordHasher, IUserRepository userRepository, AccountStore accountStore)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _accountStore= accountStore;
        }
        public async Task<UserDto> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                MessageBox.Show("Такого пользователя не найдено");
                throw new Exception();
            }
            else
            {
                var passwordIsValid = _passwordHasher.HashesMatch(user.HashedPassword, password);

                if (!passwordIsValid)
                {
                    MessageBox.Show("Неверный пароль");
                    throw new Exception();
                }
            }


            var currentUser = new UserDto
            {
                Username = username,
                Password = password,
                Role = user.JobTitle.Id
            };
            _accountStore.CurrentUser = currentUser;
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
