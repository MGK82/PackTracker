using System.Collections.Generic;
using HearthDb.Enums;

namespace PackTracker
{
    class SetProvider
    {
        public static Dictionary<int, CardSet> PackSets = new Dictionary<int, CardSet>() {
      {
          // Classic
        1, CardSet.EXPERT1
      },
      {
          // Goblins vs Gnomes
        9, CardSet.GVG
      },
      {
          // The Grand Tournament
        10, CardSet.TGT
      },
      {
          // Whispers of the Old Gods
        11, CardSet.OG
      },
      {
          // Mean Streets of Gadgetzan
        19, CardSet.GANGS
      },
      {
          // Journey to Un'Goro
        20, CardSet.UNGORO
      },
      {
          // Knights of the Frozen Throne
        21, CardSet.ICECROWN
      },
      {
          // TODO: Classic Gold
        23, CardSet.EXPERT1
      },
      {
          // Kobolds and Catacombs
        30, CardSet.LOOTAPALOOZA
      },
      {
          // The Witchwood
        31, CardSet.GILNEAS
      },
      {
          // The Boomsday Project
        38, CardSet.BOOMSDAY
      },
      {
          // Rastakhan's Rumble
        40, CardSet.TROLL
      },
      {
          // Rise of Shadows
        49, CardSet.DALARAN
      },
      {
          // Saviors of Uldum
        128, CardSet.ULDUM
      },
      {
          // Decent of Dragons
        347, CardSet.DRAGONS
      },
    };
    }
}
