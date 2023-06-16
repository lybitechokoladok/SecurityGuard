using SecurityGuard.Domain.Enums;
using SecurityGuard.Domain.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.Stores
{
    public class AccountStore
    {
        private User _currentUser;
        public User CurrentUser
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
