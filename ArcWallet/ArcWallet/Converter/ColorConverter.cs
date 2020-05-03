using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ArcWallet
{
    /// <summary>
    /// Choose the correct color for the type of transaction (Revenue -> greeen, Spent -> red)
    /// </summary>
    public class ColorConverter : IValueConverter
    {
        public bool False { get; private set; }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var transaction = value as Transaction;

            if (transaction.Type == False)
                return Color.FromRgb(250,209,208);

            return Color.FromRgb(0, 250, 154);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}