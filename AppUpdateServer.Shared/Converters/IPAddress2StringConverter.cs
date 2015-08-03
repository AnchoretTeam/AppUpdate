using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AppUpdateServer.Converters
{
    public sealed class IPAddress2StringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ip = value as IPAddress;
            return ip?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ipStr = value as string;
            if (ipStr!=null)
            {
                IPAddress ip;
                if (IPAddress.TryParse(ipStr, out ip))
                {
                    return ip;
                }
            }
            return IPAddress.None;
        }
    }
}
