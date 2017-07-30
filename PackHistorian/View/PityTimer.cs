using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthDb.Enums;
using PackTracker.Entity;

namespace PackTracker.View {
  public class PityTimer : INotifyPropertyChanged {
    int _packId;
    bool _waitForFirst;
    Rarity _rarity;
    bool _premium;

    int _current = 0;
    ObservableCollection<int> _prev = new ObservableCollection<int>();

    public int Current { get => _current; }
    public ObservableCollection<int> Prev { get => _prev; }
    public int? Average { get => _prev.Count > 0 ? (int?)Math.Round(_prev.Average(), 0) : null; }

    public PityTimer(History History, int packId, Rarity rarity, bool premium, bool skipFirst) {
      _packId = packId;
      _rarity = rarity;
      _premium = premium;
      _waitForFirst = skipFirst;

      foreach(Pack Pack in History) {
        AddPack(Pack);
      }

      History.CollectionChanged += (sender, e) => {
        if(e.Action == NotifyCollectionChangedAction.Add) {
          foreach(Pack Pack in e.NewItems) {
            AddPack(Pack);
          }
        }
      };
    }

    void AddPack(Pack Pack) {
      if(Pack.Id != _packId) {
        return;
      }

      if(Condition(Pack)) {
        int newCurr = _current;
        _current = 0;

        if(_waitForFirst) {
          _waitForFirst = false;
        } else {
          _prev.Add(newCurr);
          OnPropertyChanged("Average");
        }
      } else {
        _current++;
      }

      OnPropertyChanged("Current");
    }

    bool Condition(Pack Pack) {
      return Pack.Cards.Any(x => x.Rarity == _rarity && (_premium ? x.Premium : true));
    }

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
  }
}
