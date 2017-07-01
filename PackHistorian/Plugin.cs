
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
    private AchievementsWatcher _watcher;
    Update.Updater _updater;
    History _history;
    IStorage _storage = new XmlStorage();
    MenuItem _menu;
    Controls.History _historyWin;
    Controls.Statistic _statisticWin;
    Controls.Log _logWin;
    View.AverageCollection _averageCollection;

    Controls.History HistoryWin {
      get {
        if(_historyWin == null) {
          _historyWin = new Controls.History(_history, _averageCollection, new Controls.HistoryDatePicker(_history)) {
            Owner = Hearthstone_Deck_Tracker.Core.MainWindow,
          };
          _historyWin.Closed += (sender, e) => { _historyWin = null; };
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
        }

        return _logWin;
      }
    }

    public Plugin() {
      _watcher = new AchievementsWatcher();
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
        return "Pack Tracker";
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
        Menu.mnu_History.Click += (sender, e) => HistoryWin.Show();
        Menu.mnu_Statistic.Click += (sender, e) => StatisticWin.Show();
        Menu.mnu_Log.Click += (sender, e) => LogWin.Show();

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
        return _updater.CurrentVersion;
      }
    }

    public void OnButtonPress() {
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
      Release LatestRelease = null as Release;
      bwCheck.DoWork += (sender, e) => {
        e.Result = _updater.NewVersionAvailable(out LatestRelease);
      };
      bwCheck.RunWorkerCompleted += (sender, e) => {
        if((bool?)e.Result == true) {
          MessageBoxResult Result = MessageBox.Show("New version available.\n\n" + LatestRelease.tag_name + "\n" + LatestRelease.body + "\n\nUpdate now?", "Pack Tracker", MessageBoxButton.YesNo);
          if(Result == MessageBoxResult.Yes) {
            BackgroundWorker bwUpdate = new BackgroundWorker();
            bwUpdate.DoWork += (sender2, e2) => {
              e2.Result = _updater.Update();
            };
            bwUpdate.RunWorkerCompleted += (sender2, e2) => {
              if((bool)e2.Result) {
                MessageBox.Show("Update complete. Please restart Deck Tracker", "Pack Tracker");
              }
              else {
                MessageBox.Show("Something went wrong. Update failed.", "Pack Tracker");
              }
            };
            bwUpdate.RunWorkerAsync();
          }
        }
      };
      bwCheck.RunWorkerAsync(LatestRelease);
    }

    public void OnUnload() {
    }

    public void OnUpdate() {
    }
  }
}
