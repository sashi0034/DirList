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
    /// DataInstanceView.xaml の相互作用ロジック
    /// </summary>
    public partial class DataInstanceView : UserControl
    {
        public DataInstanceView()
        {
            InitializeComponent();
        }

        public ConfigComboBox ComboBox => viewComboBox;

        public event Action OnPushRemove;
        public event Action OnPushAdd;
        public event Action OnPushRename;

        private void buttonRemoveDataInstance_Click(object sender, RoutedEventArgs e)
        {
            OnPushRemove();
        }

        private void buttonAddNewDataInstance_Click(object sender, RoutedEventArgs e)
        {
            OnPushAdd();
        }

        private void buttonRenameDataInstance_Click(object sender, RoutedEventArgs e)
        {
            OnPushRename();
        }
    }
}
