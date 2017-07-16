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

namespace PackTracker.Controls {
  /// <summary>
  /// Interaktionslogik für Search.xaml
  /// </summary>
  public partial class Search {
    IndexRepository _index;

    public Search(PackTracker.History History) {
      InitializeComponent();

      _index = new IndexRepository(History);
      txt_Search.Focus();
    }

    private void txt_Search_KeyDown(object sender, KeyEventArgs e) {
      if(e.Key == Key.Return) {
        dg_Result.ItemsSource = _index.Find(((TextBox)sender).Text);
        txt_Search.SelectAll();
      }
    }

    private void btn_Search_Click(object sender, RoutedEventArgs e) {
      dg_Result.ItemsSource = _index.Find(txt_Search.Text);
      txt_Search.Focus();
      txt_Search.SelectAll();
    }

    private void MetroWindow_KeyDown(object sender, KeyEventArgs e) {
      if(e.Key == Key.Escape) {
        Close();
      }
    }
  }
}
