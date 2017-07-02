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
using System.Windows.Shapes;

namespace PackTracker.Controls.Settings {
  /// <summary>
  /// Interaktionslogik für Settings.xaml
  /// </summary>
  public partial class Settings {
    public Settings() {
      InitializeComponent();
      lb_tabs.ItemsSource = new List<ITitledElement>() {
        new Credits(),
      };
      lb_tabs.SelectedIndex = 0;
    }
  }
}
