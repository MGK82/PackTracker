using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hearthstone_Deck_Tracker;

namespace PackTracker.Update {
  class Updater {
    public Version CurrentVersion { get => Assembly.GetAssembly(typeof(Plugin)).GetName().Version; }

    public bool? NewVersionAvailable(out Release LatestRelease) {
      LatestRelease = null as Release;
      try {
        LatestRelease = GetLatestRelease();
      }
      catch(WebException) {
        return null;
      }

      Version LatestVersion = new Version(Regex.Match(LatestRelease.tag_name, @"\d+(\.\d+)*").ToString());

      return CurrentVersion.CompareTo(LatestVersion) < 0;
    }

    public bool Update() {
      Release LatestRelease = GetLatestRelease();
      Asset Asset = LatestRelease.assets.Single(x => x.name == "PackTracker.zip");

      try {
        using(WebClient client = new WebClient()) {
          using(Stream download = client.OpenRead(Asset.browser_download_url)) {
            string path = Path.Combine(Config.AppDataPath, "Plugins");

            ZipArchive Zipper = new ZipArchive(download);
            Zipper.ExtractToDirectory(path);

            return true;
          }
        }
      }
      catch(WebException) {
        return false;
      }
    }

    public Release GetLatestRelease() {
      HttpWebRequest request = WebRequest.CreateHttp(@"https://api.github.com/repos/mgk82/PackTracker/releases/latest");
      request.Proxy = null;
      request.UserAgent = "PackTracker";

      Release Release = new Release();
      using(Stream response = request.GetResponse().GetResponseStream()) {
        DataContractJsonSerializer ser = new DataContractJsonSerializer(Release.GetType());
        Release = (Release)ser.ReadObject(response);
      }

      return Release;
    }
  }
}
