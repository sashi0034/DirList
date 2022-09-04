using System;
using System.Collections.Generic;
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
using DirList.Util;

namespace DirList.Views
{
    /// <summary>
    /// DirPathInput.xaml の相互作用ロジック
    /// </summary>
    public partial class DirPathInput : UserControl
    {
        private readonly Key _keyOfConfrim = Key.Enter;
        private readonly string _keyNameOfConfirm = "Enter";

        public DirPathInput()
        {
            InitializeComponent();
        }

        public Action<DirPath> OnConfirm { get; set; } 

        private void inputDirPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (info == null) return;

            inputDirPath.Text = inputDirPath.Text.Replace("\"", "");

            var message = isValidInput() ?
                new InfoLabelMessage($"{_keyNameOfConfirm}で追加します。", Brushes.White) :
                new InfoLabelMessage("存在しないパスです。", Brushes.LightPink);

            info.ShowMessage(message);
        }

        private bool isValidInput()
        {
            return System.IO.Directory.Exists(inputDirPath.Text);
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            checkConfirmInput(e);
        }

        private void checkConfirmInput(KeyEventArgs e)
        {
            if (e.Key != _keyOfConfrim) return;
            if (!isValidInput()) return;

            OnConfirm(DirPath.PushNew(inputDirPath.Text));
            inputDirPath.Text = "";

            const int visibleDuration = 1000 * 2;
            info.ShowMessage(new InfoLabelMessage("リストに追加しました。", Brushes.Yellow, visibleDuration));


        }
    }
}
