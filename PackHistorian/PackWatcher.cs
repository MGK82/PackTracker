using HearthMirror.Objects;
using Hearthstone_Deck_Tracker.Hearthstone;
using HDTCard = Hearthstone_Deck_Tracker.Hearthstone.Card;
using HearthWatcher;
using HearthWatcher.EventArgs;
using System.Collections.Generic;
using System.Windows;
using PackTracker.Event;
using PackTracker.Entity;
using System;
using Hearthstone_Deck_Tracker.Enums.Hearthstone;
using System.Diagnostics;

namespace PackTracker {
  delegate void PackOpenedEventHandler(object sender, PackOpenedEventArgs e);

  class PackWatcher {
    bool _running = false;
    List<Process> _hearthstones = new List<Process>();

    public bool Running { get { return _running; } }

    public event PackOpenedEventHandler PackOpened;
    public event EventHandler PackScreenEntered;
    public event EventHandler PackScreenLeft;

    public PackWatcher() {
      Hearthstone_Deck_Tracker.API.GameEvents.OnModeChanged.Add(HandleMode);
    }

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

    private void HandleMode(Mode Mode) {
      if(!_running) return;

      if(Mode == Mode.PACKOPENING) {
        foreach(Process hs in Process.GetProcessesByName("Hearthstone")) {
          if(hs is Process && !_hearthstones.Contains(hs)) {
            hs.EnableRaisingEvents = true;
            hs.Exited += Hs_Exited;
            _hearthstones.Add(hs);
          }
        }

        PackScreenEntered.Invoke(this, new EventArgs());
      } else {
        if(_hearthstones.Count > 0) {
          _hearthstones.ForEach(x => { x.Exited -= Hs_Exited; x.EnableRaisingEvents = false; });
          _hearthstones.Clear();

          PackScreenLeft?.Invoke(this, new EventArgs());
        }
      }
    }

    private void Hs_Exited(object sender, EventArgs e) {
      if(sender is Process) {
        Process hs = (Process)sender;
        if(_hearthstones.Contains(hs)) {
          hs.Exited -= Hs_Exited;
          hs.EnableRaisingEvents = false;
          _hearthstones.Remove(hs);
        }
      }

      if(_hearthstones.Count == 0) {
        PackScreenLeft?.Invoke(this, new EventArgs());
      }
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
