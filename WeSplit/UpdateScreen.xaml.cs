﻿using System;
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
using WeSplit.ViewModel;

namespace WeSplit
{
    /// <summary>
    /// Interaction logic for UpdateScreen.xaml
    /// </summary>
    public partial class UpdateScreen : Window
    {
        public UpdateScreenViewModel VM { get; set; }

        public UpdateScreen()
        {
            InitializeComponent();
            DataContext = VM = new UpdateScreenViewModel();
        }
    }
}
