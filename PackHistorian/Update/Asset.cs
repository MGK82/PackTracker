using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PackTracker.Update {
  [DataContract]
  class Asset {
    [DataMember]
    public string name;

    [DataMember]
    public string browser_download_url;
  }
}
