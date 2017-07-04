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
    Timer _timer;

    public string Title => "Update";

    public Update(Updater Updater) {
      InitializeComponent();

      _updater = Updater;
      _timer = new Timer((new TimeSpan(0, 0, 10)).TotalMilliseconds) { AutoReset = false };
      _timer.Elapsed += (sender, e) => { Dispatcher.Invoke(() => btn_Refresh.IsEnabled = true); };

      Loaded += Update_Loaded;
    }

    private void Update_Loaded(object sender, RoutedEventArgs e) {
      Loaded -= Update_Loaded;
      Refresh();
    }

    void Refresh() {
      btn_Refresh.IsEnabled = false;
      btn_Update.IsEnabled = false;
      pb_Bar.Visibility = Visibility.Visible;

      txt_ChangeLog.Inlines.Clear();
      BackgroundWorker bw = new BackgroundWorker();

      bw.DoWork += (sender, e) => {
        e.Result = _updater.GetAllReleases();
      };

      bw.RunWorkerCompleted += (sender, e) => {
        if(e.Result is IEnumerable<Release>) {
          IEnumerable<Release> Result = (IEnumerable<Release>)e.Result;
          InsertInlines(Result, txt_ChangeLog.Inlines);
          btn_Update.IsEnabled = Result.Any(x => Updater.ParseVersion(x.tag_name) > Plugin.CurrentVersion);
        } else {
          MessageBox.Show("Request failed", "Update", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        pb_Bar.Visibility = Visibility.Hidden;
        _timer.Start();
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

    private void btn_Update_Click(object sender, RoutedEventArgs e) {
      BackgroundWorker bw = new BackgroundWorker();
      bw.DoWork += (bwsender, bwe) => {
        bwe.Result = _updater.Update();
      };
      bw.RunWorkerCompleted += (bwsender, bwe) => {
        pb_Bar.Visibility = Visibility.Hidden;

        if((bool)bwe.Result) {
          MessageBox.Show("Update completed\nPlease restart Hearthstone Deck Tracker", "Pack Tracker: Update");
        } else {
          btn_Refresh.IsEnabled = true;
          btn_Refresh.IsEnabled = true;
          MessageBox.Show("Update failed\nPlease try again later or download on Github", "Pack Tracker: Update");
        }
      };

      btn_Refresh.IsEnabled = false;
      btn_Update.IsEnabled = false;
      pb_Bar.Visibility = Visibility.Visible;
      _timer.Stop();
      bw.RunWorkerAsync();
    }
  }
}
