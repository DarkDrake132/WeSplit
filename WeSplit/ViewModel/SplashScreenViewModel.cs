using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WeSplit.ViewModel
{
    class SplashScreenViewModel : BaseViewModel
    {
        public string ShowImage { get => _ShowImage; set => _ShowImage = value; }

        private string _ShowImage;
        public SplashScreenViewModel()
        {
            ShowImage = "Images/image1.jpg";
        }
    }
}
