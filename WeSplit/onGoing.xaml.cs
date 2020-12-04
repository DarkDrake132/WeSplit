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

namespace WeSplit
{
    /// <summary>
    /// Interaction logic for onGoing.xaml
    /// </summary>
    public partial class onGoing : UserControl
    {
        public onGoing()
        {
            InitializeComponent();
        }
        //class Journey
        //{
        //    public string Title { get; set; }
        //    public string Image { get; set; }
        //    public string Location { get; set; }
        //}

        //class JourneyDAO
        //{
        //    public static BindingList<Journey> GetAll()
        //    {
        //        var result = new BindingList<Journey>()
        //        {
        //            new Journey() { Title="Chu Tùng Nhân", Image="/Images/image1.jpg", Location="New York" },
        //            new Journey() { Title="Chu Tùng Nhân", Image="/Images/image2.jpg", Location="New York" },
        //            new Journey() { Title="Chu Tùng Nhân", Image="/Images/image3.jpg", Location="New York" },
        //            new Journey() { Title="Chu Tùng Nhân", Image="/Images/image4.jpg", Location="New York" },
        //            new Journey() { Title="Chu Tùng Nhân", Image="/Images/image5.jpg", Location="New York" },
        //            new Journey() { Title="Chu Tùng Nhân", Image="/Images/image6.jpg", Location="New York" },
        //        };

        //        return result;
        //    }
        //}
        //BindingList<Journey> _list = new BindingList<Journey>();

        //private void UserControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    _list = JourneyDAO.GetAll();
        //    listJourney.ItemsSource = _list;
        //}
    }
}
