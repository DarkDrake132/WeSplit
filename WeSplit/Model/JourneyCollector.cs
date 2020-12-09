using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSplit.ViewModel;

namespace WeSplit.Model
{
    public class JourneyCollector: BaseViewModel
    {
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Location;

        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        private string _Title;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private int _isFinish;

        public int IsFinish
        {
            get { return _isFinish; }
            set { _isFinish = value; }
        }

        private string _Thumbnail;

        public string Thumbnail
        {
            get { return _Thumbnail; }
            set { _Thumbnail = value; }
        }

        public JourneyCollector(int id, string location, string title, int? isfinish, string thumbnail)
        {
            Id = id;
            Location = location;
            Title = title;
            IsFinish = isfinish ?? default(int);
            Thumbnail = thumbnail;
        }
    }
}
    