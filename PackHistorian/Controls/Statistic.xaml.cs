using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Collections.Specialized;
using PackTracker.Entity;

namespace PackTracker.Controls {
  /// <summary>
  /// Interaktionslogik für Statistic.xaml
  /// </summary>
  public partial class Statistic {
    ObservableCollection<int> _dropDown;
    public Statistic(PackTracker.History History) {
      InitializeComponent();

      Dictionary<int, View.Statistic> _statistics = new Dictionary<int, View.Statistic>();

      _dropDown = new ObservableCollection<int>(History.Select(x => x.Id).Distinct().OrderBy(x => x));
      dd_Packs.ItemsSource = _dropDown;

      dd_Packs.SelectionChanged += (sender, e) => {
        if(e.AddedItems.Count == 1) {
          int selection = (int)e.AddedItems[0];
          if(!_statistics.ContainsKey(selection)) {
            _statistics.Add(selection, new View.Statistic(selection, History));
          }

          dp_Statistic.DataContext = _statistics[selection];
        } else {
          dp_Statistic.DataContext = null;
        }
      };

      if(_dropDown.Count > 0) {
        dd_Packs.SelectedIndex = 0;
      } else {
        _dropDown.CollectionChanged += DropDown_Initialize;
      }

      History.CollectionChanged += DropDown_NewEntry;
      dd_Packs.Focus();
    }

    //TODO: Testen
    private void DropDown_Initialize(object sender, NotifyCollectionChangedEventArgs e) {
      if(e.Action == NotifyCollectionChangedAction.Add) {
        dd_Packs.SelectedItem = e.NewItems[0];
        if(sender is INotifyCollectionChanged) {
          ((INotifyCollectionChanged)sender).CollectionChanged -= DropDown_Initialize;
        }
      }
    }

    private void DropDown_NewEntry(object sender, NotifyCollectionChangedEventArgs e) {
      if(e.Action == NotifyCollectionChangedAction.Add) {
        foreach(Pack newPack in e.NewItems) {
          if(!_dropDown.Contains(newPack.Id)) {
            bool isInserted = false;

            foreach(int id in _dropDown) {
              if(newPack.Id < id) {
                _dropDown.Insert(_dropDown.IndexOf(id), newPack.Id);
                isInserted = true;
                break;
              }
            }

            if(!isInserted) {
              _dropDown.Add(newPack.Id);
            }
          }
        }
      }
    }
  }
}
