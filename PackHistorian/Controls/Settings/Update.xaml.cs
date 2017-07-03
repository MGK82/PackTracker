using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;
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
using PackTracker.Update;

namespace PackTracker.Controls.Settings {
  /// <summary>
  /// Interaktionslogik für Update.xaml
  /// </summary>
  public partial class Update : UserControl, ITitledElement {
    Updater _updater;

    public string Title => "Update";

    public Update(Updater Updater) {
      InitializeComponent();

      _updater = Updater;

      Loaded += Update_Loaded;
    }

    private void Update_Loaded(object sender, RoutedEventArgs e) {
      Loaded -= Update_Loaded;
      Refresh();
    }

    void Refresh() {
      btn_Refresh.IsEnabled = false;
      pb_Bar.Visibility = Visibility.Visible;

      txt_ChangeLog.Inlines.Clear();
      BackgroundWorker bw = new BackgroundWorker();

      bw.DoWork += (sender, e) => {
        e.Result = _updater.GetAllReleases();
      };

      bw.RunWorkerCompleted += (sender, e) => {
        if(e.Result is IEnumerable<Release>) {
          InsertInlines((IEnumerable<Release>)e.Result, txt_ChangeLog.Inlines);
        } else {
          MessageBox.Show("Request failed", "Update", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        pb_Bar.Visibility = Visibility.Hidden;
        (new Timer((new TimeSpan(0, 0, 10)).TotalMilliseconds) { AutoReset = false, Enabled = true,})
          .Elapsed += (timer, e2) => { Dispatcher.Invoke(() => btn_Refresh.IsEnabled = true); ((Timer)timer).Dispose(); };
      };

      bw.RunWorkerAsync();
    }

    void InsertInlines(IEnumerable<Release> Releases, InlineCollection Target) {
      foreach(Release Release in Releases) {
        Run Headline = new Run(Release.tag_name) {
          FontStyle = FontStyles.Oblique,
          FontWeight = FontWeights.Bold,
          Foreground = Brushes.CornflowerBlue,
          TextDecorations = TextDecorations.Underline,
          FontSize = 18,
        };
        Target.Add(Headline);

        if(Plugin.CurrentVersion == Updater.ParseVersion(Release.tag_name)) {
          Run Installed = new Run(" (installed)\n") {
            FontStyle = FontStyles.Oblique,
            Foreground = Brushes.White,
            FontSize = 10,
          };
          Target.Add(Installed);
        } else {
          Headline.Text += "\n";
        }

        Run Body = new Run(Release.body + "\n\n") {
          Foreground = Brushes.White,
        };
        Target.Add(Body);
      }
    }

    private void btn_Refresh_Click(object sender, RoutedEventArgs e) {
      Refresh();
    }
  }
}
