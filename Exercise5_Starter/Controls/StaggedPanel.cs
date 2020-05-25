using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.AllJoyn;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Exercise5.Controls
{
    public class StaggedPanel : Panel
    {
        public int StaggedHeight
        {
            get { return (int)GetValue(StaggedHeightProperty); }
            set { SetValue(StaggedHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StaggedHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StaggedHeightProperty =
            DependencyProperty.Register("StaggedHeight", typeof(int), typeof(StaggedPanel), new PropertyMetadata(60d, (obj, args) =>
            {
                if (obj is StaggedPanel panel)
                {
                    panel.UpdateLayout();
                }
            }));

        public static bool GetExpanded(DependencyObject obj)
        {
            return (bool)obj.GetValue(ExpandedProperty);
        }

        public static void SetExpanded(DependencyObject obj, bool value)
        {
            obj.SetValue(ExpandedProperty, value);
        }

        // Using a DependencyProperty as the backing store for Expanded.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpandedProperty =
            DependencyProperty.RegisterAttached("Expanded", typeof(bool), typeof(StaggedPanel), new PropertyMetadata(false));

        protected override Size MeasureOverride(Size availableSize)
        {
            var desiredSize = new Size(availableSize.Width, 0);

            var staggedAvailableSize = new Size(availableSize.Width, StaggedHeight);
            var expandedAvailableSize = new Size(availableSize.Width, double.PositiveInfinity);

            foreach (var item in Children)
            {
                if (GetExpanded(item))
                {
                    item.Measure(expandedAvailableSize);
                    desiredSize.Height += item.DesiredSize.Height;
                }
                else
                {
                    item.Measure(staggedAvailableSize);
                    desiredSize.Height += staggedAvailableSize.Height;
                }
            }

            return desiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var panelSize = new Size(finalSize.Width, 0);

            foreach (var item in Children)
            {
                if (GetExpanded(item))
                {
                    item.Arrange(new Rect(0, panelSize.Height, item.DesiredSize.Width, item.DesiredSize.Height));
                    panelSize.Height += item.DesiredSize.Height;
                }
                else
                {
                    item.Arrange(new Rect(0, panelSize.Height, item.DesiredSize.Width, StaggedHeight));
                    panelSize.Height += StaggedHeight;
                }
            }

            return panelSize;
        }
    }
}
