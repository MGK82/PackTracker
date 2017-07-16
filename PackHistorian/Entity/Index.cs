using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackTracker.Entity {
  class Index {
    Card _card;
    DateTime _datetime;

    public Card Card { get => _card; }
    public DateTime DateTime { get => _datetime; }

    public Index(Card Card, DateTime DateTime) {
      _card = Card;
      _datetime = DateTime;
    }
  }
}
