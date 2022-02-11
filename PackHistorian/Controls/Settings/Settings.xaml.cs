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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PackTracker.Update;

namespace PackTracker.Controls.Settings {
  /// <summary>
  /// Interaktionslogik für Settings.xaml
  /// </summary>
  public partial class Settings {
    PackTracker.Settings _settings;
    bool allowClosing = false;

    public Settings(PackTracker.Settings Settings) {
      InitializeComponent();
      lb_tabs.ItemsSource = new List<ITitledElement>() {
        new General(Settings),
        new Update(Settings, new Updater()),
        new Credits(),
      };
      lb_tabs.SelectedIndex = 0;

      _settings = Settings;
      AnimateSizeToContentStart();
    }

    void AnimateSizeToContentStart() {
      SizeChanged += ChangeSize;
      SizeToContent = SizeToContent.Width;
    }

    void AnimateSizeToContentStop() {
      SizeChanged -= ChangeSize;
      SizeToContent = SizeToContent.Manual;
    }

    void ChangeSize(object sender, SizeChangedEventArgs e) {
      DoubleAnimation at = new DoubleAnimation(e.PreviousSize.Width, e.NewSize.Width, new Duration(new TimeSpan(6000000)));
      AnimateSizeToContentStop();
      at.EasingFunction = new ExponentialEase() { EasingMode = EasingMode.EaseInOut };
      at.Completed += (sender2, e2) => {
        AnimateSizeToContentStart();
      };

      BeginAnimation(WidthProperty, at);
    }

    private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
      if(allowClosing) {
        return;
      } else {
        e.Cancel = true;
      }

      AnimateSizeToContentStop();
      Duration Duration = new Duration(TimeSpan.FromSeconds(.4));
      IEasingFunction Easing = new ExponentialEase() { EasingMode = EasingMode.EaseInOut };

      DoubleAnimation Width = new DoubleAnimation(ActualWidth, 2, Duration) { EasingFunction = Easing };
      Width.Completed += (sender2, e2) => {
        DoubleAnimation Height = new DoubleAnimation(ActualHeight, 0, Duration) { EasingFunction = Easing };
        Height.Completed += (sender3, e3) => {
          allowClosing = true;
          Close();
        };

        BeginAnimation(HeightProperty, Height);
      };

      BeginAnimation(WidthProperty, Width);
    }
  }
}
