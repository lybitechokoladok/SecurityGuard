using SecurityGuard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.WPF.Stores
{
    public class SelectedRequestStore
    {
		private readonly RequestStore _requestStore;

		private Request _selectedRequest;
		public Request SelectedRequest
		{
			get =>  _selectedRequest; 
			set 
			{
				_selectedRequest = value;
				SelectedRequestChanged?.Invoke();
			}
		}


		public event Action SelectedRequestChanged;

		public SelectedRequestStore(RequestStore requestStore)
		{
			_requestStore= requestStore;

			_requestStore.RequestsSelected += RequestSelected;
		}

		private void RequestSelected(Request request) 
		{
			if(request.Id == SelectedRequest.Id)
				SelectedRequest= request;
		}
	}
}
