using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using PackHistorian.Entity;
using Hearthstone_Deck_Tracker;
using System.IO;

namespace PackHistorian.Storage {
  class XmlStorage : IStorage {
    public History Fetch() {
      throw new NotImplementedException();
    }

    public void Store(History History) {
      XmlDocument Xml = new XmlDocument();
      Xml.AppendChild(Xml.CreateXmlDeclaration("1.0", "UTF-8", null));

      XmlNode Root = Xml.CreateElement("History");
      Xml.AppendChild(Root);

      foreach(Pack Pack in History) {
        XmlNode PackNode = Xml.CreateElement("Pack");
        Root.AppendChild(PackNode);

        XmlAttribute Time = Xml.CreateAttribute("Time");
        Time.Value = Pack.Time.Ticks.ToString();
        PackNode.Attributes.Append(Time);

        XmlAttribute PackId = Xml.CreateAttribute("Id");
        PackId.Value = Pack.Id.ToString();
        PackNode.Attributes.Append(PackId);

        foreach(Card Card in Pack.Cards) {
          XmlNode CardNode = Xml.CreateElement("Card");
          PackNode.AppendChild(CardNode);

          XmlAttribute CardId = Xml.CreateAttribute("Id");
          CardId.Value = Card.HDTCard.Id;
          CardNode.Attributes.Append(CardId);

          XmlAttribute Premium = Xml.CreateAttribute("Premium");
          Premium.Value = Card.Premium.ToString();
          CardNode.Attributes.Append(Premium);
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
