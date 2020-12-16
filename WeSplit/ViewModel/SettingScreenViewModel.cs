using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Input;

namespace WeSplit.ViewModel
{
    class SettingScreenViewModel
    {
        ICommand CheckCommand { get; set; }
        ICommand ConfirmCommand { get; set; }

        public SettingScreenViewModel()
        {

            CheckCommand = new RelayCommand<SettingScreen>((p) =>
            {
                return true;
            }, (p) =>
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["checkShowDialog"].Value = CheckCommand.ToString();
                config.Save(ConfigurationSaveMode.Minimal);
                ConfigurationManager.RefreshSection("appSettings");
                p.Close();
            });
        }
    }
}
