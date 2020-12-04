using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeSplit.ViewModel
{
    public class UsedToGoViewModel : BaseViewModel
    {
        private ObservableCollection<Journey> OldData;
        private ObservableCollection<Journey> _list;

        public ObservableCollection<Journey> List
        {
            get => _list;
            set { _list = value; OnPropertyChanged(); }
        }

        private string _SearchTextByLocation;

        public string SearchTextByLocation
        {
            get { return _SearchTextByLocation; }
            set { _SearchTextByLocation = value; OnPropertyChanged(); }
        }

        private string _SearchTextMemberName;

        public string SearchTextMemberName
        {
            get { return _SearchTextMemberName; }
            set { _SearchTextMemberName = value; OnPropertyChanged(); }
        }

        public ICommand SearchCommand { get; set; }

        public class Journey
        {
            public string Title { get; set; }
            public string Image { get; set; }
            public string Location { get; set; }
        }

        public UsedToGoViewModel()
        {
            OldData = new ObservableCollection<Journey>();
            OldData = LoadData();
            List = new ObservableCollection<Journey>();
            SearchTextByLocation = "";
            SearchTextMemberName = "";
            foreach (var journey in OldData)
            {
                List.Add(journey);
            }
            List = OldData;
            SearchCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                List.Clear();
                OldData.Clear();
                OldData = LoadData();
                foreach (var journey in OldData)
                {
                    if (journey.Title.Contains(SearchTextMemberName) && journey.Location.Contains(SearchTextByLocation))
                    {
                        List.Add(journey);
                    }
                }
                OnPropertyChanged("List");
            });
        }

        private ObservableCollection<Journey> LoadData()
        {
            var ret = new ObservableCollection<Journey>()
            {
                new Journey() { Title="Chu Tùng Nhân", Image="/Images/image1.jpg", Location="New York" },
                new Journey() { Title="Chu Tùng HIEUTHUHAI", Image="/Images/image2.jpg", Location="WC" },
                new Journey() { Title="Chu Tùng HIEUTHUBA", Image="/Images/image3.jpg", Location="Alabama" },
                new Journey() { Title="Chu Tùng Hà", Image="/Images/image4.jpg", Location="Valley" },
                new Journey() { Title="Chu Tùng Nhân", Image="/Images/image5.jpg", Location="New York" },
                new Journey() { Title="Chu Tùng Nhân", Image="/Images/image6.jpg", Location="New York" },
            };
            return ret;
        }
    }
}
