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
        public string KindIcon { get; set; }
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
                if (item.isFinish == 0)
                {
                    Status = "Đã hoàn thành"; OnPropertyChanged("Status");
                    ColorStatus = "Green"; OnPropertyChanged("ColorStatus");
                    KindIcon = "CloseOutline";OnPropertyChanged("KindIcon");
                    item.isFinish = 1;
                }
                else
                {
                    Status = "Đang thực hiện"; OnPropertyChanged("Status");
                    ColorStatus = "Red"; OnPropertyChanged("ColorStatus");
                    KindIcon = "CheckOutline"; OnPropertyChanged("KindIcon");
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
                if(DataProvider.Ins.DB.MEMBERs.Where(x => x.C_name == MemberDisplayName).Count() == 0)
                {
                    var nextId = DataProvider.Ins.DB.MEMBERs.Max(x => x.id);
                    nextId++;
                    var member = new MEMBER();
                    //add new member
                    member.idJourney = curJourney.Id;
                    member.id = nextId;
                    member.C_name = MemberDisplayName;
                    DataProvider.Ins.DB.MEMBERs.Add(member);
                    DataProvider.Ins.DB.SaveChanges();
                    //
                    var nextjob = 0;
                    var expense = new EXPENSE();
                    expense.idJourney = curJourney.Id;
                    expense.idMember = member.id;
                    expense.id = nextjob;
                    expense.objectPay = ObjectPaidDisplayName;
                    expense.cost = Convert.ToInt32(PaidDisplay);
                    DataProvider.Ins.DB.EXPENSEs.Add(expense);
                    DataProvider.Ins.DB.SaveChanges();
                    LoadData();
                }
                else
                {
                    var member = from e in DataProvider.Ins.DB.MEMBERs
                                 join c in DataProvider.Ins.DB.EXPENSEs on new { X1 = e.idJourney, X2 = e.id } equals new { X1 = c.idJourney, X2 = c.idMember }
                                 where e.idJourney == curJourney.Id && e.C_name == MemberDisplayName
                                 select new
                                 {
                                     idMember = e.id,
                                     idPaid = c.id
                                 };
                    var nextjob = member.Max(x => x.idPaid);
                    nextjob++;
                    var expense = new EXPENSE();
                    expense.idJourney = curJourney.Id;
                    expense.idMember = member.Max(x => x.idMember);
                    expense.id = nextjob;
                    expense.objectPay = ObjectPaidDisplayName;
                    expense.cost = Convert.ToInt32(PaidDisplay);
                    DataProvider.Ins.DB.EXPENSEs.Add(expense);
                    DataProvider.Ins.DB.SaveChanges();
                    LoadData();
                }
            });

            UpdateCommand = new RelayCommand<object>((p) =>
            {
                if(SelectedItem == null)
                {
                    return false;
                }
                if (MemberDisplayName == "" || ObjectPaidDisplayName == "" || PaidDisplay == "")
                {
                    return false;
                }
                if (ListMemberWithObjectPaid.Where(x => x.ObjectPaid == ObjectPaidDisplayName && x.Paid.ToString() == PaidDisplay && x.Name == MemberDisplayName).Count() > 0)
                {
                    return false;
                }
                if (ListMemberWithObjectPaid.Where(x => x.IdPaid == SelectedItem.IdPaid && x.Id == SelectedItem.Id && x.ObjectPaid == ObjectPaidDisplayName && x.Paid.ToString() == PaidDisplay).Count() > 0)
                {
                    return false;
                }
                if (!int.TryParse(PaidDisplay, out int n) || PaidDisplay[0] == '-')
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var item = DataProvider.Ins.DB.EXPENSEs.Where(x => x.idJourney == curJourney.Id && x.id == SelectedItem.IdPaid && x.objectPay == SelectedItem.ObjectPaid && x.cost == SelectedItem.Paid).SingleOrDefault();
                item.objectPay = ObjectPaidDisplayName;
                item.cost = Convert.ToInt32(PaidDisplay);
                DataProvider.Ins.DB.SaveChanges();
                LoadData();
            });
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if(SelectedItem ==null)
                {
                    return false;
                }
                if(ObjectPaidDisplayName != SelectedItem.ObjectPaid && MemberDisplayName != SelectedItem.Name && PaidDisplay != SelectedItem.Paid.ToString())
                {
                    return false;
                }
                if (ListMemberWithObjectPaid.Where(x => x.ObjectPaid == ObjectPaidDisplayName && x.Paid.ToString() == PaidDisplay && x.Name == MemberDisplayName).Count() == 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var item = DataProvider.Ins.DB.EXPENSEs.Where(x => x.idJourney == curJourney.Id && x.idMember == SelectedItem.Id && x.id == SelectedItem.IdPaid).SingleOrDefault();
                DataProvider.Ins.DB.EXPENSEs.Remove(item);
                DataProvider.Ins.DB.SaveChanges();
                if(DataProvider.Ins.DB.EXPENSEs.Where(x => x.idJourney == curJourney.Id && x.idMember == SelectedItem.Id).Count() == 0)
                {
                    var member = DataProvider.Ins.DB.MEMBERs.Where(x => x.id == SelectedItem.Id).SingleOrDefault();
                    DataProvider.Ins.DB.MEMBERs.Remove(member);
                    DataProvider.Ins.DB.SaveChanges();
                }
                LoadData();
            });
        }

        public void GetData()
        {
            MemberDisplayName = "";
            ObjectPaidDisplayName = "";
            PaidDisplay = "";
            var Item = DataProvider.Ins.DB.JOURNEYs.Where(x => x.id == Global.IntData).SingleOrDefault();
            curJourney = new JourneyCollector(Item.id, Item.C_location, Item.title, Item.isFinish, Item.thumbnailLink);
            if (curJourney.IsFinish == 0)
            {
                ColorStatus = "Red";
                Status = "Đang thực hiện";
                KindIcon = "CheckOutline";
            }
            else
            {
                ColorStatus = "Green";
                Status = "Đã hoàn thành";
                KindIcon = "CloseOutline";
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
                            idPaid = c.id,
                            name = p.C_name,
                            objectPaid = c.objectPay,
                            paid = c.cost
                        };
            ListMemberWithObjectPaid = new ObservableCollection<Member_ObjectPay_Cost>();
            foreach (var item in query)
            {
                ListMemberWithObjectPaid.Add(new Member_ObjectPay_Cost(item.id, item.idPaid, item.name, item.objectPaid, item.paid));
            }
            OnPropertyChanged("ListMemberWithObjectPaid");
        }
    }
}
