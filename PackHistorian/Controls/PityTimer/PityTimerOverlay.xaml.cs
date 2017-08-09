using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HearthDb.Enums;
using PackTracker.Entity;
using PackTracker.View.Cache;

namespace PackTracker.Controls.PityTimer {
  /// <summary>
  /// Interaktionslogik für PityTimerOverlay.xaml
  /// </summary>
  public partial class PityTimerOverlay : Window, INotifyPropertyChanged {
    int _packId;

    public int PackId => _packId;

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    public PityTimerOverlay(PackTracker.History History, PityTimerRepository PityTimers) {
      InitializeComponent();
      DataContext = this;

      _packId = History.Min(x => x.Id);
      Chart_Epic.DataContext = PityTimers.GetPityTimer(_packId, Rarity.EPIC, false, true);
      Chart_Leg.DataContext = PityTimers.GetPityTimer(_packId, Rarity.LEGENDARY, false, true);

      History.CollectionChanged += (sender, e) => {
        foreach(Pack Pack in e.NewItems) {
          Chart_Epic.DataContext = PityTimers.GetPityTimer(Pack.Id, Rarity.EPIC, false, true);
          Chart_Leg.DataContext = PityTimers.GetPityTimer(Pack.Id, Rarity.LEGENDARY, false, true);
          _packId = Pack.Id;
        }

        OnPropertyChanged("PackId");
      };
    }

    private void UniformGrid_Loaded(object sender, RoutedEventArgs e) {
      SetPosition();
    }

    private void SetPosition() {
      double relativeRight = .01;
      double relativeBottom = .35;
      
      Left = SystemParameters.PrimaryScreenWidth - (SystemParameters.PrimaryScreenWidth * relativeRight) - Width;
      Top = SystemParameters.PrimaryScreenHeight - (SystemParameters.PrimaryScreenHeight * relativeBottom) - Height;
    }

    private void Window_SourceInitialized(object sender, EventArgs e) {
      WindowInteropHelper helper = new WindowInteropHelper(this);
      Hearthstone_Deck_Tracker.User32.SetWindowExStyle(helper.Handle, Hearthstone_Deck_Tracker.User32.WsExTransparent | Hearthstone_Deck_Tracker.User32.WsExToolWindow);
    }
  }
}
