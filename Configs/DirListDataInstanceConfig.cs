
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
        private readonly BindingComboBox _instanceItemList;
        public int SelectedIndex => _instanceItemList.Selected;
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
            view.OnPushRemove += onPushRemoveInstance;
            view.OnPushRename += onPushRenameInstance;
        }

        private void initDefaultInstance()
        {
            var defaultData = new List<DirListDataInstance>();
            defaultData.Add(new DirListDataInstance(getNextDefaultInstanceName()));
            ResetDataInstanceList(defaultData, 0);
        }

        private void onSelect()
        {
            int newSelectedIndex = _instanceItemList.Selected;
            if (newSelectedIndex == -1) return;
            if (_oldSelectedIndex!=-1) readFromPanel(_oldSelectedIndex);
            WriteToPanel();
            _oldSelectedIndex = newSelectedIndex;
        }

        public void ResetDataInstanceList(List<DirListDataInstance> dataInstanceList, int index)
        {
            if (dataInstanceList.Count == 0) return;

            resetDataInstance(dataInstanceList);
            forceChangeSelect(index);
        }

        private void forceChangeSelect(int index)
        {
            _instanceItemList.Selected = index;
            WriteToPanel();
        }

        /// <summary>
        /// ビューのパネルに現在選択しているインスタンスを反映
        /// </summary>

        public void WriteToPanel()
        {
            if (_instanceItemList.Selected == -1) return;
            _dirListPanel.ResetBy(DataInstanceList[_instanceItemList.Selected].DirPathList);
        }

        /// <summary>
        /// ビューのパネルの内容を読み取り、現在のインスタンスの情報として格納
        /// </summary>
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
            var inputWIndow = new Views.DataInstanceInputWindow(isValidInput);
            inputWIndow.Owner = parentWIndow;
            inputWIndow.SetWindowLeftTopTo(parentWIndow);
            inputWIndow.InputText = getNextDefaultInstanceName();
            inputWIndow.ShowDialog();

            if (!inputWIndow.IsConfirmed) return;

            var newInstanceName = inputWIndow.InputText;
            addNewDataInstance(newInstanceName);
            forceChangeSelect(DataInstanceList.Count - 1);
        }

        private bool isValidInput(string input)
        {
            return 
                input!="" &&
                _instanceItemList.ItemList.Contains(input) == false;
        }

        private string getNextDefaultInstanceName()
        {
            return "Instance " + (_instanceItemList.ItemList.Count + 1);
        }

        private void onPushRemoveInstance()
        {
            if (_instanceItemList.ItemList.Count == 1)
            {
                MessageBox.Show("これ以上削除することはできません", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Keyboard.IsKeyDown(Key.LeftCtrl) &&
                showAndConfirmRemoveDialog() != MessageBoxResult.OK) return;

            int selectedIndex = _instanceItemList.Selected;
            if (selectedIndex == _instanceItemList.ItemList.Count - 1) _instanceItemList.Selected--;
            removeInstance(selectedIndex);
        }


        private MessageBoxResult showAndConfirmRemoveDialog()
        {
            var result = MessageBox.Show(
                _instanceItemList.CurrentSelectedItem + " を削除しますか?\n( Ctrlを押しながら`削除'を押すと、このダイアログをスキップします )",
                "確認",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Question, MessageBoxResult.Cancel);

            return result;
        }

        private void onPushRenameInstance()
        {
            int selectedIndex = _instanceItemList.Selected;
            string oldName = _instanceItemList.ItemList[selectedIndex];
            var inputWIndow = new Views.DataInstanceInputWindow(newName => { return newName == oldName || isValidInput(newName); });
            inputWIndow.Owner = parentWIndow;
            inputWIndow.SetWindowLeftTopTo(parentWIndow);
            inputWIndow.InputText = oldName;
            inputWIndow.ShowBeforeName(oldName);
            inputWIndow.ShowDialog();


            if (!inputWIndow.IsConfirmed) return;

            renameDataInstance(selectedIndex, inputWIndow.InputText);
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

        private void renameDataInstance(int selectedIndex, string newName)
        {
            DataInstanceList[selectedIndex].InstanceName = newName;
            _instanceItemList.UpdateItemList(list => list[selectedIndex] = newName);
        }
        private void removeInstance(int selectedIndex)
        {
            DataInstanceList.RemoveAt(selectedIndex);
            _instanceItemList.UpdateItemList(list => list.RemoveAt(selectedIndex));
        }

    }
}
