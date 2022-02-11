using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackTracker.Storage {
  interface ISettingsStorage {
    Settings Fetch();
    void Store(Settings History);
  }
}
