using System;
using System.Collections;
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
    /// ConfigComboBox.xaml の相互作用ロジック
    /// </summary>
    public partial class ConfigComboBox : UserControl
    {
        public ConfigComboBox()
        {
            InitializeComponent();
            combo.SelectedIndex = 0;
        }

        public string Text
        {
            get
            {
                return text.Text;
            }
            set
            {
                text.Text = value;
            }
        }

        public void ResetComboItem(List<string> items)
        {
            combo.Items.Clear();

            foreach (string item in items)
                combo.Items.Add(item);
        }

        //public IEnumerable Source
        //{
        //    get
        //    {
        //        return combo.ItemsSource;
        //    }
        //    set
        //    {
        //        combo.ItemsSource = value;
        //    }
        //}

        public T SelectedIndex<T>() where T : Enum
        {
            return (T)(Object)combo.SelectedIndex;
        }
    }
}
