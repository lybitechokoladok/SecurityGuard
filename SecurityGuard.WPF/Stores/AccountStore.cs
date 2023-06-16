using SecurityGuard.Domain.Enums;
using SecurityGuard.Domain.Dtos;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.Stores
{
    public class AccountStore
    {
        private UserDto _currentUser;
        public UserDto CurrentUser
        { 
            get { return _currentUser; }
            set 
            {
                _currentUser = value;
                OnCurrentUserChanged?.Invoke();
            }
        }

        public event Action OnCurrentUserChanged;

        public void Logout() 
        {
            CurrentUser = null;
        }
    }
}
