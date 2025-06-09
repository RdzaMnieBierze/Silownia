using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ciezarki.Core
{
    public static class ButtonHelper
    {
        public static readonly DependencyProperty ButtonWidthProperty =
            DependencyProperty.RegisterAttached(
                "ButtonWidth",
                typeof(double),
                typeof(ButtonHelper),
                new FrameworkPropertyMetadata(150.0));

        public static void SetButtonWidth(UIElement element, double value)
        {
            element.SetValue(ButtonWidthProperty, value);
        }

        public static double GetButtonWidth(UIElement element)
        {
            return (double)element.GetValue(ButtonWidthProperty);
        }
    }
}
