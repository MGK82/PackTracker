using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackChronicler.View {
  public class AverageCollection : INotifyCollectionChanged, IEnumerable<Average> {
    ObservableCollection<Average> _statistics = new ObservableCollection<Average>();
    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public AverageCollection(History History) {
      IEnumerable<int> PackIds = History.Select(x => x.Id).Distinct().OrderBy(x => x);
      foreach(int Id in PackIds) {
        _statistics.Add(new Average(Id, History));
      }

      History.CollectionChanged += (sender, e) => {
        if(sender is History) {
          if(e.Action == NotifyCollectionChangedAction.Add) {
            foreach(Entity.Pack Pack in e.NewItems) {
              if(!_statistics.Any(x => x.Id == Pack.Id)) {
                Add(new Average(Pack.Id, (History)sender));
              }
            }
          }
        }
      };

      _statistics.CollectionChanged += (sender, e) => { CollectionChanged?.Invoke(this, e); };
    }

    void Add(Average Statistic) {
      foreach(Average Stat in _statistics) {
        if(Stat.Id < Statistic.Id) {
          _statistics.Insert(_statistics.IndexOf(Stat), Statistic);
          return;
        }
      }

      _statistics.Add(Statistic);
    }

    public IEnumerator<Average> GetEnumerator() {
      return _statistics.GetEnumerator();

    }

    IEnumerator IEnumerable.GetEnumerator() {
      return _statistics.GetEnumerator();
    }

    public Average FindForPackId(int id) {
      return _statistics.SingleOrDefault(x => x.Id == id);
    }
  }
}
