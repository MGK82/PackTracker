using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Hearthstone_Deck_Tracker;

namespace PackTracker.View {
  class PercentConverter : AbstractValueConverter {
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      => double.TryParse(value.ToString(), out double percent) ? percent.ToString("p1", GetCultureInfo()) : value;

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }
  }
}
