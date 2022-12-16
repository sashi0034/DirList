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
using System.Windows.Shapes;

namespace DirList.Views
{
    /// <summary>
    /// InputDataInstanceWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class DataInstanceInputWindow : Window
    {
        private Func<string, bool> _isValidInput;
        public DataInstanceInputWindow(Func<string, bool> isValidInput)
        {
            InitializeComponent();
            _isValidInput = isValidInput;
            inputBox.Focus();

            this.Dispatcher.InvokeAsync(async () => 
                { 
                    await Task.Delay(0);
                    inputBox.SelectAll(); 
                });
        }

        public bool IsConfirmed { get; private set; }

        public string InputText
        {
            get { return inputBox.Text; }
            set { inputBox.Text = value; }
        }

        public void ShowBeforeName(string beforeName)
        {
            labelBeforeName.Text = "以前の名前: " + beforeName;
            labelBeforeName.Visibility = Visibility.Visible;
        }

        private void buttonConfirm_Click(object sender, RoutedEventArgs e)
        {
            confirmAndExit();
        }

        private void confirmAndExit()
        {
            IsConfirmed = true;
            Close();
        }

        private void inputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isValidInput(InputText))
            {
                buttonConfirm.IsEnabled = true;
                buttonConfirm.Opacity = 1.0;
                infoLabel.HideMeddage();
            }
            else
            {
                buttonConfirm.IsEnabled = false;
                buttonConfirm.Opacity = 0.5;
                infoLabel.ShowMessage(new InfoLabelMessage("他の名前にしてください。", Brushes.LightPink));
            }
        }

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                confirmAndExit();
            }
            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
