
using Hearthstone_Deck_Tracker.Plugins;
using System;
using System.Reflection;
using System.Windows.Controls;
using PackTracker.Storage;
using Hearthstone_Deck_Tracker.Utility.Toasts;
using System.ComponentModel;
using PackTracker.Update;
using System.Windows;

namespace PackTracker {
  public class Plugin : IPlugin {
    readonly static Version _version = new Version("1.1");
    private AchievementsWatcher _watcher;
    Updater _updater;
    History _history;
    IHistoryStorage _historyStorage = new XmlHistory();
    Settings _settings;
    ISettingsStorage _settingsStorage = new XmlSettings();
    Controls.History _historyWin;
    Controls.Statistic _statisticWin;
    Controls.Log _logWin;
    Controls.Search _searchWin;
    Controls.PityTimer.PityTimer _pityWin;
    View.AverageCollection _averageCollection;
    View.Cache.PityTimerRepository _pityTimers;

    public static Version CurrentVersion { get => _version; }

    Controls.History HistoryWin {
      get {
        if(_historyWin == null) {
          _historyWin = new Controls.History(_history, _averageCollection, new Controls.HistoryDatePicker(_history)) {
            Owner = Hearthstone_Deck_Tracker.Core.MainWindow,
          };
          _historyWin.Closed += (sender, e) => { _historyWin = null; };
          _historyWin.Loaded += (sender, e) => _historyWin.Title = Name + ": " + _historyWin.Title;
        }

        return _historyWin;
      }
    }

    Controls.Statistic StatisticWin {
      get {
        if(_statisticWin == null) {
          _statisticWin = new Controls.Statistic(_history) {
            Owner = Hearthstone_Deck_Tracker.Core.MainWindow,
          };
          _statisticWin.Closed += (sender, e) => _statisticWin = null;
          _statisticWin.Loaded += (sender, e) => _statisticWin.Title = Name + ": " + _statisticWin.Title;
        }

        return _statisticWin;
      }
    }

    Controls.Log LogWin {
      get {
        if(_logWin == null) {
          _logWin = new Controls.Log(_history) {
            Owner = Hearthstone_Deck_Tracker.Core.MainWindow,
          };
          _logWin.Closed += (sender, e) => _logWin = null;
          _logWin.Loaded += (sender, e) => _logWin.Title = Name + ": " + _logWin.Title;
        }

        return _logWin;
      }
    }

    Controls.Settings.Settings CreateSettingsWin() {
      Controls.Settings.Settings Win = new Controls.Settings.Settings(_settings) {
        Owner = Hearthstone_Deck_Tracker.Core.MainWindow
      };
      Win.Closed += (sender, e) => _settingsStorage.Store(_settings);
      Win.Title = Name + ": " + Win.Title;

      return Win;
    }

    Controls.Search SearchWin {
      get {
        if(_searchWin == null) {
          _searchWin = new Controls.Search(_history) {
            Owner = Hearthstone_Deck_Tracker.Core.MainWindow,
          };
          _searchWin.Closed += (sender, e) => _searchWin = null;
          _searchWin.Loaded += (sender, e) => _searchWin.Title = Name + ": " + _searchWin.Title;
        }

        return _searchWin;
      }
    }

    Controls.PityTimer.PityTimer PityWin {
      get {
        if(_pityWin == null) {
          _pityWin = new Controls.PityTimer.PityTimer(_history, _pityTimers) {
            Owner = Hearthstone_Deck_Tracker.Core.MainWindow,
          };
          _pityWin.Closed += (sender, e) => _pityWin = null;
          _pityWin.Loaded += (sender, e) => _pityWin.Title = Name + ": " + _pityWin.Title;
        }

        return _pityWin;
      }
    }

    public Plugin() {
      _watcher = new AchievementsWatcher();
      _updater = new Updater();

      try {
        _history = _historyStorage.Fetch();
        _averageCollection = new View.AverageCollection(_history);
      } catch {
        _history = new History();
      }

      try {
        _settings = _settingsStorage.Fetch();
      } catch {
        _settings = new Settings();
      }

      _pityTimers = new View.Cache.PityTimerRepository(_history);
    }

    public string Author
    {
      get
      {
        return "DBqFetti <dbqfetti@gmail.com>";
      }
    }

    public string ButtonText
    {
      get
      {
        return "Settings";
      }
    }

    public string Description
    {
      get
      {
        return Name + " is a plugin that allows you to keep an eye on every pack you open. This allows you to see how many cards of different rarities have dropped over time and also enables you to estimate when your next Epic or Legendary is coming!";
      }
    }

    public MenuItem MenuItem {
      get {
        Controls.Menu Menu = new Controls.Menu();
        Menu.mnu_History.Click += (sender, e) => { HistoryWin.Show(); HistoryWin.Focus(); };
        Menu.mnu_Statistic.Click += (sender, e) => { StatisticWin.Show(); StatisticWin.Focus(); };
        Menu.mnu_Log.Click += (sender, e) => { LogWin.Show(); LogWin.Focus(); };
        Menu.mnu_Search.Click += (sender, e) => { SearchWin.Show(); SearchWin.Focus(); };
        Menu.mnu_PityTimers.Click += (sender, e) => { PityWin.Show(); PityWin.Focus(); };

        return Menu;
      }
    }

    public string Name
    {
      get
      {
        return "Pack Tracker";
      }
    }

    public Version Version
    {
      get
      {
        return CurrentVersion;
      }
    }

    public void OnButtonPress() {
      CreateSettingsWin().ShowDialog();
    }

    public void OnLoad() {
      _watcher.Start();

      _watcher.PackOpened += (sender, e) => {
        _history.Add(e.Pack);
        _historyStorage.Store(_history.Ascending);

        if(_settings.Spoil) {
          View.Average Average = _averageCollection.FindForPackId(e.Pack.Id);
          ToastManager.ShowCustomToast(new Controls.Toast(e.Pack, Average));
        }
      };

      if(_settings.Update) {
        BackgroundWorker bwCheck = new BackgroundWorker();
        bwCheck.DoWork += (sender, e) => {
          e.Result = _updater.NewVersionAvailable();
        };
        bwCheck.RunWorkerCompleted += (sender, e) => {
          if((bool?)e.Result == true) {
            Controls.Settings.Settings Settings = CreateSettingsWin();
            foreach(var Item in Settings.lb_tabs.Items) {
              if(Item is Controls.Settings.Update) {
                Settings.lb_tabs.SelectedItem = Item;
                Settings.ShowDialog();
                break;
              }
            }
          }
        };
        bwCheck.RunWorkerAsync();
      }
    }

    public void OnUnload() {
    }

    public void OnUpdate() {
    }
  }
}
