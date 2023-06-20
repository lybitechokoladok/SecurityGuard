using MVVMEssentials.Commands;
using SecurityGuard.WPF.Stores;
using SecurityGuard.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.Commands
{
    class LoadRequestStatisticCommand : AsyncCommandBase
    {
        private readonly RequestStore _requestStore;

        public LoadRequestStatisticCommand( RequestStore requestStore)
        {
            _requestStore = requestStore;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await _requestStore.LoadAll();
        }
    }
}
