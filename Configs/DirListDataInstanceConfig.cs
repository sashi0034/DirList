
using DirList.Configs.Util;
using DirList.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DirList.Configs
{
    public class DirListDataInstanceConfig
    {
        private readonly BindingComboBox instanceItemList;
        public int SelectedIndex => instanceItemList.Selected;
        private readonly Views.DirListPanel dirListPanel;
        private readonly Views.TabSwitchPanel tabSwitchPanel;
        public List<DirListDataInstance> DataInstanceList { get; private set; } = new();

        private Window parentWIndow => Window.GetWindow(dirListPanel);
        private int _oldSelectedIndex = 0;

        public DirListDataInstanceConfig(Views.DataInstanceView view, Views.DirListPanel dirListPanel, Views.TabSwitchPanel tabSwitchPanel)
        {
            instanceItemList = new BindingComboBox(view.ComboBox);
            this.dirListPanel = dirListPanel;
            this.tabSwitchPanel = tabSwitchPanel;

            initDefaultInstance();

            view.ComboBox.AddEventOnSelect((_, _) => { onSelect(); });

            view.OnPushAdd += onPushAddNewInstance;
            view.OnPushRemove += onPushRemoveInstance;
            view.OnPushRename += onPushRenameInstance;

            tabSwitchPanel.OnChangeTab += (tabName) => 
            {
                instanceItemList.ChangeSelected(instanceItemList.FindIndexOf(tabName));
            };
        }

        private void initDefaultInstance()
        {
            var defaultData = new List<DirListDataInstance>();
            defaultData.Add(new DirListDataInstance(getNextDefaultInstanceName()));
            ResetDataInstanceList(defaultData, 0);
        }

        private void onSelect()
        {
            int newSelectedIndex = instanceItemList.Selected;
            if (newSelectedIndex == -1) return;
            if (_oldSelectedIndex!=-1) readFromPanel(_oldSelectedIndex);
            WriteToPanel();
            tabSwitchPanel.ChangeCurrentTabName(DataInstanceList[instanceItemList.Selected].InstanceName);
            _oldSelectedIndex = newSelectedIndex;
        }

        public void ResetDataInstanceList(List<DirListDataInstance> dataInstanceList, int index)
        {
            if (dataInstanceList.Count == 0) return;

            _oldSelectedIndex = -1;
            resetDataInstance(dataInstanceList);
            changeSelect(index);
        }

        private void changeSelect(int index)
        {
            instanceItemList.ChangeSelected(index);
            WriteToPanel();
        }

        /// <summary>
        /// ビューのパネルに現在選択しているインスタンスを反映
        /// </summary>

        public void WriteToPanel()
        {
            if (instanceItemList.Selected == -1) return;
            dirListPanel.ResetBy(DataInstanceList[instanceItemList.Selected].DirPathList);
        }

        /// <summary>
        /// ビューのパネルの内容を読み取り、現在のインスタンスの情報として格納
        /// </summary>
        public void ReadFromPanel()
        {
            readFromPanel(instanceItemList.Selected);
        }
        private void readFromPanel(int index)
        {
            if (index == -1) return;
            DataInstanceList[index].DirPathList = dirListPanel.GetDirList();
        }

        private void onPushAddNewInstance()
        {
            var inputWIndow = new Views.DataInstanceInputWindow(isValidInput);
            inputWIndow.Owner = parentWIndow;
            inputWIndow.SetWindowLeftTopTo(parentWIndow);
            inputWIndow.InputText = getNextDefaultInstanceName();
            inputWIndow.ShowDialog();

            if (!inputWIndow.IsConfirmed) return;

            var newInstanceName = inputWIndow.InputText;
            addNewDataInstance(newInstanceName);
            changeSelect(DataInstanceList.Count - 1);
        }

        private bool isValidInput(string input)
        {
            return 
                input!="" &&
                instanceItemList.ItemList.Contains(input) == false;
        }

        private string getNextDefaultInstanceName()
        {
            return "Instance " + (instanceItemList.ItemList.Count + 1);
        }

        private void onPushRemoveInstance()
        {
            if (instanceItemList.ItemList.Count == 1)
            {
                MessageBox.Show("これ以上削除することはできません", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Keyboard.IsKeyDown(Key.LeftCtrl) &&
                showAndConfirmRemoveDialog() != MessageBoxResult.OK) return;

            int selectedIndex = instanceItemList.Selected;
            string selectedName = instanceItemList.ItemList[selectedIndex];

            if (selectedIndex == instanceItemList.ItemList.Count - 1) instanceItemList.ChangeSelected(instanceItemList.Selected - 1);
            
            removeInstance(selectedIndex);
            
            tabSwitchPanel.RemoveByName(selectedName, instanceItemList.CurrentSelectedItem);
        }


        private MessageBoxResult showAndConfirmRemoveDialog()
        {
            var result = MessageBox.Show(
                instanceItemList.CurrentSelectedItem + " を削除しますか?\n( Ctrlを押しながら`削除'を押すと、このダイアログをスキップします )",
                "確認",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Question, MessageBoxResult.Cancel);

            return result;
        }

        private void onPushRenameInstance()
        {
            int selectedIndex = instanceItemList.Selected;
            string oldName = instanceItemList.ItemList[selectedIndex];
            var inputWIndow = new Views.DataInstanceInputWindow(newName => { return newName == oldName || isValidInput(newName); });
            inputWIndow.Owner = parentWIndow;
            inputWIndow.SetWindowLeftTopTo(parentWIndow);
            inputWIndow.InputText = oldName;
            inputWIndow.ShowBeforeName(oldName);
            inputWIndow.ShowDialog();

            if (!inputWIndow.IsConfirmed) return;

            var newName = inputWIndow.InputText;
            renameDataInstance(selectedIndex, newName);

            tabSwitchPanel.ReanameAll(oldName, newName);
        }

        private void resetDataInstance(List<DirListDataInstance> dataInstanceList)
        {
            DataInstanceList = dataInstanceList;
            instanceItemList.ResetItemList(
                dataInstanceList
                .Select(list => list.InstanceName)
                .ToList());
        }

        private void addNewDataInstance(string newInstanceName)
        {
            DataInstanceList.Add(new DirListDataInstance(newInstanceName));
            instanceItemList.UpdateItemList(list => list.Add(newInstanceName));
        }

        private void renameDataInstance(int selectedIndex, string newName)
        {
            DataInstanceList[selectedIndex].InstanceName = newName;
            instanceItemList.UpdateItemList(list => list[selectedIndex] = newName);
        }
        private void removeInstance(int selectedIndex)
        {
            DataInstanceList.RemoveAt(selectedIndex);
            instanceItemList.UpdateItemList(list => list.RemoveAt(selectedIndex));
        }

    }
}
