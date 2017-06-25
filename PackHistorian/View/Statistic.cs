using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PackChronicler.Entity;
using System.Collections.Specialized;
using System.ComponentModel;
using HearthDb.Enums;

namespace PackChronicler.View {
  class Statistic : INotifyPropertyChanged{
    int _packId;
    List<Pack> _packs;

    int _commonAmount = 0;
    double _commonPacks = 0;

    int _rareAmount = 0;
    double _rarePacks = 0;

    int _epicAmount = 0;
    double _epicPacks = 0;

    int _legendaryAmount = 0;
    double _legendaryPacks = 0;

    int _totalAmount = 0;

    public int CommonAmount { get => _commonAmount; }
    public double CommonCards { get => 100d / _totalAmount * _commonAmount; }
    public double CommonPacks { get => 100d / _packs.Count * _commonPacks; }

    public int RareAmount { get => _rareAmount; }
    public double RareCards { get => 100d / _totalAmount * _rareAmount; }
    public double RarePacks { get => 100d / _packs.Count * _rarePacks; }

    public int EpicAmount { get => _epicAmount; }
    public double EpicCards { get => 100d / _totalAmount * _epicAmount; }
    public double EpicPacks { get => 100d / _packs.Count * _epicPacks; }

    public int LegendaryAmount { get => _legendaryAmount; }
    public double LegendaryCards { get => 100d / _totalAmount * _legendaryAmount; }
    public double LegendaryPacks { get => 100d / _packs.Count * _legendaryPacks; }

    public int TotalPacks { get => _packs.Count; }

    public Statistic(int packId, History History) {
      _packId = packId;
      _packs = new List<Pack>(History.Where(x => x.Id == packId));

      foreach(Pack Pack in _packs) {
        CountRarity(Pack);
      }

      History.CollectionChanged += (sender, e) => {
        if(e.Action == NotifyCollectionChangedAction.Add) {
          foreach(Pack Pack in e.NewItems) {
            if(Pack.Id == _packId) {
              _packs.Add(Pack);
              CountRarity(Pack);

              if(Pack.Cards.Any(x => x.Rarity == Rarity.COMMON)) {
                OnPropertyChanged("CommonAmount");
              }

              if(Pack.Cards.Any(x => x.Rarity == Rarity.RARE)) {
                OnPropertyChanged("RareAmount");
              }

              if(Pack.Cards.Any(x => x.Rarity == Rarity.EPIC)) {
                OnPropertyChanged("EpicAmount");
              }

              if(Pack.Cards.Any(x => x.Rarity == Rarity.LEGENDARY)) {
                OnPropertyChanged("LegendaryAmount");
              }

              OnPropertyChanged("CommonCards");
              OnPropertyChanged("CommonPacks");
              OnPropertyChanged("RareCards");
              OnPropertyChanged("RarePacks");
              OnPropertyChanged("EpicCards");
              OnPropertyChanged("EpicPacks");
              OnPropertyChanged("LegendaryCards");
              OnPropertyChanged("LegendaryPacks");
              OnPropertyChanged("TotalPacks");
            }
          }
        }
      };
    }

    void CountRarity(Pack Pack) {
      bool
        hasCommon = false,
        hasRare = false,
        hasEpic = false,
        hasLegendary = false;

      foreach(Card Card in Pack.Cards) {
        switch(Card.Rarity) {
          case Rarity.COMMON:
            _commonAmount++;
            hasCommon = true;
            break;
          case Rarity.RARE:
            _rareAmount++;
            hasRare = true;
            break;
          case Rarity.EPIC:
            _epicAmount++;
            hasEpic = true;
            break;
          case Rarity.LEGENDARY:
            hasLegendary = true;
            _legendaryAmount++;
            break;
        }

        _totalAmount++;
      }

      if(hasCommon) {
        _commonPacks++;
      }

      if(hasRare) {
        _rarePacks++;
      }

      if(hasEpic) {
        _epicPacks++;
      }

      if(hasLegendary) {
        _legendaryPacks++;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
  }
}
