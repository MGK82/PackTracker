using System;
using System.Collections.Generic;
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
using LiveCharts.Wpf;
using LiveCharts;
using LiveCharts.Defaults;
using System.ComponentModel;

namespace PackTracker.Controls.PityTimer {
  /// <summary>
  /// Interaktionslogik für BarChartSingle.xaml
  /// </summary>
  public partial class BarChartSingle : UserControl, INotifyPropertyChanged {
    ObservableValue _single = new ObservableValue(0);
    ColumnSeries _cs = new ColumnSeries();
    SeriesCollection _sc;

    public SeriesCollection Single => _sc;
    public string Title { get; set; }
    public int Threshold { get; set; }
    public int SoftThreshold { get; set; }
    public double? MaxValue { get; set; }
    public AxisPosition Position { get; set; } = AxisPosition.LeftBottom;
    public Brush Fill { set {
        _cs.Fill = value.Clone();
        _cs.Fill.Opacity = .9;
    } }

    public int? Average => DataContext is View.PityTimer ? ((View.PityTimer)DataContext).Average : null;

    public BarChartSingle() {
      InitializeComponent();
      Chart.DataContext = this;

      ChartValues<ObservableValue> cv = new ChartValues<ObservableValue>();
      cv.Add(_single);

      _cs.Values = cv;

      _sc = new SeriesCollection() {
        _cs,
      };

      DataContextChanged += BarChartSingle_DataContextChanged;
    }

    private void BarChartSingle_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
      if(e.OldValue is View.PityTimer) {
        View.PityTimer pt = (View.PityTimer)e.OldValue;
        pt.PropertyChanged -= Pt_PropertyChanged;
      }

      if(e.NewValue is View.PityTimer) {
        View.PityTimer pt = (View.PityTimer)e.NewValue;
        _single.Value = pt.Current;
        pt.PropertyChanged += Pt_PropertyChanged;
      } else {
        _single.Value = 0;
      }

      OnPropertyChanged("Average");
    }

    private void Pt_PropertyChanged(object sender, PropertyChangedEventArgs e) {
      switch(e.PropertyName) {
        case "Current":
          _single.Value = ((View.PityTimer)sender).Current;
          break;
        case "Average":
          OnPropertyChanged(e.PropertyName);
          break;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
  }
}
