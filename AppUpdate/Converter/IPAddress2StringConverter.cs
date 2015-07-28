using System;
using System.Globalization;
using System.Net;
using System.Windows.Data;

namespace AppUpdate.Converter
{
    // ReSharper disable once InconsistentNaming
    public sealed class IPAddress2StringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ipAddress = value as IPAddress;
            return ipAddress?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ipAddressStr = value as string;
            IPAddress ipAddress;
            if (ipAddressStr != null && IPAddress.TryParse(ipAddressStr, out ipAddress))
            {
                return ipAddress;
            }
            return IPAddress.Loopback;
        }
    }
}
