using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeSplit.Model;

namespace WeSplit.ViewModel
{
    public class UsedToGoViewModel : BaseViewModel
    {
        private ObservableCollection<JOURNEY> OldData;
        private ObservableCollection<JOURNEY> _list;

        public ObservableCollection<JOURNEY> List
        {
            get => _list;
            set { _list = value; OnPropertyChanged(); }
        }

        //private Journey _SelectedItem;

        //public Journey SelectedItem
        //{
        //    get { return _SelectedItem; }
        //    set { _SelectedItem = value; }
        //}


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
        public ICommand DetailCommand { get; set; }

        public UsedToGoViewModel()
        {
            OldData = new ObservableCollection<JOURNEY>();
            OldData = LoadData();
            List = new ObservableCollection<JOURNEY>();
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
                    if (journey.C_location.Contains(SearchTextByLocation) && ContainsMember(journey, SearchTextMemberName))
                    {
                        List.Add(journey);
                    }
                }
                OnPropertyChanged("List");
            });

            DetailCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Global.IntData = Int32.Parse(p.ToString());
                DetailScreen dt = new DetailScreen();
                dt.ShowDialog();
            });
        }

        private bool ContainsMember(JOURNEY journey, string searchTextMemberName)
        {
            var query = from p in DataProvider.Ins.DB.JOURNEYs
                        join c in DataProvider.Ins.DB.MEMBERs on p.id equals c.idJourney
                        where p.id == journey.id && c.C_name.Contains(searchTextMemberName)
                        select new
                        {
                            id = p.id,
                            name = c.C_name
                        };
            if (query.Count() > 0)
            {
                return true;
            }
            return false;
        }

        private ObservableCollection<JOURNEY> LoadData()
        {
            var data = new ObservableCollection<JOURNEY>(DataProvider.Ins.DB.JOURNEYs.Where(x => x.isFinish == 1));
            return data;
        }
    }
}
