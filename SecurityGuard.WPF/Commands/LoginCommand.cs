using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using SecurityGuard.Domain.Dtos;
using SecurityGuard.Domain.Services;
using SecurityGuard.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SecurityGuard.WPF.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly LoginViewModel _viewModel;
        private readonly INavigationService _navigationService;
        private readonly IAccountService _accountService;
        public LoginCommand(INavigationService navigationService, LoginViewModel viewModel, IAccountService accountService)
        {
            _navigationService = navigationService;
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
            _accountService = accountService;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) 
        {
            if( e.PropertyName == nameof(LoginViewModel.Username) ||
                e.PropertyName == nameof(LoginViewModel.Password))
                OnCanExecuteChanged();
        }
        protected override async Task ExecuteAsync(object parameter)
        {
            UserDto user = await _accountService.LoginAsync(_viewModel.Username, _viewModel.Password);
            if (user == null)
                MessageBox.Show("erroe", "erroe");
            else
            _navigationService.Navigate();
        }
    }
}
