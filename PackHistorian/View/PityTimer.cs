using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PackTracker.Entity;

namespace PackTracker.View {
  public class PityTimer : INotifyPropertyChanged {
    bool _waitForFirst;
    Func<Pack, bool> _condition;

    int _current = 0;
    ObservableCollection<int> _prev = new ObservableCollection<int>();

    public int Current { get => _current; }
    public ObservableCollection<int> Prev { get => _prev; }
    public int? Average { get => _prev.Count > 0 ? (int?)Math.Round(_prev.Average(), 0) : null; }

    public PityTimer(History History, Func<Pack, bool> Condition, bool skipFirst) {
      _waitForFirst = skipFirst;
      _condition = Condition;

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
      if(_condition(Pack)) {
        if(_waitForFirst) {
          _waitForFirst = false;
        } else {
          _prev.Add(_current);
          _current = 0;
          OnPropertyChanged("Current");
        }
      } else {
        if(!_waitForFirst) {
          _current++;
          OnPropertyChanged("Current");
        }
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
  }
}
