using HearthMirror.Objects;
using Hearthstone_Deck_Tracker.Hearthstone;
using HDTCard = Hearthstone_Deck_Tracker.Hearthstone.Card;
using HearthWatcher;
using HearthWatcher.EventArgs;
using System.Collections.Generic;
using System.Windows;
using PackHistorian.Event;
using PackHistorian.Entity;
using System;

namespace PackHistorian {
  delegate void PackOpenedEventHandler(object sender, PackOpenedEventArgs e);

  class AchievementsWatcher {
    bool _running = false;

    public bool Running { get { return _running; } }

    public AchievementsWatcher() {
      Watchers.PackWatcher.Run();
    }

    public event PackOpenedEventHandler PackOpened;

    private void NewPack(object sender, PackEventArgs e) {
      DateTime Time = DateTime.Now;
      List<Entity.Card> Cards = new List<Entity.Card>();

      foreach(var Card in e.Cards) {
        HDTCard cardFromId = Database.GetCardFromId(Card.Id);
        Cards.Add(new Entity.Card(cardFromId, Card.Premium));
      }

      OnPackOpened(new Pack(e.PackId, Time, Cards));
    }

    void OnPackOpened(Pack Pack) {
      PackOpened?.Invoke(this, new PackOpenedEventArgs(Pack));
    }

    public void Start() {
      if(!_running) {
        Watchers.PackWatcher.NewPackEventHandler += NewPack;
        _running = true;
      }
    }

    public void Stop() {
      if(_running) {
        Watchers.PackWatcher.NewPackEventHandler -= NewPack;
        _running = false;
      }
    }
  }
}
