using System.Windows;

namespace Ciezarki.Core
{
    public static class ComboBoxHelper
    {
        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.RegisterAttached(
                "HorizontalOffset",
                typeof(double),
                typeof(ComboBoxHelper),
                new FrameworkPropertyMetadata(0.0));

        public static void SetHorizontalOffset(UIElement element, double value)
        {
            element.SetValue(HorizontalOffsetProperty, value);
        }

        public static double GetHorizontalOffset(UIElement element)
        {
            return (double)element.GetValue(HorizontalOffsetProperty);
        }
    }
}
