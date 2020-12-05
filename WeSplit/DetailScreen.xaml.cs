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
    /// Interaction logic for DetailScreen.xaml
    /// </summary>
    public partial class DetailScreen : Window
    {
        public DetailViewModel VM { get; set; }
        public DetailScreen()
        {
            InitializeComponent();
            DataContext = VM = new DetailViewModel();
        }
    }
}
