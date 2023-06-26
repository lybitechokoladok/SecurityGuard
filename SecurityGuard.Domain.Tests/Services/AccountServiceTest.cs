using Moq;
using SecurityGuard.Domain.Abstractions;
using SecurityGuard.Domain.Dtos;
using SecurityGuard.Domain.Models;
using SecurityGuard.Domain.Repositories;
using SecurityGuard.Domain.Services;
using SecurityGuard.Infrastructure;
using SecurityGuard.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Tests.Services
{
    [TestFixture]
    public class AccountServiceTest
    {
        private  DbConnection _context;
        private UserRepository _userRepository;
        private PasswordHasher _passwordHasher;
        private AuthenticationService _authenticationService;
        [SetUp]
        public void SetUp() 
        {
            _context = new DbConnection();
            _userRepository = new UserRepository(_context);
            _passwordHasher = new PasswordHasher();
            _authenticationService = new AuthenticationService(_passwordHasher, _userRepository);
        }

        [Test]
        public async Task Login_WithCorrectPasswordForExistingUsername_ReturnsAccountForCorrectUsername()
        {

            string expectedUsername = "palpason";
            string password = "palpason123";

            UserDto account = await _authenticationService.LoginAsync(expectedUsername, password);

            string actualUsername = account.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithInCorrectPasswordForExistingUsername_ThrowsExeption()
        {

            string expectedUsername = "palpason";
            string password = "palpason1";

            Exception exception = Assert.ThrowsAsync<Exception>(() => _authenticationService.LoginAsync(expectedUsername, password));
            Assert.NotNull(exception);
        }

        [Test]
        public void Login_WithNonExistingUsername_ThrowsExeption()
        {

            string expectedUsername = "palpaso";
            string password = "palpason1";

            Exception exception = Assert.ThrowsAsync<Exception>(() => _authenticationService.LoginAsync(expectedUsername, password));
            Assert.NotNull(exception);
        }
    }
}
