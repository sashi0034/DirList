
using DirList.Configs.Util;
using DirList.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DirList.Configs
{
    public class DirListDataInstanceConfig
    {
        private readonly BindingComboBox _instanceItemList;
        private readonly Views.DirListPanel _dirListPanel;
        public List<DirListDataInstance> DataInstanceList { get; private set; } = new();

        private Window parentWIndow => Window.GetWindow(_dirListPanel);

        public DirListDataInstanceConfig(Views.DataInstanceView view, Views.DirListPanel dirListPanel)
        {
            _instanceItemList = new BindingComboBox(view.ComboBox);
            _dirListPanel = dirListPanel;

            initDefaultInstance();

            view.OnPushAdd += addNewInstance;
            view.OnPushRemove += removeInstance;
            view.OnPushRename += renameInstance;
        }

        private void initDefaultInstance()
        {
            var defaultData = new List<DirListDataInstance>();
            defaultData.Add(new DirListDataInstance("Instance 1"));
            ResetDataInstanceList(defaultData);
        }


        public void ResetDataInstanceList(List<DirListDataInstance> dataInstanceList)
        {
            if (dataInstanceList.Count==0) return;

            DataInstanceList = dataInstanceList;
            _instanceItemList.ResetItemList(
                dataInstanceList
                .Select(list => list.InstanceName)
                .ToList());
            _instanceItemList.Selected = 0;
            WriteToPanel();
        }

        public void WriteToPanel()
        {
            _dirListPanel.ResetBy(DataInstanceList[_instanceItemList.Selected].DirPathList);
        }
        public void ReadFromPanel()
        {
            DataInstanceList[_instanceItemList.Selected].DirPathList = _dirListPanel.GetDirList();
        }

        private void addNewInstance()
        {
            var inputWIndow = new Views.DataInstanceInputWindow();
            inputWIndow.Owner = parentWIndow;
            inputWIndow.SetWindowLeftTopTo(parentWIndow);
            inputWIndow.ShowDialog();

            if (!inputWIndow.IsConfirmed) return;
            _instanceItemList.UpdateItemList(list => list.Add(inputWIndow.InputText));
        }

        private void removeInstance()
        {
            
        }

        private void renameInstance()
        {

        }
    }
}
