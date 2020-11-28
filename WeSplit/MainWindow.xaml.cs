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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeSplit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            GridPrinciple.Children.Clear();
            GridPrinciple.Children.Add(new onGoing());
        }

        private void onGoing_Button_Click(object sender, RoutedEventArgs e)
        {
            GridPrinciple.Children.Clear();
            GridPrinciple.Children.Add(new onGoing());
            onGoing_Button.Background = Brushes.Black;
            usedToGo_Button.Background = Brushes.DimGray;
        }

        private void usedToGo_Button_Click(object sender, RoutedEventArgs e)
        {
            GridPrinciple.Children.Clear();
            GridPrinciple.Children.Add(new usedToGo());
            usedToGo_Button.Background = Brushes.Black;
            onGoing_Button.Background = Brushes.DimGray;
        }
    }
}
