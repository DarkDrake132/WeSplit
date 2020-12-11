using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeSplit.Model;

namespace WeSplit.ViewModel
{
    class UpdateScreenViewModel : BaseViewModel
    {
        private JourneyCollector _curJourney;
        public JourneyCollector curJourney
        {
            get { return _curJourney; }
            set { _curJourney = value; OnPropertyChanged(); }
        }
        private ObservableCollection<MEMBER> _listMem;

        public ObservableCollection<MEMBER> ListMem
        {
            get { return _listMem; }
            set { _listMem = value; }
        }
        private ObservableCollection<EXPENSE> _listExpe;

        public ObservableCollection<EXPENSE> ListExpe
        {
            get { return _listExpe; }
            set { _listExpe = value; }
        }
        private ObservableCollection<MemberWithPaid> _listPaid;

        public ObservableCollection<MemberWithPaid> ListPaid
        {
            get { return _listPaid; }
            set { _listPaid = value; }
        }
        public string ColorStatus { get; set; }
        public string Status { get; set; }
        public ICommand ChangeStatusCommand { get; set; }
        public UpdateScreenViewModel()
        {
            GetData();

            ChangeStatusCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Status = "Đã hoàn thành"; OnPropertyChanged("Status");
                ColorStatus = "Green"; OnPropertyChanged("ColorStatus");
            });
        }

        public void GetData()
        {
            var Item = DataProvider.Ins.DB.JOURNEYs.Where(x => x.id == Global.IntData).SingleOrDefault();
            curJourney = new JourneyCollector(Item.id, Item.C_location, Item.title, Item.isFinish, Item.thumbnailLink);
            ColorStatus = "Red";
            Status = "Đang thực hiện";
            ListMem = new ObservableCollection<MEMBER>(DataProvider.Ins.DB.MEMBERs.Where(x => x.idJourney == curJourney.Id));
            ListExpe = new ObservableCollection<EXPENSE>(DataProvider.Ins.DB.EXPENSEs.Where(x => x.idJourney == curJourney.Id));
            ListPaid = new ObservableCollection<MemberWithPaid>();
            foreach (var item in ListMem)
            {
                int sum = 0;
                foreach (var iTem in ListExpe)
                {
                    if (iTem.idMember == item.id)
                    {
                        sum += iTem.cost.GetValueOrDefault();
                    }
                }
                MemberWithPaid temp = new MemberWithPaid(item.C_name, sum);
                ListPaid.Add(temp);
            }
        }
    }
}
