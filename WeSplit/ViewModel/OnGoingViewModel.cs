using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplit.ViewModel
{
    public class OnGoingViewModel : BaseViewModel
    {
        private ObservableCollection<Journey> _list;

        public ObservableCollection<Journey> List
        {
            get => _list;
            set { _list = value; OnPropertyChanged(); }
        }

        public class Journey
        {
            public string Title { get; set; }
            public string Image { get; set; }
            public string Location { get; set; }
        }

        public OnGoingViewModel()
        {
            List = LoadData();
        }

        private ObservableCollection<Journey> LoadData()
        {
            var ret = new ObservableCollection<Journey>()
            {
                new Journey() { Title="Chu Tùng Nhân", Image="/Images/image1.jpg", Location="New York" },
                new Journey() { Title="Chu Tùng Nhân", Image="/Images/image2.jpg", Location="New York" },
                new Journey() { Title="Chu Tùng Nhân", Image="/Images/image3.jpg", Location="New York" },
                new Journey() { Title="Chu Tùng Nhân", Image="/Images/image4.jpg", Location="New York" },
                new Journey() { Title="Chu Tùng Nhân", Image="/Images/image5.jpg", Location="New York" },
                new Journey() { Title="Chu Tùng Nhân", Image="/Images/image6.jpg", Location="New York" },
            };
            return ret;
        }

        //class JourneyDAO
        //{
        //    public static BindingList<Journey> GetAll()
        //    {
        //        var result = new BindingList<Journey>()
        //        {
        //            new Journey() { Title="Chu Tùng Nhân", Image="/Images/image1.jpg", Location="New York" },
        //            new Journey() { Title="Chu Tùng Nhân", Image="/Images/image2.jpg", Location="New York" },
        //            new Journey() { Title="Chu Tùng Nhân", Image="/Images/image3.jpg", Location="New York" },
        //            new Journey() { Title="Chu Tùng Nhân", Image="/Images/image4.jpg", Location="New York" },
        //            new Journey() { Title="Chu Tùng Nhân", Image="/Images/image5.jpg", Location="New York" },
        //            new Journey() { Title="Chu Tùng Nhân", Image="/Images/image6.jpg", Location="New York" },
        //        };

        //        return result;
        //    }
        //}
        //BindingList<Journey> _list = new BindingList<Journey>();
    }
}
