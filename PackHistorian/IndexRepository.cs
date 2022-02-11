using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthDb.Enums;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using PackTracker.Entity;

namespace PackTracker {
  class IndexRepository {
    readonly StringBuilder _sb = new StringBuilder();
    Dictionary<string, string> _index = new Dictionary<string, string>();                       //<searchstring, id>
    Dictionary<string, List<Index>> _indexObjects = new Dictionary<string, List<Index>>();      //<id, index-objects>

    public IndexRepository(History History) {
      foreach(Pack Pack in History) {
        foreach(Entity.Card Card in Pack.Cards) {
          Add(new Index(Card, Pack.Time));
        }
      }

      History.CollectionChanged += (sender, e) => {
        if(e.Action == NotifyCollectionChangedAction.Add) {
          foreach(Pack Pack in e.NewItems) {
            foreach(Entity.Card Card in Pack.Cards) {
              Add(new Index(Card, Pack.Time));
            }
          }
        }
      };
    }

    void Add(Index Index) {
      if(!_index.ContainsValue(Index.Card.HDTCard.Id)) {
        HearthDb.Card DbCard = HearthDb.Cards.GetFromDbfId(Index.Card.HDTCard.DbfIf);

        string name = "";
        string text = "";
        name = DbCard.Name?.ToLower();
        text = DbCard.Text?.ToLower();

        string locName = "";
        string locText = "";

        if(Enum.TryParse(Config.Instance.SelectedLanguage, out Locale lang)) {
          locName = DbCard.GetLocName(lang)?.ToLower();
          locText = DbCard.GetLocText(lang)?.ToLower();
        }

        _sb.Append(locName).Append(name).Append(locText).Append(text);
        _index.Add(_sb.ToString(), Index.Card.HDTCard.Id);
        _sb.Clear();

        _indexObjects.Add(Index.Card.HDTCard.Id, new List<Index>());
      }

      _indexObjects[Index.Card.HDTCard.Id].Add(Index);
    }

    public IEnumerable<Index> Find(string searchString) {
      string[] elems = searchString.Split(' ');
      IEnumerable<KeyValuePair<string, string>> Filtered = _index.Where(x => x.Key.Contains(elems[0]));

      foreach(string elem in elems.Skip(1)) {
        Filtered = Filtered.Where(x => x.Key.Contains(elem));
      }

      List<Index> Result = new List<Index>();
      foreach(List<Index> Index in _indexObjects.Where(x => Filtered.Select(y => y.Value).Contains(x.Key)).Select(x => x.Value)) {
        Result.AddRange(Index);
      }

      return Result.Distinct();
    }
  }
}
