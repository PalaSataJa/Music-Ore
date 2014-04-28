using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MusicOre.Views
{
	public static class DialogCloser
	{
		public static readonly DependencyProperty DialogResultProperty =
				DependencyProperty.RegisterAttached(
						"DialogResult",
						typeof(bool?),
						typeof(DialogCloser),
						new PropertyMetadata(DialogResultChanged));

		private static void DialogResultChanged(
				DependencyObject d,
				DependencyPropertyChangedEventArgs e)
		{
			var window = d as Window;
			if (window != null)
				window.DialogResult = e.NewValue as bool?;
		}
		public static void SetDialogResult(Window target, bool? value)
		{
			target.SetValue(DialogResultProperty, value);
		}
	}
    public class TimeSpanToStringConverter:IValueConverter{
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan)
            {
                return ((TimeSpan) value).ToString();
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}