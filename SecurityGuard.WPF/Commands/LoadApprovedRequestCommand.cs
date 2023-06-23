using MVVMEssentials.Commands;
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
    public class LoadApprovedRequestCommand : AsyncCommandBase
    {
        private readonly RequestStore _requestStore;

        public LoadApprovedRequestCommand( RequestStore requestStore)
        {
            _requestStore = requestStore;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _requestStore.LoadAllApproved();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось загрузить список заявок", "Сообщение");
                throw new Exception();
            }
        }
    }
}
