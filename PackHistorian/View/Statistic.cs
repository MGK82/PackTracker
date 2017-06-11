using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthDb.Enums;
using System.Collections.Specialized;
using System.ComponentModel;

namespace PackChronicler.View {
  class Statistic : INotifyPropertyChanged {
    int _packId;
    Rarity _rarity;
    List<int> _counts = new List<int>();
    bool _skipping = true;
    int _current = 0;

    public event PropertyChangedEventHandler PropertyChanged;

    public int? Average { get { return _counts.Count > 0 ? (int?)Math.Round(_counts.Average(), 0) : null; } }
    public int Current { get { return _current; } }

    public Statistic(int PackId, Rarity Rarity, History History) {
      _packId = PackId;
      _rarity = Rarity;
      AddCounts(History);

      History.CollectionChanged += History_CollectionChanged;
    }

    private void AddCounts(IEnumerable<Entity.Pack> Packs) {
      bool notifyAverage = false;
      bool notifyCurrent = false;

      foreach(Entity.Pack Pack in Packs) {
        if(Pack.Id == _packId) {
          _current++;
          notifyCurrent = true;

          if(Pack.Cards.Any(x => x.Rarity == _rarity)) {
            if(_skipping) {
              _skipping = false;
            } else {
              _counts.Add(_current);
              _current = 0;
              notifyAverage = true;
            }
          }
        }
      }

      if(notifyAverage) {
        OnPropertyChanged("Average");
      }

      if(notifyCurrent) {
        OnPropertyChanged("Current");
      }
    }

    private void History_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
      if(e.Action == NotifyCollectionChangedAction.Add) {
        if(e.NewItems is IEnumerable<Entity.Pack>) {
          AddCounts((IEnumerable<Entity.Pack>)e.NewItems);
        }
      }
    }

    private void OnPropertyChanged(string property) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
  }
}
