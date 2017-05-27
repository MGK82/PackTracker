
using Hearthstone_Deck_Tracker.Plugins;
using System;
using System.Reflection;
using System.Windows.Controls;

namespace PackHistorian {
  public class Plugin : IPlugin {
    private AchievementsWatcher _watcher;

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
        return "Pack Historian";
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
        return "Pack Historian";
      }
    }

    public Version Version
    {
      get
      {
        return Assembly.GetEntryAssembly().GetName().Version;
      }
    }

    public void OnButtonPress() {
    }

    public void OnLoad() {
      _watcher = new AchievementsWatcher();
    }

    public void OnUnload() {
    }

    public void OnUpdate() {
    }
  }
}
