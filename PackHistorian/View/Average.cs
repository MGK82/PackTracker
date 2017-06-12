using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthDb.Enums;
using System.Collections.Specialized;
using System.ComponentModel;

namespace PackChronicler.View {
  public class Average : INotifyPropertyChanged {
    int _packId;

    List<int>
      _countsEpic = new List<int>(),
      _countsLeg = new List<int>();

    bool
      _skippingEpic = true,
      _skippingLeg = true;

    int
      _currentEpic = 0,
      _currentLeg = 0;

    public event PropertyChangedEventHandler PropertyChanged;

    public int Id { get { return _packId; } }

    public int? AverageEpic { get { return _countsEpic.Count > 1 ? (int?)Math.Round(_countsEpic.Average(), 0) : null; } }
    public int? AverageLegendary { get { return _countsLeg.Count > 1 ? (int?)Math.Round(_countsLeg.Average(), 0) : null; } }

    public int CurrentEpic { get { return _currentEpic; } }
    public int CurrentLegendary { get { return _currentLeg; } }

    public Average(int PackId, History History) {
      _packId = PackId;
      AddCounts(History);

      History.CollectionChanged += History_CollectionChanged;
    }

    private void History_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
      if(e.Action == NotifyCollectionChangedAction.Add) {
          AddCounts(e.NewItems.Cast<Entity.Pack>());
      }
    }

    private void AddCounts(IEnumerable<Entity.Pack> Packs) {
      bool notifyAverageEpic = false;
      bool notifyAverageLegendary = false;
      bool notifyCurrent = false;

      foreach(Entity.Pack Pack in Packs) {
        if(Pack.Id == _packId) {
          _currentEpic++;
          _currentLeg++;

          notifyCurrent = true;

          if(Pack.Cards.Any(x => x.Rarity == Rarity.EPIC)) {
            if(_skippingEpic) {
              _skippingEpic = false;
            } else {
              _countsEpic.Add(_currentEpic);
              notifyAverageEpic = true;
            }

            _currentEpic = 0;
          }

          if(Pack.Cards.Any(x => x.Rarity == Rarity.LEGENDARY)) {
            if(_skippingLeg) {
              _skippingLeg = false;
            } else {
              _countsLeg.Add(_currentLeg);
              notifyAverageLegendary = true;
            }

            _currentLeg = 0;
          }
        }
      }

      if(notifyAverageEpic) {
        OnPropertyChanged("AverageEpic");
      }

      if(notifyAverageLegendary) {
        OnPropertyChanged("AverageLegendary");
      }

      if(notifyCurrent) {
        OnPropertyChanged("CurrentEpic");
        OnPropertyChanged("CurrentLegendary");
      }
    }

    private void OnPropertyChanged(string property) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
  }
}
