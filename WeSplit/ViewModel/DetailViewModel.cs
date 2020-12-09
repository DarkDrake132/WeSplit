using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeSplit.Model;

namespace WeSplit.ViewModel
{
    public class DetailViewModel : BaseViewModel
    {
        private JourneyCollector _curJourney;
        private string _Status;

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private string _colorStatus;

        public string ColorStatus
        {
            get { return _colorStatus; }
            set { _colorStatus = value; }
        }


        public JourneyCollector curJourney
        {
            get { return _curJourney; }
            set { _curJourney = value; OnPropertyChanged(); }
        }
        
        public DetailViewModel()
        {
            curJourney = TestData();
            Status = "Trạng thái:\nĐang thực hiện";
            ColorStatus = "Red";
            if (curJourney.IsFinish == 1)
            {
                Status = "Trạng thái:\nĐã hoàn thành";
                ColorStatus = "Green";
            }
        }

        private JourneyCollector TestData()
        {
            var Item = DataProvider.Ins.DB.JOURNEYs.Where(x => x.id == 2).SingleOrDefault();
            var value = new JourneyCollector(Item.id, Item.C_location, Item.title, Item.isFinish, Item.thumbnailLink);
            return value;
        }
    }
}
