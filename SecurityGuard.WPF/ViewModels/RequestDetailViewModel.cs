﻿using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using MVVMEssentials.ViewModels;
using SecurityGuard.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecurityGuard.WPF.ViewModels
{
    public class RequestDetailViewModel : ViewModelBase
    {
        public RequestDetailFormViewModel RequestDetailsFormViewModel { get; set; }
        public RequestDetailViewModel(SelectedRequestStore selectedRequestStore,
            MemberStore memberStore,
            RequestStore requestStore,
            ModalNavigationStore modalNavigationStore,
            INavigationService closeNavigationService)
        {
            RequestDetailsFormViewModel = new RequestDetailFormViewModel(closeNavigationService,
                memberStore,
                requestStore,
                modalNavigationStore,
                selectedRequestStore)
            {
                RequestId = selectedRequestStore.SelectedRequest.Id,
                PasportNumber = selectedRequestStore.SelectedRequest.Clients.PasportNumber,
                PasportSeries = selectedRequestStore.SelectedRequest.Clients.PasportSeries,
                FullName = selectedRequestStore.SelectedRequest.ToString(),
                ArrivalDate = selectedRequestStore.SelectedRequest.ArrivalDate
            };
        }
    }
}
