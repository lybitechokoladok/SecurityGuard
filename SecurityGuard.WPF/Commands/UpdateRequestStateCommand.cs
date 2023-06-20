using MVVMEssentials.Commands;
using MVVMEssentials.Services;
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
    public class UpdateRequestStateCommand : AsyncCommandBase
    {
        private readonly RequestStore _store;
        private readonly ModalNavigationStore _navigationStore;
        private readonly RequestDetailFormViewModel _viewModel;

        public UpdateRequestStateCommand(RequestStore store, ModalNavigationStore navigationStore, RequestDetailFormViewModel requestDetailFormViewModel)
        {
            _store = store;
            _navigationStore = navigationStore;
            _viewModel = requestDetailFormViewModel;
        }
        protected override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _store.UpdateRequestState(_viewModel.RequestDetailId, (int)RequestState.Approved);

                _navigationStore.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось одобрить заявку","Сообщение");
                throw new Exception();
            }
        }
    }
}
