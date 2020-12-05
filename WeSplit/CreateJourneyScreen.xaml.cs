using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WeSplit.ViewModel;

namespace WeSplit
{
    /// <summary>
    /// Interaction logic for CreateJourneyScreen.xaml
    /// </summary>
    public partial class CreateJourneyScreen : Window
    {
        public CreateJourneyViewModel VM { get; set; }
        public CreateJourneyScreen()
        {
            InitializeComponent();
            DataContext = VM = new CreateJourneyViewModel();
        }
    }
}
