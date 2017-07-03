using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PackTracker.Update {
  [DataContract]
  public class Release {
    [DataMember]
    public string tag_name;

    [DataMember]
    public List<Asset> assets;

    [DataMember]
    public string body;
  }
}
