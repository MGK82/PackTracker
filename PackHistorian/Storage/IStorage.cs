using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackTracker.Storage {
  interface IStorage {
    History Fetch();
    void Store(History History);
  }
}
