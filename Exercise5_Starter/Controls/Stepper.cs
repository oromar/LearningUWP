using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235



namespace Exercise5.Controls
{
    [TemplatePart(Name = "PlusButton", Type = typeof(Button))]
    [TemplatePart(Name = "MinusButton", Type = typeof(Button))]
    public sealed class Stepper : Control
    {

        public int Value
        {
            get
            {

                return (int)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        public int Step
        {
            get
            {

                return (int)GetValue(StepProperty);
            }
            set
            {
                SetValue(StepProperty, value);
            }
        }

        public Stepper()
        {
            this.DefaultStyleKey = typeof(Stepper);
        }

        public static int GetValue(DependencyObject obj)
        {
            return (int)obj.GetValue(ValueProperty);
        }

        public static void SetValue(DependencyObject obj, int value)
        {
            obj.SetValue(ValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached("Value", typeof(int), typeof(Stepper), new PropertyMetadata(0));

        public static int GetStep(DependencyObject obj)
        {
            return (int)obj.GetValue(StepProperty);
        }

        public static void SetStep(DependencyObject obj, int value)
        {
            obj.SetValue(StepProperty, value);
        }

        // Using a DependencyProperty as the backing store for Step.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.RegisterAttached("Step", typeof(int), typeof(Stepper), new PropertyMetadata(1));


        protected override void OnApplyTemplate()
        {
            if (GetTemplateChild("PlusButton") is Button plusButton)
            {
                plusButton.Click += Plus;
            }
            if (GetTemplateChild("MinusButton") is Button minusButton)
            {
                minusButton.Click += Minus;
            }
        }

        public void Plus(object sender, RoutedEventArgs e)
        {
            Value += Step;
        }

        public void Minus(object sender, RoutedEventArgs e)
        {
            Value -= Step;
        }
    }
}
