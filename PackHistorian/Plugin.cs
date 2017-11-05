
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
    const string _name = "Pack Tracker";
    readonly static Version _version = new Version("1.2.4");

    private PackWatcher _watcher;
    Updater _updater;
    History _history;
    IHistoryStorage _historyStorage = new XmlHistory();
    Settings _settings;
    ISettingsStorage _settingsStorage = new XmlSettings();
    WindowManager _windows = new WindowManager(_name);
    View.AverageCollection _averageCollection;
    View.Cache.PityTimerRepository _pityTimers;

    public static Version CurrentVersion { get => _version; }

    public Plugin() {
      _watcher = new PackWatcher();
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


      //watcher
      _watcher.PackOpened += (sender, e) => {
        _history.Add(e.Pack);
        _historyStorage.Store(_history.Ascending);

        if(_settings.Spoil) {
          View.Average Average = _averageCollection.FindForPackId(e.Pack.Id);
          ToastManager.ShowCustomToast(new Controls.Toast(e.Pack, Average));
        }
      };

      _watcher.PackScreenEntered += (sender, e) => { if(_settings.PityOverlay) _windows.ShowPityTimerOverlay(_history, _pityTimers); };
      _watcher.PackScreenLeft += (sender, e) => _windows.ClosePityTimerOverlay();
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
        Menu.mnu_History.Click += (sender, e) => _windows.ShowHistoryWin(_history);
        Menu.mnu_Statistic.Click += (sender, e) => _windows.ShowStatisticWin(_history);
        Menu.mnu_Log.Click += (sender, e) => _windows.ShowLogWin(_history);
        Menu.mnu_Search.Click += (sender, e) => _windows.ShowSearchWin(_history);
        Menu.mnu_PityTimers.Click += (sender, e) => _windows.ShowPityWin(_history, _pityTimers);

        return Menu;
      }
    }

    public string Name => _name;
    public static string NAME => _name;

    public Version Version
    {
      get
      {
        return CurrentVersion;
      }
    }

    public void OnButtonPress() {
      _windows.ShowSettingsWin(_settings, _settingsStorage);
    }

    public void OnLoad() {
      _watcher.Start();

      if(_settings.Update) {
        BackgroundWorker bwCheck = new BackgroundWorker();
        bwCheck.DoWork += (sender, e) => {
          e.Result = _updater.NewVersionAvailable();
        };
        bwCheck.RunWorkerCompleted += (sender, e) => {
          if((bool?)e.Result == true) {
            _windows.ShowSettingsWin(_settings, _settingsStorage, typeof(Controls.Settings.Update));
          }
        };
        bwCheck.RunWorkerAsync();
      }
    }

    public void OnUnload() {
      _watcher.Stop();
    }

    public void OnUpdate() {
    }
  }
}
