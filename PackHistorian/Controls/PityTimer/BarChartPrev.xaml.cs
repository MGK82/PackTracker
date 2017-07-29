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
  public partial class BarChartPrev : UserControl, IBarChart {
    SeriesCollection _sc;
    ColumnSeries _cs;
    ChartValues<int> _prevTimer = new ChartValues<int>();

    public SeriesCollection Prev { get => _sc; }
    public Brush Fill { set => _cs.Fill = value; }
    public int Threshold { get; set; }
    public int? Average { get => DataContext is View.PityTimer ? ((View.PityTimer)DataContext).Average : null; }
    public string XTitle { get; set; }
    public string YTitle { get; set; }

    public BarChartPrev() {
      InitializeComponent();
      Chart.DataContext = this;

      _cs = new ColumnSeries() {
        Values = _prevTimer,
        Fill = Brushes.Red,
      };

      _sc = new SeriesCollection() {
        _cs,
      };

      DataContextChanged += (sender, e) => {
        if(e.NewValue is View.PityTimer) {
          View.PityTimer pt = (View.PityTimer)e.NewValue;
          _prevTimer.Clear();
          _prevTimer.AddRange(pt.Prev);

          pt.Prev.CollectionChanged += PrevChanged;
          pt.PropertyChanged += AverageChanged;
        } else {
          if(e.OldValue is View.PityTimer) {
            ((View.PityTimer)e.OldValue).Prev.CollectionChanged -= PrevChanged;
            ((View.PityTimer)e.OldValue).PropertyChanged -= AverageChanged;
          }
          _sc = new SeriesCollection();
        }

        OnPropertyChanged("Prev");
      };
    }

    private void AverageChanged(object sender, PropertyChangedEventArgs e) {
      if(e.PropertyName == "Average") {
        OnPropertyChanged(e.PropertyName);
      }
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
