using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using SecurityGuard.Domain.Repositories;
using SecurityGuard.WPF.Stores;
using SecurityGuard.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SecurityGuard.WPF.Commands
{
    public class OpenRequestDetailCommand : AsyncCommandBase
    {
        private readonly INavigationService _navigationService;
        private readonly ClientStore _clientStore;
        private readonly RequestListingViewModel _viewModel;

        public OpenRequestDetailCommand(INavigationService navigationService, ClientStore clientStore, RequestListingViewModel viewModel)
        {
            _navigationService = navigationService;
            _clientStore = clientStore;
            _viewModel = viewModel;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            var isBanned = await _clientStore.IsClientsBlackList(_viewModel.SelectedRequest.ClientNumber);
            if (isBanned)
            {
                MessageBox.Show("Данный пользователь находится в черном списке","Сообщение");
                throw new Exception();
            }
            else
                _navigationService.Navigate();
        }
    }
}
