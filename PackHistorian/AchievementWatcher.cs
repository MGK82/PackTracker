using HearthMirror.Objects;
using Hearthstone_Deck_Tracker.Hearthstone;
using HDTCard = Hearthstone_Deck_Tracker.Hearthstone.Card;
using HearthWatcher;
using HearthWatcher.EventArgs;
using System.Collections.Generic;
using System.Windows;

namespace PackHistorian {
  class AchievementsWatcher {
    public AchievementsWatcher() {
      Watchers.PackWatcher.NewPackEventHandler += NewPack;
      Watchers.PackWatcher.Run();
    }

    private void NewPack(object sender, PackEventArgs e) {

      string msg = e.PackId.ToString();
      foreach(var Card in e.Cards) {
        HDTCard cardFromId = Database.GetCardFromId(Card.Id);
        msg += "\n" + cardFromId.Name + " (" + cardFromId.LocalizedName + ")";
      }

      MessageBox.Show(msg);
    }
  }
}
