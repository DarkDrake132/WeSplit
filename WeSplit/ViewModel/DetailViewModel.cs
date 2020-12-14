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


        private SeriesCollection _pieChart1;

        public SeriesCollection PieChart1
        {
            get { return _pieChart1; }
            set { _pieChart1 = value; OnPropertyChanged("PieChart"); }
        }
        private ObservableCollection<MemberWithPaid> _listPaid;

        public ObservableCollection<MemberWithPaid> ListPaid
        {
            get { return _listPaid; }
            set { _listPaid = value; }
        }
        private string _selectedImg;

        public string SelectedImg
        {
            get { return _selectedImg; }
            set { _selectedImg = value; }
        }
        private ObservableCollection<Member_ObjectPay_Cost> _listMemCost;

        public ObservableCollection<Member_ObjectPay_Cost> ListMemCost
        {
            get { return _listMemCost; }
            set { _listMemCost = value; }
        }



        public Func<ChartPoint, string> PointLabel { get; set; }

        public ICommand UpdateCommand { get; set; }
        public ICommand ChangeImageCommand { get; set; }
        public string Status { get; set; }
        public string ColorStatus { get; set; }
        public int MemberCount { get; set; }

        public DetailViewModel()
        {
            // Load data test
            GetData();

            UpdateCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                UpdateScreen up = new UpdateScreen();
                up.ShowDialog();
                GetData();
            });

            ChangeImageCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                SelectedImg = p.ToString();
                OnPropertyChanged("SelectedImg");
            });
        }

        private void GetData()
        {
            double totalCost = 0;
            var Item = DataProvider.Ins.DB.JOURNEYs.Where(x => x.id == Global.IntData).SingleOrDefault();
            curJourney = new JourneyCollector(Item.id, Item.C_location, Item.title, Item.isFinish, Item.thumbnailLink);OnPropertyChanged("curJourney");
            SelectedImg = curJourney.Thumbnail; OnPropertyChanged("SelectedImg");
            Status = "Trạng thái:\nĐang thực hiện";
            ColorStatus = "Red";
            if (curJourney.IsFinish == 1)
            {
                Status = "Trạng thái:\nĐã hoàn thành";
                ColorStatus = "Green";
            }
            OnPropertyChanged("Status"); OnPropertyChanged("ColorStatus");
            MemberCount = DataProvider.Ins.DB.MEMBERs.Where(x => x.idJourney == curJourney.Id).Count(); OnPropertyChanged("MemberCount");
            ListImg = new ObservableCollection<IMAGE_DESTINATION>(DataProvider.Ins.DB.IMAGE_DESTINATION.Where(x => x.idJourney == curJourney.Id)); OnPropertyChanged("ListImg");
            ListMem = new ObservableCollection<MEMBER>(DataProvider.Ins.DB.MEMBERs.Where(x => x.idJourney == curJourney.Id));
            ListExpe = new ObservableCollection<EXPENSE>(DataProvider.Ins.DB.EXPENSEs.Where(x => x.idJourney == curJourney.Id));
            foreach (var item in ListExpe)
            {
                totalCost += item.cost.GetValueOrDefault();
            }
            PieChart1 = new SeriesCollection();
            ListPaid = new ObservableCollection<MemberWithPaid>();
            ListMemCost = new ObservableCollection<Member_ObjectPay_Cost>();
            foreach (var item in ListMem)
            {
                int sum = 0;
                foreach (var iTem in ListExpe)
                {
                    if (iTem.idMember == item.id)
                    {
                        sum += iTem.cost.GetValueOrDefault();
                        Member_ObjectPay_Cost tmp = new Member_ObjectPay_Cost(item.id, iTem.id, item.C_name, iTem.objectPay, iTem.cost);
                        ListMemCost.Add(tmp);
                    }
                }
                double percent = ((double)sum / totalCost) * 100;
                if (sum > 0) 
                {
                    PieChart1.Add(new PieSeries { Values = new ChartValues<double> { Math.Round(percent,2) }, Title = item.C_name , DataLabels = true});
                }
                MemberWithPaid temp = new MemberWithPaid(item.C_name, sum);
                ListPaid.Add(temp);
            }
            OnPropertyChanged("PieChart1"); OnPropertyChanged("ListPaid"); OnPropertyChanged("ListMemCost");
        }
    }
}
