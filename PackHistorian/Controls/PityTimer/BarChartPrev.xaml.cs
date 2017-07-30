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
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace PackTracker.Controls.PityTimer {
  /// <summary>
  /// Interaktionslogik für BarChart.xaml
  /// </summary>
  public partial class BarChartPrev : UserControl, INotifyPropertyChanged {
    SeriesCollection _sc;
    ColumnSeries _cs;
    ObservableValue _currTimer = new ObservableValue(0);
    ChartValues<ObservableValue> _prevTimer = new ChartValues<ObservableValue>();

    Brush _fillOrig, _fillPrev, _fillCurr;

    public SeriesCollection Prev { get => _sc; }
    public Brush Fill { set {
        _fillPrev = value.Clone();
        _fillPrev.Opacity = .9;
        _fillCurr = value.Clone();
        _fillCurr.Opacity = .5;
        _fillOrig = value;
    } }

    public int SoftThreshold { get; set; }
    public int Threshold { get; set; }
    public int? Average { get => DataContext is View.PityTimer ? ((View.PityTimer)DataContext).Average : null; }
    public string XTitle { get; set; }
    public string YTitle { get; set; }

    public BarChartPrev() {
      InitializeComponent();
      Chart.DataContext = this;

      CartesianMapper<ObservableValue> mapper = Mappers.Xy<ObservableValue>()
        .Fill((x, y) => x == _currTimer ? _fillCurr : _fillPrev)
        .Y((obs, y) => obs.Value)
        .X((obs, x) => x)
      ;
      _cs = new ColumnSeries(mapper) {
        Values = _prevTimer,
      };

      _sc = new SeriesCollection() {
        _cs,
      };

      DataContextChanged += (sender, e) => {
        if(e.NewValue is View.PityTimer) {
          View.PityTimer pt = (View.PityTimer)e.NewValue;
          _prevTimer.Clear();
          _prevTimer.AddRange(pt.Prev.Select(x => new ObservableValue(x)));
          _currTimer = new ObservableValue(pt.Current);
          _prevTimer.Add(_currTimer);

          pt.Prev.CollectionChanged += PrevChanged;
          pt.PropertyChanged += AverageChanged;
          pt.PropertyChanged += CurrentChanged;
        } else {
          if(e.OldValue is View.PityTimer) {
            ((View.PityTimer)e.OldValue).Prev.CollectionChanged -= PrevChanged;
            ((View.PityTimer)e.OldValue).PropertyChanged -= AverageChanged;
            ((View.PityTimer)e.OldValue).PropertyChanged += CurrentChanged;
          }
          _sc = new SeriesCollection();
        }

        OnPropertyChanged("Prev");
      };
    }

    private void CurrentChanged(object sender, PropertyChangedEventArgs e) {
      if(e.PropertyName == "Current" && sender is View.PityTimer) {
        View.PityTimer pt = (View.PityTimer)sender;
        _currTimer.Value = pt.Current;
      }
    }

    private void AverageChanged(object sender, PropertyChangedEventArgs e) {
      if(e.PropertyName == "Average") {
        OnPropertyChanged(e.PropertyName);
      }
    }

    private void PrevChanged(object sender, NotifyCollectionChangedEventArgs e) {
      if(DataContext is View.PityTimer) {
        View.PityTimer pt = (View.PityTimer)DataContext;
        _currTimer = new ObservableValue(pt.Current);
        _prevTimer.Add(_currTimer);
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
  }
}
