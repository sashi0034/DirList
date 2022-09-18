using System;
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

namespace DirList.Views
{
    /// <summary>
    /// InputDataInstanceWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class DataInstanceInputWindow : Window
    {
        public DataInstanceInputWindow()
        {
            InitializeComponent();
        }

        public bool IsConfirmed { get; private set; }

        public string InputText
        {
            get { return inputBox.Text; }
            set { inputBox.Text = value; }
        }

        private void buttonConfirm_Click(object sender, RoutedEventArgs e)
        {
            IsConfirmed = true;
            Close();
        }
    }
}
