using stoko_db_BLL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace stoko {
    class EnumConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            String str;

            switch ((int)value) {
                case 0:
                    str = Application.Current.Resources["sNo"] as String;
                    break;
                case 1:
                    str = Application.Current.Resources["sYes"] as String;
                    break;
                default:
                    str = Application.Current.Resources["cNone"] as String;
                    break;
            }
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
