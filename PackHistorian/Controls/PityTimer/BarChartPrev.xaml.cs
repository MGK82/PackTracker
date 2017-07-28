using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace PackTracker.Controls.PityTimer {
  /// <summary>
  /// Interaktionslogik für BarChart.xaml
  /// </summary>
  public partial class BarChartPrev : UserControl, INotifyPropertyChanged {
    SeriesCollection _sc;
    ChartValues<int> _prevTimer = new ChartValues<int>();

    public SeriesCollection SystemCollection { get => _sc; }

    public BarChartPrev() {
      InitializeComponent();
      Chart.DataContext = this;

      _sc = new SeriesCollection() {
        new ColumnSeries() {
          Values = _prevTimer,
          Fill = Brushes.Red,
        }
      };

      DataContextChanged += (sender, e) => {
        if(e.NewValue is View.PityTimer) {
          View.PityTimer pt = (View.PityTimer)e.NewValue;
          _prevTimer.Clear();
          _prevTimer.AddRange(pt.Prev);

          pt.Prev.CollectionChanged += PrevChanged;
        } else {
          if(e.OldValue is View.PityTimer) {
            ((View.PityTimer)e.OldValue).Prev.CollectionChanged -= PrevChanged;
          }
          _sc = new SeriesCollection();
        }

        OnPropertyChanged("SystemCollection");
      };
    }

    private void PrevChanged(object sender, NotifyCollectionChangedEventArgs e) {
      if(e.Action == NotifyCollectionChangedAction.Add) {
        _prevTimer.AddRange(e.NewItems.Cast<int>());
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
  }
}
