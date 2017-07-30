using MahApps.Metro.Controls;
using PackTracker.Entity;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace PackTracker.Controls {
  public partial class PackDropDown : SplitButton {
    private ObservableCollection<int> _dropDown;

    public PackDropDown() {
      InitializeComponent();

      _dropDown = new ObservableCollection<int>();
    }

    private void dd_Packs_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
      _dropDown.Clear();

      if(e.NewValue is PackTracker.History) {
        PackTracker.History History = (PackTracker.History)e.NewValue;
        _dropDown = new ObservableCollection<int>(History.Select(x => x.Id).Distinct().OrderBy(x => x));
        dd_Packs.ItemsSource = _dropDown;
        History.CollectionChanged += DropDown_NewEntry;
      }
      if(e.OldValue is PackTracker.History) {
        ((PackTracker.History)e.OldValue).CollectionChanged -= DropDown_NewEntry;
      }

      if(_dropDown.Count > 0) {
        dd_Packs.SelectedIndex = -1;
        dd_Packs.SelectedIndex = 0;
      }
    }

    private void DropDown_NewEntry(object sender, NotifyCollectionChangedEventArgs e) {
      if(e.Action == NotifyCollectionChangedAction.Add) {
        foreach(Pack newPack in e.NewItems) {
          if(!_dropDown.Contains(newPack.Id)) {
            bool isInserted = false;

            foreach(int id in _dropDown) {
              if(newPack.Id < id) {
                _dropDown.Insert(_dropDown.IndexOf(id), newPack.Id);
                isInserted = true;
                break;
              }
            }

            if(!isInserted) {
              _dropDown.Add(newPack.Id);
            }
          }

          dd_Packs.SelectedItem = newPack.Id;
        }
      }
    }

    private void dd_Packs_MouseWheel(object sender, MouseWheelEventArgs e) {
      if(e.Delta > 0) {
        if(dd_Packs.SelectedIndex > 0) {
          dd_Packs.SelectedIndex--;
        }
      } else {
        if(dd_Packs.SelectedIndex < dd_Packs.Items.Count - 1) {
          dd_Packs.SelectedIndex++;
        }
      }
    }

    private void dd_Packs_Click(object sender, RoutedEventArgs e) {
      dd_Packs.IsExpanded = !dd_Packs.IsExpanded;
    }
  }
}
