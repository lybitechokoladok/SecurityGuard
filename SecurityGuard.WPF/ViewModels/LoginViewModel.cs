﻿using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using SecurityGuard.Domain.Services;
using SecurityGuard.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecurityGuard.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RemeberMeCommand { get; }
        public ICommand ForgotPasswordCommand { get; }

        public LoginViewModel(INavigationService loginNavigationService, IAccountService accountService)
        {
            LoginCommand = new LoginCommand(loginNavigationService, this, accountService);
        }
    }
}
