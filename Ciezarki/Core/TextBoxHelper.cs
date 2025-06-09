using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Ciezarki.Core
{
    public static class TextBoxHelper
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.RegisterAttached(
                "Title",
                typeof(string),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(string.Empty));

        public static void SetTitle(UIElement element, string value)
        {
            element.SetValue(TitleProperty, value);
        }

        public static string GetTitle(UIElement element)
        {
            return (string)element.GetValue(TitleProperty);
        }

        public static readonly DependencyProperty BorderWidthProperty =
            DependencyProperty.RegisterAttached(
                "BorderWidth",
                typeof(double),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(330.0));

        public static void SetBorderWidth(UIElement element, double value)
        {
            element.SetValue(BorderWidthProperty, value);
        }

        public static double GetBorderWidth(UIElement element)
        {
            return (double)element.GetValue(BorderWidthProperty);
        }

        public static readonly DependencyProperty BorderHeightProperty =
    DependencyProperty.RegisterAttached(
        "BorderHeight",
        typeof(double),
        typeof(TextBoxHelper),
        new FrameworkPropertyMetadata(100.0));

        public static void SetBorderHeight(UIElement element, double value)
        {
            element.SetValue(BorderHeightProperty, value);
        }

        public static double GetBorderHeight(UIElement element)
        {
            return (double)element.GetValue(BorderHeightProperty);
        }
    }
}
