using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Hearthstone_Deck_Tracker;

namespace PackTracker.View {
  abstract class AbstractValueConverter : IValueConverter {
    public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
    public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

    protected CultureInfo GetCultureInfo() {
      CultureInfo cult = null as CultureInfo;
      try {
        string lang = Config.Instance.Localization.ToString().Insert(2, "-");

        return new CultureInfo(lang);
      }
      catch(CultureNotFoundException) {
        return CultureInfo.InstalledUICulture;
      }
    }
  }
}
