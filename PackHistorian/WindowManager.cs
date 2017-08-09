using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PackTracker.Controls;
using PackTracker.Storage;
using PackTracker.View.Cache;

namespace PackTracker {

  class WindowManager {
    string _name;
    Window _pityWin, _statisticWin, _historyWin, _logWin, _searchWin, _pityOverlay;

    public WindowManager(string name) {
      _name = name;
    }

    public void ShowPityWin(History History, PityTimerRepository PityTimers) {
      if(_pityWin == null) {
        _pityWin = new Controls.PityTimer.PityTimer(History, PityTimers) {
          Owner = Hearthstone_Deck_Tracker.Core.MainWindow,
        };
        _pityWin.Closed += (sender, e) => _pityWin = null;
        _pityWin.Loaded += (sender, e) => _pityWin.Title = _name + ": " + _pityWin.Title;

        _pityWin.Show();
      }

      _pityWin.Focus();
    }

    public void ShowStatisticWin(History History) {
      if(_statisticWin == null) {
        _statisticWin = new Statistic(History) {
          Owner = Hearthstone_Deck_Tracker.Core.MainWindow,
        };
        _statisticWin.Closed += (sender, e) => _statisticWin = null;
        _statisticWin.Loaded += (sender, e) => _statisticWin.Title = _name + ": " + _statisticWin.Title;

        _statisticWin.Show();
      }

      _statisticWin.Focus();
    }

    public void ShowHistoryWin(History History) {
      if(_historyWin == null) {
        _historyWin = new Controls.History(History, new HistoryDatePicker(History)) {
          Owner = Hearthstone_Deck_Tracker.Core.MainWindow,
        };
        _historyWin.Closed += (sender, e) => { _historyWin = null; };
        _historyWin.Loaded += (sender, e) => _historyWin.Title = _name + ": " + _historyWin.Title;

      _historyWin.Show();
      }

      _historyWin.Focus(); ;
    }

    public void ShowLogWin(History History) {
      if(_logWin == null) {
        _logWin = new Log(History) {
          Owner = Hearthstone_Deck_Tracker.Core.MainWindow,
        };
        _logWin.Closed += (sender, e) => _logWin = null;
        _logWin.Loaded += (sender, e) => _logWin.Title = _name + ": " + _logWin.Title;

        _logWin.Show();
      }

      _logWin.Focus();
    }

    public void ShowSearchWin(History History) {
      if(_searchWin == null) {
        _searchWin = new Search(History) {
          Owner = Hearthstone_Deck_Tracker.Core.MainWindow,
        };
        _searchWin.Closed += (sender, e) => _searchWin = null;
        _searchWin.Loaded += (sender, e) => _searchWin.Title = _name + ": " + _searchWin.Title;

        _searchWin.Show();
      }

      _searchWin.Focus();
    }

    public void ShowSettingsWin(Settings Settings, ISettingsStorage SettingsStorage, Type PreSelection = null) {
      Controls.Settings.Settings Win = new Controls.Settings.Settings(Settings) {
        Owner = Hearthstone_Deck_Tracker.Core.MainWindow
      };
      Win.Closed += (sender, e) => SettingsStorage.Store(Settings);
      Win.Title = _name + ": " + Win.Title;

      if(PreSelection != null) {
        foreach(var Item in Win.lb_tabs.Items) {
          if(Item.GetType() == PreSelection) {
            Win.lb_tabs.SelectedItem = Item;
            break;
          }
        }
      }

      Win.ShowDialog();
    }

    public void ShowPityTimerOverlay(History History, PityTimerRepository PityTimers) {
      if(_pityOverlay == null) {
        _pityOverlay = new Controls.PityTimer.PityTimerOverlay(History, PityTimers);
        Hearthstone_Deck_Tracker.Core.MainWindow.Closed += ClosePityTimerOverlay;
        _pityOverlay.Closed += (sender, e) => _pityOverlay = null;
      }

      _pityOverlay.Show();
    }

    private void ClosePityTimerOverlay(object sender, EventArgs e) {
      ClosePityTimerOverlay();
    }

    public void ClosePityTimerOverlay() {
      if(_pityOverlay != null) {
          _pityOverlay.Dispatcher.Invoke(() => {
            _pityOverlay.Close();
          });

          _pityOverlay = null;
          Hearthstone_Deck_Tracker.Core.MainWindow.Closed -= ClosePityTimerOverlay;
      }
    }
  }
}
