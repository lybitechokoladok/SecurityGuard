using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.ViewModels
{
    public class LoginLayoutViewModel : ViewModelBase
    {
        public ViewModelBase ContentViewModel { get; }

        public LoginLayoutViewModel( ViewModelBase contentViewModel)
        {
            ContentViewModel = contentViewModel;
        }

        public override void Dispose()
        {
            ContentViewModel.Dispose();

            base.Dispose();
        }
    }
}
