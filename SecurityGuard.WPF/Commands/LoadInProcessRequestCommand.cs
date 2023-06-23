using Azure.Core;
using MVVMEssentials.Commands;
using SecurityGuard.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SecurityGuard.WPF.Commands
{
    public class LoadInProcessRequestCommand : AsyncCommandBase
    {
        private readonly RequestStore _store;

        public LoadInProcessRequestCommand(RequestStore store)
        {
            _store = store;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _store.LoadAllCurrent();
            }
            
            catch (Exception)
            {
                MessageBox.Show("Не удалось загрузить список заявок", "Сообщение");
                throw new Exception();
            }
        }
    }
}
