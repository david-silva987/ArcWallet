﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ArcWallet
{
    class ImagePostfixValueConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;
            if (path=="Santé")
            {
                path = "Sante";
            }

            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
