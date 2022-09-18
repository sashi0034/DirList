using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace DirList.Util
{
    public class MarginSetter
    {
        public static Thickness GetMargin(DependencyObject obj) => obj != null ? (Thickness)obj.GetValue(PropertyMargin) : new Thickness();

        public static void SetMargin(DependencyObject obj, Thickness value) => obj?.SetValue(PropertyMargin, value);

        public static readonly DependencyProperty PropertyMargin
            = DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(MarginSetter), new UIPropertyMetadata(new Thickness(), Change));


        public static void Change(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Panel panel)) return;
            panel.Loaded += new RoutedEventHandler(SetMarginControlsChildren);
        }


        public static void SetMarginControlsChildren(object sender, RoutedEventArgs e)
        {

            if (!(sender is Panel panel)) return;
            foreach (var child in panel.Children)
            {
                if (!(child is FrameworkElement elementChild)) continue;
                elementChild.Margin = GetMargin(panel);
            }

        }
    }
}
