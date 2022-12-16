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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DirList.Views
{
    /// <summary>
    /// DataInstanceTab.xaml の相互作用ロジック
    /// </summary>
    public partial class TabSwitchElement : UserControl
    {
        public readonly Brush initialBg;

        public TabSwitchElement()
        {
            InitializeComponent();
            initialBg = border.Background;
        }


        public bool IsActive
        {
            get => border.Background == initialBg;
            set
            {
                border.Background = value
                    ? initialBg
                    : Brushes.Transparent;
            }
        }
    }
}
