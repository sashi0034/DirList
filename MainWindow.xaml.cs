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
        public MainWindow()
        {
            InitializeComponent();
        }


        private void dirPathInput_onConfirm(DirPath path)
        {
            var dirLine = new DirLineElement();
            dirLine.Dir = path;
            dirLine.AddEventOnPushDelete((_, _) =>
            {
                panelDirList.Children.Remove(dirLine);
            });
            panelDirList.Children.Add(dirLine);
        }
    }
}
