
using Hearthstone_Deck_Tracker.Plugins;
using System;
using System.Reflection;
using System.Windows.Controls;
using PackChronicler.Storage;

namespace PackChronicler {
  public class Plugin : IPlugin {
    private AchievementsWatcher _watcher;
    History _history;
    IStorage _storage = new XmlStorage();

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
    }

    public void OnLoad() {
      _watcher.Start();

      _watcher.PackOpened += (sender, e) => {
        _history.Add(e.Pack);
        _storage.Store(_history.Ascending);
      };

      //List<Entity.Card> temp = new List<Entity.Card>() {
      //  { new Entity.Card(Database.GetCardFromId("GVG_043"), false) },
      //  { new Entity.Card(Database.GetCardFromId("GVG_043"), true) },
      //  { new Entity.Card(Database.GetCardFromId("GVG_043"), false) },
      //  { new Entity.Card(Database.GetCardFromId("GVG_043"), true) },
      //  { new Entity.Card(Database.GetCardFromId("GVG_043"), false) },
      //};
      //_history.Add(new Entity.Pack(2, DateTime.Now, temp));
      //MessageBox.Show("Nach mock: " + _history.Count);
      //_storage.Store(_history);
    }

    public void OnUnload() {
    }

    public void OnUpdate() {
    }
  }
}
