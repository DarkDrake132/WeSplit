using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeSplit.ViewModel
{
    class UpdateScreenViewModel : BaseViewModel
    {
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
            ColorStatus = "Red";
            Status = "Đang thực hiện";
        }
    }
}
