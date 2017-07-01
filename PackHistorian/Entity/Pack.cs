using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackTracker.Entity {
  public class Pack {
    int _id;
    DateTime _time;
    IEnumerable<Card> _cards;

    public int Id { get { return _id; } }
    public DateTime Time { get { return _time; } }
    public IEnumerable<Card> Cards { get { return _cards; } }

    public Pack(int id, DateTime Time, IEnumerable<Card> Cards) {
      _id = id;
      _time = Time;
      _cards = Cards;
    }
  }
}
