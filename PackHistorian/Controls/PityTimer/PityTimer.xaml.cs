using System;
using System.Collections.Generic;
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
using HearthDb.Enums;
using MahApps.Metro.Controls;
using PackTracker.View.Cache;

namespace PackTracker.Controls.PityTimer {
  /// <summary>
  /// Interaktionslogik für PityTimer.xaml
  /// </summary>
  public partial class PityTimer : MetroWindow {
    PityTimerRepository _pityTimers;

    public PityTimer(PackTracker.History History, PityTimerRepository PityTimers) {
      InitializeComponent();

      _pityTimers = PityTimers;

      dd_Packs.SelectionChanged += (sender, e) => {
        if(e.AddedItems.Count == 1) {
          Ep_Prev.DataContext = Ep_Label.DataContext = _pityTimers.GetPityTimer((int)e.AddedItems[0], Rarity.EPIC, false, true);
          Leg_Prev.DataContext = Leg_Label.DataContext = _pityTimers.GetPityTimer((int)e.AddedItems[0], Rarity.LEGENDARY, false, true);
        }
      };

      Loaded += (sender, e) => dd_Packs.DataContext = History;
    }
  }
}
