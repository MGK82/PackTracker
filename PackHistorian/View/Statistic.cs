using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PackTracker.Entity;
using System.Collections.Specialized;
using System.ComponentModel;
using HearthDb.Enums;

namespace PackTracker.View {
  class Statistic : INotifyPropertyChanged{
    int _packId;
    List<Pack> _packs;

    int
      _commonAmount = 0,
      _commonPacks = 0,

      _rareAmount = 0,
      _rarePacks = 0,

      _epicAmount = 0,
      _epicPacks = 0,

      _legendaryAmount = 0,
      _legendaryPacks = 0,

      _totalAmount = 0;

    public int CommonAmount { get => _commonAmount; }
    public double CommonCards { get => _totalAmount == 0 ? 0 : (double)_commonAmount / _totalAmount; }
    public double CommonPacks { get => _packs.Count == 0 ? 0 : (double)_commonPacks / _packs.Count; }

    public int RareAmount { get => _rareAmount; }
    public double RareCards { get => _totalAmount == 0 ? 0 : (double)_rareAmount / _totalAmount; }
    public double RarePacks { get => _packs.Count == 0 ? 0 : (double)_rarePacks / _packs.Count; }

    public int EpicAmount { get => _epicAmount; }
    public double EpicCards { get => _totalAmount == 0 ? 0 : (double)_epicAmount / _totalAmount; }
    public double EpicPacks { get => _packs.Count == 0 ? 0 : (double)_epicPacks / _packs.Count; }

    public int LegendaryAmount { get => _legendaryAmount; }
    public double LegendaryCards { get => _totalAmount == 0 ? 0 : (double)_legendaryAmount / _totalAmount; }
    public double LegendaryPacks { get => _packs.Count == 0 ? 0 : (double)_legendaryPacks / _packs.Count; }

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
