using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeSplit.ViewModel;

namespace WeSplit
{
    /// <summary>
    /// Interaction logic for onGoing.xaml
    /// </summary>
    public partial class onGoing : UserControl
    {
        public OnGoingViewModel VM { get; set; }
        public onGoing()
        {
            InitializeComponent();
            DataContext = VM = new OnGoingViewModel();
        }
    }
}
