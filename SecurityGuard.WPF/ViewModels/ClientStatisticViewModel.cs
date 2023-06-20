using LiveCharts;
using LiveCharts.Wpf;
using MVVMEssentials.ViewModels;
using SecurityGuard.Domain.Enums;
using SecurityGuard.Domain.Models;
using SecurityGuard.WPF.Commands;
using SecurityGuard.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecurityGuard.WPF.ViewModels
{
    public class ClientStatisticViewModel : ViewModelBase 
    { 
        
        private readonly RequestStore _requestStore;

        private readonly ObservableCollection<Request> _requests;
        public IEnumerable<Request> Requests => _requests;
        public int NewCount { get; set; }
        public int ApprovedCount { get; set; }
        public int InProcessCount { get; set; }
        public int ClosedCount { get; set; }
        public SeriesCollection PieCollection {  get; set; }
        public ICommand LoadAllRequestCommand { get; }
        public ClientStatisticViewModel(RequestStore requestStore)
        {
            _requests = new ObservableCollection<Request>();
            PieCollection = new SeriesCollection();
            _requestStore = requestStore;

            LoadAllRequestCommand = new LoadRequestStatisticCommand( requestStore);
            LoadAllRequestCommand.Execute(null); 

            _requestStore.RequestsLoaded += OnRequestLoaded;
        }

        private void OnRequestLoaded()
        {
            _requests.Clear();

            foreach (Request request in _requestStore.Requests)
            {
               _requests.Add(request);
            }

            NewCount = _requestStore.Requests.Where(p => p.RequestDetails.RequestState.Id == (int)Domain.Enums.RequestState.New).Count();
            ApprovedCount = _requestStore.Requests.Where(p => p.RequestDetails.RequestState.Id == (int)Domain.Enums.RequestState.Approved).Count();
            InProcessCount = _requestStore.Requests.Where(p => p.RequestDetails.RequestState.Id == (int)Domain.Enums.RequestState.InProcess).Count();
            ClosedCount = _requestStore.Requests.Where(p => p.RequestDetails.RequestState.Id == (int)Domain.Enums.RequestState.Closed).Count();

            PieCollection.Add(new PieSeries { Title = "Новые", Values = new ChartValues<int> { NewCount }, DataLabels = true });
            PieCollection.Add(new PieSeries { Title = "Одобренные", Values = new ChartValues<int> { ApprovedCount }, DataLabels = true });
            PieCollection.Add(new PieSeries { Title = "В процессе", Values = new ChartValues<int> { InProcessCount }, DataLabels = true });
            PieCollection.Add(new PieSeries { Title = "Завершенные", Values = new ChartValues<int> { ClosedCount }, DataLabels = true });
        }

        public override void Dispose()
        {
            _requestStore.RequestsLoaded -= OnRequestLoaded;

            base.Dispose();
        }
    }
}
