
using Hearthstone_Deck_Tracker.Plugins;
using System;
using System.Reflection;
using System.Windows.Controls;
using PackChronicler.Storage;
using Hearthstone_Deck_Tracker.Utility.Toasts;

namespace PackChronicler {
  public class Plugin : IPlugin {
    private AchievementsWatcher _watcher;
    History _history;
    IStorage _storage = new XmlStorage();
    Controls.History _historyWin;

    Controls.History HistoryWin {
      get {
        if(_historyWin == null) {
          _historyWin = new Controls.History(_history);
          _historyWin.Closed += (sender, e) => { _historyWin = null; };
        }

        return _historyWin;
      }
    }

    public Plugin() {
      _watcher = new AchievementsWatcher();
      try {
        _history = _storage.Fetch();
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
        return "Pack Chronicler";
      }
    }

    public string Description
    {
      get
      {
        return "";
      }
    }

    public MenuItem MenuItem
    {
      get
      {
        return null;
      }
    }

    public string Name
    {
      get
      {
        return "Pack Chronicler";
      }
    }

    public Version Version
    {
      get
      {
        return Assembly.GetAssembly(this.GetType()).GetName().Version;
      }
    }

    public void OnButtonPress() {
      HistoryWin.Show();
    }

    public void OnLoad() {
      _watcher.Start();

      _watcher.PackOpened += (sender, e) => {
        _history.Add(e.Pack);
        _storage.Store(_history.Ascending);
      };

      _watcher.PackOpened += (sender, e) => {
        ToastManager.ShowCustomToast(new Controls.Cards(e.Pack.Cards));
      };
    }

    public void OnUnload() {
    }

    public void OnUpdate() {
    }
  }
}
