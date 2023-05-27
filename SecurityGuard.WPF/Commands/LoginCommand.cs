using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using SecurityGuard.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly LoginViewModel _viewModel;
        private readonly INavigationService _navigationService;

        public LoginCommand(INavigationService navigationService, LoginViewModel viewModel)
        {
            _navigationService = navigationService;
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) 
        {
            if( e.PropertyName == nameof(LoginViewModel.Username) ||
                e.PropertyName == nameof(LoginViewModel.Password))
                OnCanExecuteChanged();
        }
        protected override async Task ExecuteAsync(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
