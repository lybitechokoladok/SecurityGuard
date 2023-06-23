using MVVMEssentials.Commands;
using SecurityGuard.Domain.Enums;
using SecurityGuard.WPF.Components;
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
    public class EndRequestCommand : AsyncCommandBase
    {
        private readonly RequestStore _store;
        private readonly InProcessRequestListingViewModel _viewModel;

        public EndRequestCommand(RequestStore store, InProcessRequestListingViewModel viewModel)
        {
            _store = store;
            _viewModel = viewModel;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _store.UpdateRequestState(_viewModel.SelectedRequest.Request.RequestDetails.Id, (int)RequestState.Closed);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось завершить посещение", "Сообщение");
                throw new Exception();
            }
        }
    }
}
