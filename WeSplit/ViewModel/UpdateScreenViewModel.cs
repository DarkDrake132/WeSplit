using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeSplit.Model;
using System.Windows.Forms;
using System.IO;

namespace WeSplit.ViewModel
{
    class UpdateScreenViewModel : BaseViewModel
    {
        private ObservableCollection<IMAGE_DESTINATION> _ListImage;
        public ObservableCollection<IMAGE_DESTINATION> ListImage { get => _ListImage; set { _ListImage = value;  OnPropertyChanged(); } }

        private IMAGE_DESTINATION _SelectedImage;
        public IMAGE_DESTINATION SelectedImage
        {
            get => _SelectedImage;
            set
            {
                _SelectedImage = value;
                OnPropertyChanged();
            }
        }

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

        private string _MainImage;
        public string MainImage { get => _MainImage; set { _MainImage = value; OnPropertyChanged(); } }

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
        public ICommand ChangeMainImageCommand { get; set; }
        public ICommand AddActivityImageCommand { get; set; }
        public ICommand DeleteActivityImageCommand { get; set; }

        public UpdateScreenViewModel()
        {
            GetData();

            ChangeMainImageCommand = new RelayCommand<object>((p) =>
             {
                  return true;
             }, (p) =>
             {
                 OpenFileDialog ofd = new OpenFileDialog();
                 ofd.Filter = "(*.jpg)|*.jpg|(*.png)|.*png";
                 DialogResult dr = ofd.ShowDialog();
                 if (dr == System.Windows.Forms.DialogResult.OK)
                 {
                     var tempName = "";
                     changedLocation(ofd.FileName.ToString(), System.IO.Path.GetFileName(ofd.FileName.ToString()), ref tempName);
                     MainImage = tempName;

                     var item = DataProvider.Ins.DB.JOURNEYs.Where(x => x.id == curJourney.Id).SingleOrDefault();
                     item.thumbnailLink = MainImage;
                     DataProvider.Ins.DB.SaveChanges();
                 }
             });

            AddActivityImageCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "(*.jpg)|*.jpg|(*.png)|.*png";
                DialogResult dr = ofd.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    ListImage.Add(new IMAGE_DESTINATION());
                    ListImage[ListImage.Count - 1].idJourney = curJourney.Id;
                    ListImage[ListImage.Count - 1].id = ListImage[ListImage.Count - 2].id + 1;

                    string tempName = "";
                    changedLocation(ofd.FileName.ToString(), System.IO.Path.GetFileName(ofd.FileName.ToString()), ref tempName);
                    ListImage[ListImage.Count - 1].imageLink = tempName;


                    DataProvider.Ins.DB.IMAGE_DESTINATION.Add(ListImage.Last());
                    DataProvider.Ins.DB.SaveChanges();
                }
            });

            DeleteActivityImageCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedImage == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                //var item = DataProvider.Ins.DB.IMAGE_DESTINATION.Where(x => x.id == SelectedImage.id).Single();
                DataProvider.Ins.DB.IMAGE_DESTINATION.Remove(SelectedImage);
                DataProvider.Ins.DB.SaveChanges();

                ListImage.Remove(SelectedImage);
            });

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

            MainImage = curJourney.Thumbnail;
            ListImage = new ObservableCollection<IMAGE_DESTINATION>(DataProvider.Ins.DB.IMAGE_DESTINATION.Where(x => x.idJourney == curJourney.Id));

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

        private void getFileName(ref string path)
        {
            string res = System.IO.Path.GetFileName(path);
            path = path.Substring(0, path.IndexOf(res) - 1);
            //return res;
        }
        private void changedLocation(string sourcePath, string name, ref string newName)
        {
            string targetPath = Environment.CurrentDirectory.ToString();
            string temp = System.IO.Directory.GetParent(targetPath).ToString();
            temp = System.IO.Directory.GetParent(temp).ToString();
            targetPath = temp + "/Images";

            getFileName(ref sourcePath);
            string sourceFile = System.IO.Path.Combine(@sourcePath, name);
            var tail = name.Substring(name.Length - 4);
            name = DateTime.Now.ToString("yyyyMMddHHmmssfff") + tail;
            string destFile = System.IO.Path.Combine(@targetPath, name);
            FileInfo f1 = new FileInfo(sourceFile);
            FileInfo f2 = new FileInfo(destFile);
            f1.CopyTo(destFile);

            newName = name;
        }
    }
}
