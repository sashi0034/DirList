using DirList.Configs;
using DirList.Util;
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
    /// DirListPanel.xaml の相互作用ロジック
    /// </summary>
    public partial class DirListPanel : UserControl
    {
        public DirListPanel()
        {
            InitializeComponent();
        }

        public void AddDir(ConfigRecord configRecord, DirPath path)
        {
            var dirLine = new DirLineElement(configRecord);
            dirLine.Dir = path;
            dirLine.AddEventOnPushDelete((_, _) =>
            {
                panel.Children.Remove(dirLine);
            });
            panel.Children.Add(dirLine);
        }

        public List<DirPath> GetDirList()
        {
            var list = new List<DirPath>();
            foreach (var child in panel.Children)
            {
                var line = child as DirLineElement;
                if (line == null) continue;
                list.Add(line.Dir);
            }
            return list;
        }
    }

}
