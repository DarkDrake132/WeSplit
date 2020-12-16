using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using WeSplit.Model;
using System.Windows.Input;
using System.Configuration;

namespace WeSplit.ViewModel
{
    class SplashScreenViewModel : BaseViewModel
    {
        public ICommand CloseCommand { get; set; }
        //public ICommand CloseDialogCommand { get; set; }
        public string ShowImage { get => _ShowImage; set => _ShowImage = value; }
        public string SplashTitle { get => _SplashTitle; set => _SplashTitle = value; }
        public string LocationName { get => _LocationName; set => _LocationName = value; }
        public bool CloseDialog { get => _CloseDialog; set => _CloseDialog = value; }

        private string _ShowImage;
        private string _SplashTitle;
        private string _LocationName;
        private bool _CloseDialog;

        public Random rng = new Random();
        public SplashScreenViewModel()
        {
            var maxId = DataProvider.Ins.DB.JOURNEYs.Max(x => x.id);
            int index = rng.Next(1, maxId);
            var i = DataProvider.Ins.DB.JOURNEYs.Where(x => x.id == index).SingleOrDefault();
            ShowImage = i.thumbnailLink;
            SplashTitle = i.title;
            LocationName = i.C_location;

            CloseCommand = new RelayCommand<SplashScreen>((p) =>
            {
                return true;
            }, (p) =>
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["checkShowDialog"].Value = (CloseDialog)?"false":"true";
                config.Save(ConfigurationSaveMode.Minimal);

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                p.Close();
            });
            //CloseDialogCommand = new RelayCommand<object>((p) =>
            //{
            //    return true;
            //}, (p) =>
            // {
            //     var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //     config.AppSettings.Settings["checkShowDialog"].Value = "false";
            //     config.Save(ConfigurationSaveMode.Minimal);
            // });
        }
    }
}
