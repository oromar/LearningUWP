using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace Exercise5.Controls
{
    public sealed class IconButton : Button
    {
        public IconButton()
        {
            this.DefaultStyleKey = typeof(IconButton);
        }

        public static IconElement GetIcon(DependencyObject obj)
        {
            return (IconElement)obj.GetValue(IconProperty);
        }

        public static void SetIcon(DependencyObject obj, IconElement value)
        {
            obj.SetValue(IconProperty, value);
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached("Icon", typeof(IconElement), typeof(IconButton), new PropertyMetadata(null));
    }
}
