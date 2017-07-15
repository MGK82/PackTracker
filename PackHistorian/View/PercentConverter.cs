using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Hearthstone_Deck_Tracker;

namespace PackTracker.View {
  class PercentConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
      if(double.TryParse(value.ToString(), out double percent)) {
        CultureInfo cult = null as CultureInfo;
        try {
          string lang = Config.Instance.SelectedLanguage.Insert(2, "-");
          cult = new CultureInfo(lang);
        }
        catch(CultureNotFoundException) {
          cult = CultureInfo.InstalledUICulture;
        }

        return percent.ToString("p1", cult);
      }

      return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }
  }
}
