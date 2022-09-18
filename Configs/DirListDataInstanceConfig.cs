
using DirList.Configs.Util;
using DirList.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DirList.Configs
{
    public class DirListDataInstanceConfig
    {
        private readonly BindingComboBox _instanceItemList;
        private readonly Views.DirListPanel _dirListPanel;
        public List<DirListDataInstance> DataInstanceList { get; private set; } = new();

        private Window parentWIndow => Window.GetWindow(_dirListPanel);
        private int _oldSelectedIndex = 0;

        public DirListDataInstanceConfig(Views.DataInstanceView view, Views.DirListPanel dirListPanel)
        {
            _instanceItemList = new BindingComboBox(view.ComboBox);
            _dirListPanel = dirListPanel;

            initDefaultInstance();

            view.ComboBox.AddEventOnSelect((_, _) => { onSelect(); });

            view.OnPushAdd += onPushAddNewInstance;
            view.OnPushRemove += removeInstance;
            view.OnPushRename += renameInstance;
        }

        private void initDefaultInstance()
        {
            var defaultData = new List<DirListDataInstance>();
            defaultData.Add(new DirListDataInstance("Instance 1"));
            ResetDataInstanceList(defaultData);
        }

        private void onSelect()
        {
            int newSelectedIndex = _instanceItemList.Selected;
            if (newSelectedIndex == -1) return;
            if (newSelectedIndex != _oldSelectedIndex) readFromPanel(_oldSelectedIndex);
            WriteToPanel();
            _oldSelectedIndex = newSelectedIndex;
        }

        public void ResetDataInstanceList(List<DirListDataInstance> dataInstanceList)
        {
            if (dataInstanceList.Count == 0) return;

            resetDataInstance(dataInstanceList);
            forceChangeSelect(0);
        }

        private void forceChangeSelect(int index)
        {
            _instanceItemList.Selected = index;
            WriteToPanel();
            _oldSelectedIndex = index;
        }

        public void WriteToPanel()
        {
            if (_instanceItemList.Selected == -1) return;
            _dirListPanel.ResetBy(DataInstanceList[_instanceItemList.Selected].DirPathList);
        }
        public void ReadFromPanel()
        {
            readFromPanel(_instanceItemList.Selected);
        }
        private void readFromPanel(int index)
        {
            if (index == -1) return;
            DataInstanceList[index].DirPathList = _dirListPanel.GetDirList();
        }

        private void onPushAddNewInstance()
        {
            var inputWIndow = new Views.DataInstanceInputWindow();
            inputWIndow.Owner = parentWIndow;
            inputWIndow.SetWindowLeftTopTo(parentWIndow);
            inputWIndow.ShowDialog();

            if (!inputWIndow.IsConfirmed) return;

            var newInstanceName = inputWIndow.InputText;
            addNewDataInstance(newInstanceName);
            forceChangeSelect(DataInstanceList.Count-1);
        }

        private void removeInstance()
        {

        }

        private void renameInstance()
        {

        }

        private void resetDataInstance(List<DirListDataInstance> dataInstanceList)
        {
            DataInstanceList = dataInstanceList;
            _instanceItemList.ResetItemList(
                dataInstanceList
                .Select(list => list.InstanceName)
                .ToList());
        }

        private void addNewDataInstance(string newInstanceName)
        {
            DataInstanceList.Add(new DirListDataInstance(newInstanceName));
            _instanceItemList.UpdateItemList(list => list.Add(newInstanceName));
        }

    }
}
