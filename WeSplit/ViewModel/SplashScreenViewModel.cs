using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using WeSplit.Model;

namespace WeSplit.ViewModel
{
    class SplashScreenViewModel : BaseViewModel
    {
        public string ShowImage { get => _ShowImage; set => _ShowImage = value; }

        private string _ShowImage;
        public Random rng = new Random();
        public SplashScreenViewModel()
        {
            var maxId = DataProvider.Ins.DB.JOURNEYs.Max(x => x.id);
            int index = rng.Next(1, maxId);
            var i = DataProvider.Ins.DB.JOURNEYs.Where(x => x.id == index).SingleOrDefault();
            ShowImage = i.thumbnailLink;
        }
    }
}
