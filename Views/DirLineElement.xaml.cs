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
using DirList.Configs;
using DirList.Util;
using static System.Net.Mime.MediaTypeNames;

namespace DirList.Views
{
    /// <summary>
    /// DirLineElement.xaml の相互作用ロジック
    /// </summary>
    public partial class DirLineElement : UserControl
    {
        private readonly ConfigRecord _configRecord;
        public DirLineElement(ConfigRecord config)
        {
            _configRecord = config;

            InitializeComponent();

            Dir = new DirPath("C:\\");
        }

        public DirPath Dir
        {
            get
            {
                return new DirPath(buttonDir.Content as string);
            }
            set
            {
                buttonDir.Content = value.Path;
            }
        }

        public void AddEventOnPushDelete(RoutedEventHandler e)
        {
            buttonDelete.Click += e;
        }


        private void buttonDir_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var configInfo = _configRecord.ProgramForOpen.GetInfoToExecute();

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                UseShellExecute = configInfo.UseShellExecute,
                FileName = configInfo.FileName,
                Arguments = Dir.Path,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            try
            {
                Process.Start(processStartInfo);
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void buttonCopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_configRecord.OptionOnCopy.GetFormatedPath(Dir));
        }

    }
}
