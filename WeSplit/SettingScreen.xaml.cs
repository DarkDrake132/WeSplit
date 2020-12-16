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
using System.Configuration;

namespace WeSplit
{
    /// <summary>
    /// Interaction logic for SettingScreen.xaml
    /// </summary>
    public partial class SettingScreen : Window
    {
        public SettingScreen()
        {
            InitializeComponent();

            var value = ConfigurationManager.AppSettings["checkShowDialog"];
            var showSplash = bool.Parse(value);

            SettingSplashScreen.IsChecked = showSplash;
        }
    }
}
