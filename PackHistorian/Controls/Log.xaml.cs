using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HearthDb.Enums;
using PackTracker.Entity;
using PackTracker.View;

namespace PackTracker.Controls {
  /// <summary>
  /// Interaktionslogik für Log.xaml
  /// </summary>
  public partial class Log {
    SolidColorBrush Legendary, Epic, Rare;

    public Log(PackTracker.History History) {
      InitializeComponent();
      Legendary = (SolidColorBrush)FindResource("Legendary");
      Epic = (SolidColorBrush)FindResource("Epic");
      Rare = (SolidColorBrush)FindResource("Rare");

      Loaded += (sender, e) => AddLogs(History);

      History.CollectionChanged += (sender, e) => {
        if(e.Action == NotifyCollectionChangedAction.Add) {
          AddLogs(e.NewItems.Cast<Pack>());
        }
      };
    }

    void AddLogs(IEnumerable<Pack> Packs) {
      StringBuilder sb = new StringBuilder();
      DateTimeConverter DateTimeConverter = new DateTimeConverter();
      PackNameConverter PackNameConverter = new PackNameConverter();
      string sep = ",";

      foreach(Pack Pack in Packs) {
        sb.Clear();

        string date = DateTimeConverter.Convert(Pack.Time, null, null, null).ToString();
        string packname = PackNameConverter.Convert(Pack.Id, null, null, null).ToString();
        int commons = Pack.Cards.Count(x => x.Rarity == Rarity.COMMON);
        int commonGolds = commons > 0 ? Pack.Cards.Count(x => x.Premium && x.Rarity == Rarity.COMMON) : 0;
        int rares = Pack.Cards.Count(x => x.Rarity == Rarity.RARE);
        int rareGolds = rares > 0 ? Pack.Cards.Count(x => x.Premium && x.Rarity == Rarity.RARE) : 0;
        int epics = Pack.Cards.Count(x => x.Rarity == Rarity.EPIC);
        int epicGolds = epics > 0 ? Pack.Cards.Count(x => x.Premium && x.Rarity == Rarity.EPIC) : 0;
        int legendarys = Pack.Cards.Count(x => x.Rarity == Rarity.LEGENDARY);
        int legendaryGolds = legendarys > 0 ? Pack.Cards.Count(x => x.Premium && x.Rarity == Rarity.LEGENDARY) : 0;

        SolidColorBrush Color = null as SolidColorBrush;
        if(legendarys > 0) {
          Color = Legendary;
        }
        else if(epics > 0) {
          Color = Epic;
        }
        else {
          Color = Rare;
        }

        sb
          .Append(date).Append(": ")
          .Append(packname).Append("(")
          .Append(commons);
        AddGoldStars(commonGolds, Color, sb);

        sb
          .Append(sep)
          .Append(rares);
        AddGoldStars(rareGolds, Color, sb);

        sb
          .Append(sep)
          .Append(epics);
        AddGoldStars(epicGolds, Color, sb);

        sb
          .Append(sep)
          .Append(legendarys);
        AddGoldStars(legendaryGolds, Color, sb);

        sb.AppendLine(")");
        txt_Log.Inlines.Add(new Run(sb.ToString()) { Foreground = Color });
      }

      sv_Scrollbar.ScrollToEnd();
    }

    void AddGoldStars(int amount, SolidColorBrush Color, StringBuilder sb) {
      if(amount > 0) {
        txt_Log.Inlines.Add(new Run(sb.ToString()) { Foreground = Color });
        sb.Clear().Append('*', amount);

        txt_Log.Inlines.Add(new Run(sb.ToString()) { Foreground = Brushes.Gold });
        sb.Clear();
      }
    }
  }
}
