﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackTracker {
  public class Settings {
    public bool Spoil { get; set; } = false;
    public bool PityOverlay { get; set; } = true;
    public bool Update { get; set; } = true;
  }
}
