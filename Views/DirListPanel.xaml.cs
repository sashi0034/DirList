using DirList.Configs;
using DirList.Util;
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
        private ConfigRecord _config;
        public void RegisterConfig(ConfigRecord config)
        {
            Debug.Assert(_config == null);
            _config = config;
        }

        public void AddDir(DirPath path)
        {
            var dirLine = new DirLineElement(_config);
            dirLine.Dir = path;
            dirLine.AddEventOnPushDelete((_, _) =>
            {
                panel.Children.Remove(dirLine);
            });
            panel.Children.Add(dirLine);
        }

        public void ResetBy(List<DirPath> newList)
        {
            foreach (var oldChild in panel.Children)
            {
                var line = oldChild as DirLineElement;
                if (line == null) continue;
                panel.Children.Remove(line);
            }

            foreach (var dirPathList in newList)
            {
                AddDir(dirPathList);
            }
        }

        public void SortBy(Action<List<DirLineElement>> sortMethod)
        {
            var lineList = new List<DirLineElement>();

            // 子要素を取得
            foreach (var child in panel.Children)
            {
                var line = child as DirLineElement;
                if (line == null) continue;
                lineList.Add(line);
            }

            // 取り除く
            foreach (var line in lineList)
            {
                panel.Children.Remove(line);
            }

            sortMethod(lineList);

            // 戻す
            for (int i=0; i<lineList.Count; ++i)
            {
                panel.Children.Add(lineList[i]);
            }
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
