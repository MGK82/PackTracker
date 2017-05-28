using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using PackHistorian.Entity;
using Hearthstone_Deck_Tracker;
using System.IO;
using Hearthstone_Deck_Tracker.Hearthstone;
using HDTCard = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace PackHistorian.Storage {
  class XmlStorage : IStorage {
    public History Fetch() {
      History History = new History();

      string path = Path.Combine(Config.AppDataPath, "PackHistorian", "History.xml");
      if(File.Exists(path)) {
        XmlDocument Xml = new XmlDocument();
        Xml.Load(path);
        XmlNode Root = Xml.SelectSingleNode("history");

        if(Root != null) {
          XmlNodeList Packs = Root.SelectNodes("pack");

          if(Packs.Count > 0) {
            foreach(XmlNode Pack in Packs) {

                if(int.TryParse(Pack.Attributes["id"]?.Value, out int packId) && long.TryParse(Pack.Attributes["time"]?.Value, out long ticks)) {
                  DateTime Time = new DateTime(ticks);
                  XmlNodeList Cards = Pack.SelectNodes("card");

                if(Cards.Count > 0) {
                  List<Entity.Card> HistoryCards = new List<Entity.Card>();

                  foreach(XmlNode Card in Cards) {
                    string cardId = Card.Attributes["id"]?.Value;
                    if(!string.IsNullOrEmpty(cardId)) {
                      HDTCard HDTCard = Database.GetCardFromId(cardId);
                      string premium = Card.Attributes["premium"]?.Value;

                      HistoryCards.Add(new Entity.Card(HDTCard, premium == "premium"));
                    } else {
                      return new History();
                    }
                  }

                  History.Add(new Pack(packId, Time, HistoryCards));
                } else {
                  return new History();
                }
              } else {
                return new History();
              }
            }
          }
        }
      }

      return History;
    }

    public void Store(History History) {
      XmlDocument Xml = new XmlDocument();
      Xml.AppendChild(Xml.CreateXmlDeclaration("1.0", "UTF-8", null));

      XmlNode Root = Xml.CreateElement("history");
      Xml.AppendChild(Root);

      foreach(Pack Pack in History) {
        XmlNode PackNode = Xml.CreateElement("pack");
        Root.AppendChild(PackNode);

        XmlAttribute Time = Xml.CreateAttribute("time");
        Time.Value = Pack.Time.Ticks.ToString();
        PackNode.Attributes.Append(Time);

        XmlAttribute PackId = Xml.CreateAttribute("id");
        PackId.Value = Pack.Id.ToString();
        PackNode.Attributes.Append(PackId);

        foreach(Entity.Card Card in Pack.Cards) {
          XmlNode CardNode = Xml.CreateElement("card");
          PackNode.AppendChild(CardNode);

          XmlAttribute CardId = Xml.CreateAttribute("id");
          CardId.Value = Card.HDTCard.Id;
          CardNode.Attributes.Append(CardId);

          if(Card.Premium) {
            XmlAttribute Premium = Xml.CreateAttribute("premium");
            Premium.Value = "premium";
            CardNode.Attributes.Append(Premium);
          }
        }
      }

      string path = Path.Combine(Config.AppDataPath, "PackHistorian");
      if(!Directory.Exists(path)) {
        Directory.CreateDirectory(path);
      }
      path = Path.Combine(path, "History.xml");

      Xml.Save(path);
    }
  }
}
