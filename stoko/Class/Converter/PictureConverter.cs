﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace stoko {
    class AvatarConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            String avatarUrl;

            if (value == null || (String)value == String.Empty) {
                avatarUrl = Configs.Data.Global["imgSrvUrl"] + "defaultavatarurl.png";
            } else {
                avatarUrl = Configs.Data.Global["imgSrvUrl"] + "clients/" + (String)value;
            }
            return avatarUrl;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
    class ProductPictureConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            String avatarUrl;

            if (value == null) {
                avatarUrl = Configs.Data.Global["imgSrvUrl"] + "defaultproduitimg.png";
            } else {
                avatarUrl = Configs.Data.Global["imgSrvUrl"] + "products/" + (String)value;
            }
            return avatarUrl;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
