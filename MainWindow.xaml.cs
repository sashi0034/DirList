﻿using DirList.Configs;
using DirList.Util;
using DirList.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DirList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ConfigRecord _configRecord;

        public MainWindow()
        {
            InitializeComponent();

            _configRecord = new ConfigRecord(
                new OptionOnCopy(viewOptionOnCopy),
                new ProgramForOpen(viewProgramForOpen)
                );
        }


        private void dirPathInput_onConfirm(DirPath path)
        {
            var dirLine = new DirLineElement(_configRecord);
            dirLine.Dir = path;
            dirLine.AddEventOnPushDelete((_, _) =>
            {
                panelDirList.Children.Remove(dirLine);
            });
            panelDirList.Children.Add(dirLine);
        }
    }
}
