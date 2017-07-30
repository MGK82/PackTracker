using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.Specialized;
using PackTracker.Entity;

namespace PackTracker.Controls {
  /// <summary>
  /// Interaktionslogik für Statistic.xaml
  /// </summary>
  public partial class Statistic {
    public Statistic(PackTracker.History History) {
      InitializeComponent();

      Dictionary<int, View.Statistic> _statistics = new Dictionary<int, View.Statistic>();

      dd_Packs.SelectionChanged += (sender, e) => {
        if(e.AddedItems.Count == 1) {
          int selection = (int)e.AddedItems[0];
          if(!_statistics.ContainsKey(selection)) {
            _statistics.Add(selection, new View.Statistic(selection, History));
          }

          dp_Statistic.DataContext = _statistics[selection];
        } else {
          dp_Statistic.DataContext = null;
        }
      };

      Loaded += (sender, e) => dd_Packs.DataContext = History;
      dd_Packs.Focus();
    }
  }
}
