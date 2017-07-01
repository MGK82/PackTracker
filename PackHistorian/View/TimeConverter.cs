using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackTracker.View {
  class TimeConverter : DateTimeConverter {
    protected override string _format { get => "T"; }
  }
}
