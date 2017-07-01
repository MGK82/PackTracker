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
using PackTracker.Entity;

namespace PackTracker.Controls {
  /// <summary>
  /// Interaktionslogik für Toast.xaml
  /// </summary>
  public partial class Toast : UserControl {
    public Toast(Pack Pack, View.Average Average) {
      InitializeComponent();
      ctr_Cards.DataContext = Pack.Cards;
      ctr_Average.DataContext = Average;
    }
  }
}
