﻿using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using MVVMEssentials.ViewModels;
using SecurityGuard.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.Services
{
    public class LayoutNavigationService<TViewModel> : INavigationService 
        where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;
        private readonly Func<NavigationBarViewModel> _createNavigationBarViewModel;

        public LayoutNavigationService(NavigationStore navigationStore,
            Func<TViewModel> createViewModel,
            Func<NavigationBarViewModel> createNavigationBarViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
            _createNavigationBarViewModel = createNavigationBarViewModel;
        }

        public LayoutNavigationService(NavigationStore navigationStore,
           Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
            _createNavigationBarViewModel = null;
        }

        public void Navigate()
        {
            if (_createNavigationBarViewModel is null)
                _navigationStore.CurrentViewModel = new LayoutViewModel(_createViewModel());
            else
                _navigationStore.CurrentViewModel = new LayoutViewModel(_createNavigationBarViewModel(), _createViewModel());
        }
    }
}
