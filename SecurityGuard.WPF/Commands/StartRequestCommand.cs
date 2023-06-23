using MVVMEssentials.Commands;
using MVVMEssentials.Stores;
using SecurityGuard.Domain.Enums;
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
    public class StartRequestCommand : AsyncCommandBase
    {
        private readonly RequestStore _store;
        private readonly ApprovedRequestListingViewModel _viewModel;

        public StartRequestCommand(RequestStore store, ApprovedRequestListingViewModel viewModel)
        {
            _store = store;
            _viewModel = viewModel;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _store.UpdateRequestState(_viewModel.SelectedRequest.Request.RequestDetails.Id, (int)RequestState.InProcess);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось начать заявку", "Сообщение");
                throw new Exception();
            }
            await _store.LoadAllApproved();
        }
    }
}
