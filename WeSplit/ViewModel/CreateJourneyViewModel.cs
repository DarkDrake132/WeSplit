using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeSplit.Model;
using System.IO;
using System.Windows.Forms;

namespace WeSplit.ViewModel
{
    public class CreateJourneyViewModel : BaseViewModel
    {
        public string AddLocation { get => _AddLocation; set => _AddLocation = value; }
        public string AddTitle { get => _AddTitle; set => _AddTitle = value; }
        public ObservableCollection<string> ListImage { get => _ListImage; set => _ListImage = value; }
        public string Image { get => _Image; set => _Image = value; }
        public string AddState { get => _AddState; set => _AddState = value; }

        private string _AddLocation;
        private string _AddTitle;
        private string _AddState;
        private ObservableCollection<string> _ListImage = new ObservableCollection<string> { "" };//Make sure take exactly type of image
        private string _Image;

        public ICommand AddMainImage { get; set; }
        public ICommand AddActivitiesImage { get; set; }
        public ICommand SaveAll { get; set; }
  

        private void getFileName(ref string path)
        {
            string res = System.IO.Path.GetFileName(path);
            path = path.Substring(0, path.IndexOf(res) - 1);
            //return res;
        }
        private void changedLocation(string sourcePath, string name, ref string newName)
        {
            string targetPath = Environment.CurrentDirectory.ToString() + "/Images";
            getFileName(ref sourcePath);
            string sourceFile = System.IO.Path.Combine(@sourcePath, name);
            var tail = name.Substring(name.Length - 4);
            name = DateTime.Now.ToString("yyyyMMddHHmmssfff") + tail;
            string destFile = System.IO.Path.Combine(@targetPath, name);
            FileInfo f1 = new FileInfo(sourceFile);
            FileInfo f2 = new FileInfo(destFile);
            f1.CopyTo(destFile);

            newName = name;
        }

        public CreateJourneyViewModel()
        {
            var id = DataProvider.Ins.DB.JOURNEYs.Max(x => x.id);
            id += 1;
            
            AddMainImage = new RelayCommand<object>((p) =>
            {
                if (ListImage[0] != "")
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;
                ofd.Filter = "(*.jpg)|*.jpg|(*.png)|.*png";
                DialogResult dr = ofd.ShowDialog();
                if(dr == System.Windows.Forms.DialogResult.OK)
                {
                    string file = ofd.FileName;
                    ListImage[0] = file;
                    
                }
                Image = ListImage[0];
            });

            AddActivitiesImage = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p)=> {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.Filter = "(*.jpg)|*.jpg|(*.png)|.*png";
                DialogResult dr = ofd.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    ListImage.Add(ofd.FileName.ToString());
                }
            });

            

            SaveAll = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(AddLocation))
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                for (int i = 0; i < ListImage.Count; i++)
                {
                    string newName = "";
                    changedLocation(ListImage[i], System.IO.Path.GetFileName(ListImage[i]), ref newName);
                    ListImage[i] = newName;
                }
                
                DataProvider.Ins.DB.JOURNEYs.Add(new JOURNEY() { id = id, C_location = AddLocation, title = AddTitle, isFinish = (AddState.Contains("Kết thúc")) ? 1 : 0, thumbnailLink = ListImage[0] });
                
                DataProvider.Ins.DB.SaveChanges();
            });
        }

    }
}
