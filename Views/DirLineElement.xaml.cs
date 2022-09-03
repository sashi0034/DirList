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
    /// DirLineElement.xaml の相互作用ロジック
    /// </summary>
    public partial class DirLineElement : UserControl
    {
        public DirLineElement()
        {
            InitializeComponent();

            Dir = new DirPath("C:\\dev");
        }

        public DirPath Dir
        {
            get
            {
                return new DirPath(buttonDir.Content as string);
            }
            set
            {
                buttonDir.Content = value.Path;
            }
        }

        public void AddEventOnPushDelete(RoutedEventHandler e)
        {
            buttonDelete.Click += e;
        }


    }
}
