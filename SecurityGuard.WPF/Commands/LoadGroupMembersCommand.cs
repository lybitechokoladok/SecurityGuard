﻿using MVVMEssentials.Commands;
using SecurityGuard.WPF.Stores;
using SecurityGuard.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.Commands
{
    public class LoadGroupMembersCommand : AsyncCommandBase
    {
        private readonly RequestDetailFormViewModel _viewModel;
        private readonly SelectedRequestStore _selectedRequestStore;
        private readonly MemberStore _memberStore;

        public LoadGroupMembersCommand(RequestDetailFormViewModel viewModel, MemberStore memberStore, SelectedRequestStore selectedRequestStore)
        {
            _viewModel = viewModel;
            _memberStore = memberStore;
            _selectedRequestStore = selectedRequestStore;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await _memberStore.LoadGroupById(_viewModel.GroupId);
        }
    }
}
