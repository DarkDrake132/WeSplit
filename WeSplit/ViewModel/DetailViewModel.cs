using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeSplit.Model;
using LiveCharts;
using LiveCharts.Wpf;

namespace WeSplit.ViewModel
{
    public class DetailViewModel : BaseViewModel
    {
        private JourneyCollector _curJourney;
        public JourneyCollector curJourney
        {
            get { return _curJourney; }
            set { _curJourney = value; OnPropertyChanged(); }
        }
        private ObservableCollection<IMAGE_DESTINATION> _listImg;

        public ObservableCollection<IMAGE_DESTINATION> ListImg
        {
            get { return _listImg; }
            set { _listImg = value; OnPropertyChanged(); }
        }
        public Func<ChartPoint, string> PointLabel { get; set; }

        public ICommand UpdateCommand { get; set; }
        public string Status { get; set; }
        public string ColorStatus { get; set; }
        public int MemberCount { get; set; }

        public DetailViewModel()
        {
            TestData();
            Status = "Trạng thái:\nĐang thực hiện";
            ColorStatus = "Red";
            if (curJourney.IsFinish == 1)
            {
                Status = "Trạng thái:\nĐã hoàn thành";
                ColorStatus = "Green";
            }

            UpdateCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                // Update code here
            });
        }

        private void TestData()
        {
            var Item = DataProvider.Ins.DB.JOURNEYs.Where(x => x.isFinish == 0).SingleOrDefault();
            curJourney = new JourneyCollector(Item.id, Item.C_location, Item.title, Item.isFinish, Item.thumbnailLink);
            MemberCount = DataProvider.Ins.DB.MEMBERs.Where(x => x.idJourney == curJourney.Id).Count();
            ListImg = new ObservableCollection<IMAGE_DESTINATION>(DataProvider.Ins.DB.IMAGE_DESTINATION.Where(x => x.idJourney == curJourney.Id));
            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        }
    }
}
