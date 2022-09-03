using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    public record InfoLabelMessage(string Message, Brush Brush)
    {
        public readonly int VisibleDuration = -1;

        public InfoLabelMessage(string message, Brush brush, int visibleDuration) : this(message, brush)
        {
            VisibleDuration = visibleDuration;
        }

        public bool CanInvisible => VisibleDuration > 0;
    }

    /// <summary>
    /// InfoLabel.xaml の相互作用ロジック
    /// </summary>
    public partial class InfoLabel : UserControl
    {
        public InfoLabel()
        {
            InitializeComponent();
        }

        private CancellationTokenSource? _cancellationOldTaskRef = null;

        public void ShowMessage(InfoLabelMessage message)
        {
            _cancellationOldTaskRef?.Cancel();
            label.Visibility = Visibility.Visible;

            label.Content = message.Message;
            label.Foreground = message.Brush;

            if (message.CanInvisible) makeInvisibleAsync(message.VisibleDuration);
        }

        private async void makeInvisibleAsync(int time)
        {
            var cancel = new CancellationTokenSource();
            _cancellationOldTaskRef = cancel;

            await Task.Delay(time);
            if (cancel.IsCancellationRequested) return;
            
            label.Visibility = Visibility.Hidden;
        }
    }
}
