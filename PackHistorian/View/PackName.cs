using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using HearthDb.Enums;
using Hearthstone_Deck_Tracker;

namespace PackChronicler.View {
  class PackName : IValueConverter {
    static Config _config = Config.Instance;
    static Dictionary<int, Dictionary<Locale, string>> PackNames = new Dictionary<int, Dictionary<Locale, string>>() {
      {
        1, new Dictionary<Locale, string>() {
          { Locale.enUS, "Classic" },
          { Locale.enGB, "Classic" },
          { Locale.deDE, "Klassik" },
        }
      },
      {
        19, new Dictionary<Locale, string>() {
          { Locale.enUS, "Mean Streets of Gadgetzan" },
          { Locale.enGB, "Mean Streets of Gadgetzan" },
          { Locale.deDE, "Die Straßen von Gadgetzan" },
        }
      }
    };

    public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture) {
      if(int.TryParse(value.ToString(), out int id)) {
        if(PackNames.ContainsKey(id)) {
          if(Enum.TryParse(_config.SelectedLanguage, out Locale lang)) {
            if(PackNames[id].ContainsKey(lang)) {
              return PackNames[id][lang];
            } else {
              if(PackNames[id].ContainsKey(Locale.enUS)) {
                return PackNames[id][Locale.enUS];
              }
            }
          }
        }
      }

      return value;
    }

    public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }
  }
}
