using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PackTracker.Controls.PityTimer {
  interface IBarChart : INotifyPropertyChanged {
    Brush Fill { set; }
    int Threshold { get; set; }
    int? Average { get; }
    string XTitle { get; set; }
    string YTitle { get; set; }

    //get this from ContentControl
    object DataContext { get; set; }
    event DependencyPropertyChangedEventHandler DataContextChanged;
  }
}
