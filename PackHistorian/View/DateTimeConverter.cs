using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Hearthstone_Deck_Tracker;

namespace PackTracker.View {
  class DateTimeConverter : AbstractValueConverter {
    static Config _config = Config.Instance;
    protected virtual string _format { get => "G"; }

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is DateTime ? ((DateTime)value).ToString(_format, GetCultureInfo()) : value;
    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }
  }
}
