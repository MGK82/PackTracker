using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDTCard = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace PackChronicler.Entity {
  public class Card {
    HDTCard _card;
    bool _premium;

    public HDTCard HDTCard { get { return _card; } }
    public bool Premium { get { return _premium; } }

    public Card(HDTCard Card, bool premium) {
      _card = Card;
      _premium = premium;
    }
  }
}
