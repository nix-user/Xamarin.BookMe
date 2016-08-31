using BookMeMobile.Render;
using BookMeMobile.WinPhone.Render;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;

[assembly: ExportRenderer(typeof(TimePicker24Hour), typeof(TimePicker24HRenderer))]

namespace BookMeMobile.WinPhone.Render
{
    public class TimePicker24HRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                var nativeControl = (Windows.UI.Xaml.Controls.TimePicker)Control;
                nativeControl.Foreground = new SolidColorBrush(Colors.Gray);
                Control.ClockIdentifier = "24HourClock";
            }
        }
    }
}