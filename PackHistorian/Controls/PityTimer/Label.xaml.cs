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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace PackTracker.Controls.PityTimer {
  /// <summary>
  /// Interaktionslogik für Label.xaml
  /// </summary>
  public partial class Label : UserControl, INotifyPropertyChanged {
    int _curr = 0;

    public int Current {
      get { return _curr; }
      set {
        if(_curr == value) return;
        pt_Curr.Transition = value > _curr ? TransitionType.Down : TransitionType.Up;
        _curr = value;
        OnPropertyChanged("Current");
      }
    }

    public int Limit { get; set; }

    public Label() {
      InitializeComponent();
      Counters.DataContext = this;
    }

    private void This_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
      if(e.OldValue is View.PityTimer) {
        View.PityTimer pt = (View.PityTimer)e.OldValue;

        pt.PropertyChanged -= Pt_PropertyChanged;
      }

      if(e.NewValue is View.PityTimer) {
        View.PityTimer pt = (View.PityTimer)e.NewValue;

        Current = pt.Current;
        pt.PropertyChanged += Pt_PropertyChanged;
      } else {
        Current = 0;
      }
    }

    private void Pt_PropertyChanged(object sender, PropertyChangedEventArgs e) {
      if(!(sender is View.PityTimer)) {
        return;
      }
      View.PityTimer pt = (View.PityTimer)sender;

      switch(e.PropertyName) {
        case "Current":
          Current = pt.Current;
          break;
        case "Average":
          break;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
  }
}
