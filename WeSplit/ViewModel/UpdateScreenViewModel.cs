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

        private ObservableCollection<Member_ObjectPay_Cost> _listMemberWithObjectPaid;

        public ObservableCollection<Member_ObjectPay_Cost> ListMemberWithObjectPaid
        {
            get { return _listMemberWithObjectPaid; }
            set { _listMemberWithObjectPaid = value; }
        }


        private Member_ObjectPay_Cost _selectedItem;

        public Member_ObjectPay_Cost SelectedItem
        {
            get { return _selectedItem; }
            set 
            { 
                _selectedItem = value;
                OnPropertyChanged();
                if(SelectedItem != null)
                {
                    MemberDisplayName = SelectedItem.Name;
                    ObjectPaidDisplayName = SelectedItem.ObjectPaid;
                    PaidDisplay = SelectedItem.Paid.ToString();
                    OnPropertyChanged("MemberDisplayName");
                    OnPropertyChanged("ObjectPaidDisplayName");
                    OnPropertyChanged("PaidDisplay");
                }
                else
                {
                    MemberDisplayName = "";
                    ObjectPaidDisplayName = "";
                    PaidDisplay = "";
                    OnPropertyChanged("MemberDisplayName");
                    OnPropertyChanged("ObjectPaidDisplayName");
                    OnPropertyChanged("PaidDisplay");
                }
            }
        }



        public string ColorStatus { get; set; }
        public string Status { get; set; }

        public string MemberDisplayName { get; set; }
        public string ObjectPaidDisplayName { get; set; }
        public string PaidDisplay { get; set; }


        public ICommand ChangeStatusCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }

        public UpdateScreenViewModel()
        {
            GetData();

            ChangeStatusCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                var item = DataProvider.Ins.DB.JOURNEYs.Where(x => x.id == curJourney.Id).SingleOrDefault();
                if(item.isFinish == 0)
                {
                    Status = "Đang thực hiện"; OnPropertyChanged("Status");
                    ColorStatus = "Red"; OnPropertyChanged("ColorStatus");
                    item.isFinish = 1;
                }
                else
                {
                    Status = "Đã hoàn thành"; OnPropertyChanged("Status");
                    ColorStatus = "Green"; OnPropertyChanged("ColorStatus");
                    item.isFinish = 0;
                }
                DataProvider.Ins.DB.SaveChanges();
            });

            AddCommand = new RelayCommand<object>((p) =>
            {
                if(MemberDisplayName == "" || ObjectPaidDisplayName == "" || PaidDisplay == "")
                {
                    return false;
                }
                if (ListMemberWithObjectPaid.Where(x => x.Name == MemberDisplayName && x.ObjectPaid == ObjectPaidDisplayName).Count() > 0)
                {
                    return false;
                }
                if(!int.TryParse(PaidDisplay, out int n) || PaidDisplay[0] == '-')
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var item = DataProvider.Ins.DB.JOURNEYs.Where(x => x.id == curJourney.Id).SingleOrDefault();
                if (item.isFinish == 0)
                {
                    Status = "Đang thực hiện"; OnPropertyChanged("Status");
                    ColorStatus = "Red"; OnPropertyChanged("ColorStatus");
                    item.isFinish = 1;
                }
                else
                {
                    Status = "Đã hoàn thành"; OnPropertyChanged("Status");
                    ColorStatus = "Green"; OnPropertyChanged("ColorStatus");
                    item.isFinish = 0;
                }
                DataProvider.Ins.DB.SaveChanges();
            });
        }

        public void GetData()
        {
            MemberDisplayName = "";
            ObjectPaidDisplayName = "";
            PaidDisplay = "";
            var Item = DataProvider.Ins.DB.JOURNEYs.Where(x => x.id == Global.IntData).SingleOrDefault();
            curJourney = new JourneyCollector(Item.id, Item.C_location, Item.title, Item.isFinish, Item.thumbnailLink);
            if(curJourney.Id == 0)
            {
                ColorStatus = "Red";
                Status = "Đang thực hiện";
            }
            else
            {
                ColorStatus = "Green";
                Status = "Đã hoàn thành";
            }

            LoadData();
            
        }

        private void LoadData()
        {
            var query = from p in DataProvider.Ins.DB.MEMBERs
                        join c in DataProvider.Ins.DB.EXPENSEs on new { X1 = p.idJourney, X2 = p.id } equals new {X1 = c.idJourney, X2 = c.idMember}
                        where p.idJourney == curJourney.Id
                        select new
                        {
                            id = p.id,
                            name = p.C_name,
                            objectPaid = c.objectPay,
                            paid = c.cost
                        };
            Console.WriteLine(query);
            ListMemberWithObjectPaid = new ObservableCollection<Member_ObjectPay_Cost>();
            foreach (var item in query)
            {
                ListMemberWithObjectPaid.Add(new Member_ObjectPay_Cost(item.id, item.name, item.objectPaid, item.paid));
            }
        }
    }
}
