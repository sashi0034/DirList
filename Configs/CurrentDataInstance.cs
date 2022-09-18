﻿
using DirList.Configs.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DirList.Configs
{
    public class CurrentDataInstance
    {
        private readonly BindingComboBox _instanceList;
        public BindingComboBox InstanceList => _instanceList;
        private readonly Views.DirListPanel _dirListPanel;
        private Window parentWIndow => Window.GetWindow(_dirListPanel);

        public CurrentDataInstance(Views.DataInstanceView view, Views.DirListPanel dirListPanel)
        {
            _instanceList = new BindingComboBox(view.ComboBox);
            _dirListPanel = dirListPanel;

            view.OnPushAdd += addNewInstance;
            view.OnPushRemove += removeInstance;
            view.OnPushRename += renameInstance;
        }

        private void addNewInstance()
        {
            var inputWIndow = new Views.DataInstanceInputWindow();
            inputWIndow.Owner = parentWIndow;
            inputWIndow.ShowDialog();

            if (!inputWIndow.IsConfirmed) return;
            _instanceList.UpdateItemList(list => list.Add(inputWIndow.InputText));
        }

        private void removeInstance()
        {
            
        }

        private void renameInstance()
        {

        }
    }
}
