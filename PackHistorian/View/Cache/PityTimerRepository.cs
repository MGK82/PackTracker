using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HearthDb.Enums;

namespace PackTracker.View.Cache {
  public class PityTimerRepository {
    History _history;
    List<PityTimer> _cache = new List<PityTimer>();

    public PityTimerRepository(History History) {
      _history = History;
    }

    public PityTimer GetPityTimer(int packId, Rarity rarity, bool premium, bool skipFirst) {
      PityTimer pt = _cache.FirstOrDefault(x => x.PackId == packId && x.Rarity == rarity && x.Premium == premium && x.SkipFirst == skipFirst);
      if(!(pt is PityTimer)) {
        pt = new PityTimer(_history, packId, rarity, premium, skipFirst);
        _cache.Add(pt);
      }

      return pt;
    }
  }
}
