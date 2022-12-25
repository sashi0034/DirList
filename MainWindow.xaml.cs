using DirList.Configs;
using DirList.Views;
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

namespace DirList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ConfigRecord _configRecord;
        private readonly UserDataLinker _userDataLinker;
        private bool _isClosed = false;
        private const int backgroundSaveDuration = 30 * 60 * 1000; // 30 minutes

        public MainWindow()
        {
            InitializeComponent();

            _configRecord = new ConfigRecord(
                new OptionOnCopy(viewOptionOnCopy),
                new ProgramForOpen(viewProgramForOpen),
                new DirListSort(viewDirlistSort, dirListPanel),
                new DirListDataInstanceConfig(dataInstanceView, dirListPanel, tabSwitchPanel)
                );

            dirListPanel.RegisterConfig(_configRecord);

            _userDataLinker = UserDataLinker.LoadUserData(new MainWindowInfo(
                _configRecord, 
                dirListPanel,
                tabSwitchPanel));

            this.Dispatcher.Invoke(saveDataAsBackgroundAsync);
        }

        private async Task saveDataAsBackgroundAsync()
        {
            while (_isClosed == false)
            {
                textSaveDataAsBackground.Visibility = Visibility.Collapsed;

                await Task.Delay(backgroundSaveDuration);

                _userDataLinker.SaveUserData();
                textSaveDataAsBackground.Visibility = Visibility.Visible;

                await Task.Delay(1000 * 3);
            }
        }

        private void dirPathInput_onConfirm(DirPath path)
        {
            dirListPanel.AddDir(path);
        }

        private void window_Closed(object sender, EventArgs e)
        {
            _userDataLinker.SaveUserData();
            _isClosed = true;
        }
    }
}
