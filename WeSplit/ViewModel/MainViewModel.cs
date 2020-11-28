using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace WeSplit.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand CreateJourneyCommand { get; set; }

        public MainViewModel()
        {
            CreateJourneyCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CreateJourneyScreen cjs = new CreateJourneyScreen();
                cjs.ShowDialog();
            });

        }
    }
}
