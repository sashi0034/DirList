using DirList.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirList.Configs
{
    public enum DirListSortKind
    {
        [Description("作成順")]
        Time,
        [Description("名前順")]
        Name,
    }

    public class DirListSort
    {
        private readonly Views.ConfigComboBox _view;
        private readonly Views.DirListPanel _dirListPanel;
        public DirListSort(Views.ConfigComboBox view, Views.DirListPanel dirListPanel)
        {
            var descriptionList = UtilFunc.GetEnumDescriptionList<DirListSortKind>();
            view.ResetComboItem(descriptionList);
            _view = view;
            _dirListPanel = dirListPanel;

            view.AddEventOnSelect((_, _) => { sortPanel(); });
        }
        public DirListSortKind Selected
        {
            get { return _view.GetSelectedIndex<DirListSortKind>(); }
            set { _view.SetSelectedIndex(value); }
        }
        private void sortPanel()
        {
            _dirListPanel.SortBy((list) =>
            {
                switch (Selected)
                {
                    case DirListSortKind.Time:
                        list.Sort((left, right) => { return left.Dir.PushedTime.CompareTo(right.Dir.PushedTime); });
                        break;
                    case DirListSortKind.Name:
                        list.Sort((left, right) => { return left.Dir.Path.CompareTo(right.Dir.Path); });
                        break;
                }
            });
        }

    }
}
