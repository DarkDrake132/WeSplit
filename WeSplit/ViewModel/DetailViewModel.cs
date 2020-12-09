using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeSplit.Model;

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
        }
    }
}
