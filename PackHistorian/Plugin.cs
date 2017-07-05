
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
    readonly static Version _version = new Version("0.3");
    private AchievementsWatcher _watcher;
    Updater _updater;
    History _history;
    IStorage _storage = new XmlStorage();
    Controls.History _historyWin;
    Controls.Statistic _statisticWin;
    Controls.Log _logWin;
    View.AverageCollection _averageCollection;

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

    public Plugin() {
      _watcher = new AchievementsWatcher();
      _updater = new Updater();

      try {
        _history = _storage.Fetch();
        _averageCollection = new View.AverageCollection(_history);
      } catch {
        _history = new History();
      }
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
        return "Description goes here";
      }
    }

    public MenuItem MenuItem {
      get {
        Controls.Menu Menu = new Controls.Menu();
        Menu.mnu_History.Click += (sender, e) => { HistoryWin.Show(); HistoryWin.Focus(); };
        Menu.mnu_Statistic.Click += (sender, e) => { StatisticWin.Show(); StatisticWin.Focus(); };
        Menu.mnu_Log.Click += (sender, e) => { LogWin.Show(); LogWin.Focus(); };

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
      (new Controls.Settings.Settings() { Owner = Hearthstone_Deck_Tracker.Core.MainWindow }).ShowDialog();
    }

    public void OnLoad() {
      _watcher.Start();

      _watcher.PackOpened += (sender, e) => {
        _history.Add(e.Pack);
        _storage.Store(_history.Ascending);

        View.Average Average = _averageCollection.FindForPackId(e.Pack.Id);
        ToastManager.ShowCustomToast(new Controls.Toast(e.Pack, Average));
      };

      BackgroundWorker bwCheck = new BackgroundWorker();
      bwCheck.DoWork += (sender, e) => {
        e.Result = _updater.NewVersionAvailable();
      };
      bwCheck.RunWorkerCompleted += (sender, e) => {
        if((bool?)e.Result == true) {
          Controls.Settings.Settings Settings = new Controls.Settings.Settings() { Owner = Hearthstone_Deck_Tracker.Core.MainWindow };
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

    public void OnUnload() {
    }

    public void OnUpdate() {
    }
  }
}
