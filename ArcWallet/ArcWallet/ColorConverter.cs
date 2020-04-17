using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ArcWallet
{
    public class ColorConverter : IValueConverter
    {
        public bool False { get; private set; }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var car = value as Transaction;

            if (car.Type == False)
                return Color.LightCoral;

            return Color.LightGreen;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}