using DirList.Configs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    /// TabSwitchArea.xaml の相互作用ロジック
    /// </summary>
    public partial class TabSwitchPanel : UserControl
    {
        private int _currentIndex = 0;
        public event Action<string> OnChangeTab;

        public TabSwitchPanel()
        {
            InitializeComponent();
        }

        public void Setup(TabData tabData, DirListDataInstanceConfig dataInstanceConfig)
        {
            var tabNames = tabData != null && tabData.TabNames.Count > 0
                ? tabData.TabNames
                : new List<string>() { dataInstanceConfig.DataInstanceList[dataInstanceConfig.SelectedIndex].InstanceName };

            foreach (var tab in tabNames)
            {
                addNewTab(tab);
            }

            changeTabIndex(tabData != null ? tabData.CurrentIndex : 0);
        }

        public TabData Save()
        {
            List<string> tabNames = new();
            for (int i=0; i<stackTabs.Children.Count; ++i)
            {
                tabNames.Add(getTabByIndex(i).TitleName);
            }

            return new TabData()
            {
                CurrentIndex = _currentIndex,
                TabNames = tabNames
            };
        }

        private TabSwitchElement addNewTab(string tabName)
        {
            var tab = new TabSwitchElement(tabName);
            tab.AddEventOnButtonClose((_, _)=> 
            {
                if (stackTabs.Children.Count <= 1) return;
                stackTabs.Children.Remove(tab);
                roundTabIndex();
            });
            tab.AddEventOnClickTab((_, _) =>
            {
                int newIndex = stackTabs.Children.IndexOf(tab);
                changeTabIndex(newIndex);
            });
            stackTabs.Children.Add(tab);
            return tab;
        }

        private void roundTabIndex()
        {
            changeTabIndex(Math.Min(_currentIndex, stackTabs.Children.Count - 1));
        }

        public void ReanameAll(string beforeName, string afterName)
        {
            for (int i=0; i < stackTabs.Children.Count; i++)
            {
                if (getTabByIndex(i).TitleName == beforeName)
                    getTabByIndex(i).TitleName = afterName;
            }
        }
        public void RemoveByName(string name, string substituteForEmpty)
        {
            for (int i = stackTabs.Children.Count - 1; i >= 0; i--)
            {
                if (getTabByIndex(i).TitleName == name)
                    stackTabs.Children.RemoveAt(i);
            }

            if (stackTabs.Children.Count == 0)
                addNewTab(substituteForEmpty);

            roundTabIndex();
        }


        public void ChangeCurrentTabName(string name)
        {
            if (_currentIndex >= stackTabs.Children.Count) return;
            var tab = stackTabs.Children[_currentIndex] as TabSwitchElement;
            Debug.Assert(tab != null);
            tab.TitleName = name;
        }

        private TabSwitchElement getCurrentTab()
        {
            return getTabByIndex(_currentIndex);
        }
        private TabSwitchElement getTabByIndex(int index)
        {
            var tab = stackTabs.Children[index] as TabSwitchElement;
            Debug.Assert(tab != null);
            return tab;
        }


        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            addNewTab(getCurrentTab().TitleName);
            changeTabIndex(stackTabs.Children.Count - 1);
        }

        private void changeTabIndex(int index)
        {
            _currentIndex = index;
            for (int i=0; i<stackTabs.Children.Count; ++i)
            {
                getTabByIndex(i).IsActive = i == _currentIndex;
            }
            OnChangeTab(getTabByIndex(index).TitleName);
        }
    }
}
