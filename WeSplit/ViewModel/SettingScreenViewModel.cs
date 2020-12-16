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
    public class SettingScreenViewModel : BaseViewModel
    {
        public ICommand ConfirmCommand { get; set; }
        public bool CheckDialog { get; set; }

        public SettingScreenViewModel()
        {
            ConfirmCommand = new RelayCommand<SettingScreen>((p) =>
            {
                return true;
            }, (p) =>
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["checkShowDialog"].Value = CheckDialog ? "true" : "false";
                config.Save(ConfigurationSaveMode.Minimal);
                ConfigurationManager.RefreshSection("appSettings");

                //p.Close();
            });
        }
    }
}
