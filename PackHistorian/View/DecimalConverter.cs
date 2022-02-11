using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Hearthstone_Deck_Tracker;

namespace PackTracker.View {
  class DecimalConverter : AbstractValueConverter {
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => int.TryParse(value.ToString(), out int dec) ? dec.ToString("n0", GetCultureInfo()) : value;
    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }
  }
}
