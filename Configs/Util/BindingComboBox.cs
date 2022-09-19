﻿using DirList.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirList.Configs.Util
{
    public class BindingComboBox
    {
        private readonly Views.ConfigComboBox _view;
        private readonly List<string> _itemList = new List<string>();

        public BindingComboBox(ConfigComboBox view)
        {
            _view = view;
        }
        public IReadOnlyList<string> ItemList => _itemList;

        public void UpdateItemList(Action<List<String>> func)
        {
            int selected = Selected;

            func(_itemList);
            _view.ResetComboItem(_itemList);

            Selected = Math.Min(selected, _itemList.Count - 1);
        }
        public void ResetItemList(List<string> list)
        {
            UpdateItemList(before =>
            {
                before.Clear();
                before.AddRange(list);
            });
        }
        public int Selected
        {
            get { return _view.GetSelectedIndex(); }
            set { _view.SetSelectedIndex(value); }
        }
    }
}
