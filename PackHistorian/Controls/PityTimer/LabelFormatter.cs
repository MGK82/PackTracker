using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackTracker.Controls.PityTimer {
  static class LabelFormatter {
    public static Func<double, string> PlusOne => val => (val + 1).ToString();
  }
}
