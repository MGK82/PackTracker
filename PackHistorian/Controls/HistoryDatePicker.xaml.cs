using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PackChronicler.Controls {
  /// <summary>
  /// Interaktionslogik für HistoryDatePicker.xaml
  /// </summary>
  public partial class HistoryDatePicker : UserControl, INotifyPropertyChanged {
    PackChronicler.History _history;
    Dictionary<DateTime?, ObservableCollection<Entity.Pack>> _associatedPacks = new Dictionary<DateTime?, ObservableCollection<Entity.Pack>>();

    public ObservableCollection<Entity.Pack> AssociatedPack { get
      {
        DateTime? Selection = dp_DatePicker.SelectedDate;
        if(Selection == null) {
          return new ObservableCollection<Entity.Pack>();
        }

        if(!_associatedPacks.ContainsKey(Selection)) {
          _associatedPacks.Add(Selection, new ObservableCollection<Entity.Pack>(_history.Where(x => x.Time.Date == Selection)));
        }

        return _associatedPacks[Selection];
      }
    }

    public HistoryDatePicker(PackChronicler.History History) {
      InitializeComponent();
      _history = History;

      if(History.Count > 0) {
        InitializeCalender(History);
      } else {
        dp_DatePicker.DisplayDateStart = DateTime.Today;
        History.CollectionChanged += InitializeCalender;
      }

      dp_DatePicker.SelectedDateChanged += (sender, e) => OnPropertyChanged("AssociatedPack");
      History.CollectionChanged += (sender, e) => {
        if(e.Action == NotifyCollectionChangedAction.Add) {
          foreach(var Pack in e.NewItems) {
            if(Pack is Entity.Pack) {
              Entity.Pack NewPack = (Entity.Pack)Pack;
              if(_associatedPacks.ContainsKey(NewPack.Time.Date)) {
                _associatedPacks[NewPack.Time.Date].Add(NewPack);
              }

              if(dp_DatePicker.SelectedDate != NewPack.Time.Date) {
                dp_DatePicker.SelectedDate = NewPack.Time.Date;
              }
            }
          }
        }
      };
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    private void InitializeCalender(PackChronicler.History History) {
      Entity.Pack FirstPack = History.First();
      dp_DatePicker.DisplayDateStart = FirstPack.Time;
      dp_DatePicker.SelectedDate = History.Last().Time.Date;

      IEnumerable<DateTime> HistoryDates = History.Select(x => x.Time.Date).Distinct();
      for(DateTime i = FirstPack.Time.Date; i.Date < DateTime.Today; i = i.AddDays(1)) {
        if(!HistoryDates.Contains(i)) {
          dp_DatePicker.BlackoutDates.Add(new CalendarDateRange(i));
        }
      }
    }

    private void InitializeCalender(object sender, NotifyCollectionChangedEventArgs e) {
      if(e.Action == NotifyCollectionChangedAction.Add && e.NewItems.Count > 0 && sender is PackChronicler.History) {
        PackChronicler.History History = (PackChronicler.History)sender;
        InitializeCalender(History);
        History.CollectionChanged -= InitializeCalender;
      }
    }
  }
}
