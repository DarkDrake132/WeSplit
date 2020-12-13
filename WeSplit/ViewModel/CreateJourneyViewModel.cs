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
        public int id = DataProvider.Ins.DB.JOURNEYs.Max(x => x.id) + 1;

        private ObservableCollection<MEMBER> _ListMember = new ObservableCollection<MEMBER>();
        public ObservableCollection<MEMBER> ListMember { get => _ListMember; set => _ListMember = value; }

        private ObservableCollection<EXPENSE> _ListExpense = new ObservableCollection<EXPENSE>();
        public ObservableCollection<EXPENSE> ListExpense { get => _ListExpense; set => _ListExpense = value; }

        public string AddLocation { get => _AddLocation; set => _AddLocation = value; }
        public string AddTitle { get => _AddTitle; set => _AddTitle = value; }
        public ObservableCollection<IMAGE_DESTINATION> ListImage { get => _ListImage; set => _ListImage = value; }
        public string Image { get => _Image; set => _Image = value; }
        public string AddState { get => _AddState; set => _AddState = value; }

        private string _AddLocation;
        private string _AddTitle;
        private string _AddState;
        private ObservableCollection<IMAGE_DESTINATION> _ListImage = new ObservableCollection<IMAGE_DESTINATION>();
        private string _Image;

        public ICommand AddMainImage { get; set; }
        public ICommand AddActivitiesImage { get; set; }
        public ICommand SaveAll { get; set; }
        public ICommand AddMember { get; set; }
        public ICommand AddExpense { get; set; }
        private void getFileName(ref string path)
        {
            string res = System.IO.Path.GetFileName(path);
            path = path.Substring(0, path.IndexOf(res) - 1);
            //return res;
        }
        private void changedLocation(string sourcePath, string name, ref string newName)
        {
            string targetPath = Environment.CurrentDirectory.ToString();
            string temp = System.IO.Directory.GetParent(targetPath).ToString();
            temp = System.IO.Directory.GetParent(temp).ToString();
            targetPath = temp + "/Images";

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
            string mainImg = "";
            AddMainImage = new RelayCommand<object>((p) =>
            {
                if(mainImg != "")
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
                    ListImage.Add(new IMAGE_DESTINATION());
                    ListImage[ListImage.Count - 1].idJourney = id;
                    ListImage[ListImage.Count - 1].id = -1;
                    ListImage[ListImage.Count - 1].imageLink = ofd.FileName.ToString();

                    mainImg = ListImage[ListImage.Count - 1].imageLink;
                }
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
                    ListImage.Add(new IMAGE_DESTINATION());
                    ListImage[ListImage.Count - 1].idJourney = id;
                    ListImage[ListImage.Count - 1].imageLink = ofd.FileName.ToString();
                }
            });

            AddExpense = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p)=> {
                ListExpense.Add(new EXPENSE());
            });

            AddMember = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) => 
            {
                ListMember.Add(new MEMBER());
                var IdMember = DataProvider.Ins.DB.MEMBERs.Max(x => x.id);
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
                //change images' location and rename it
                string newName = "";
                for (int i = 0; i < ListImage.Count; i++)
                {
                    changedLocation(ListImage[i].imageLink, System.IO.Path.GetFileName(ListImage[i].imageLink), ref newName);
                    ListImage[i].imageLink = newName;
                }
                changedLocation(mainImg, System.IO.Path.GetFileName(mainImg), ref newName);
                mainImg = newName;

                //Start save Database
                DataProvider.Ins.DB.JOURNEYs.Add(new JOURNEY() { id = id, C_location = AddLocation, title = AddTitle, isFinish = (AddState.Contains("Kết thúc")) ? 1 : 0, thumbnailLink = mainImg });

                //members' DB
                var IdMember = 1;
                foreach (var temp in ListMember)
                {
                    temp.idJourney = id;
                    temp.id = IdMember;
                    IdMember += 1;
                    DataProvider.Ins.DB.MEMBERs.Add(temp);
                    DataProvider.Ins.DB.SaveChanges();
                }

                //Images' DB
                var IdImage = 1;
                foreach (var temp in ListImage)
                {
                    if(temp.id != -1)
                    {
                        temp.id = IdImage;
                        IdImage += 1;
                        DataProvider.Ins.DB.IMAGE_DESTINATION.Add(temp);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                }

                DataProvider.Ins.DB.SaveChanges();
            });
        }

    }
}
