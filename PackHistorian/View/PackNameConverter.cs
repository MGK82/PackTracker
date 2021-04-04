using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using HearthDb.Enums;
using Hearthstone_Deck_Tracker;

namespace PackTracker.View {
  class PackNameConverter : IValueConverter {
    static Config _config = Config.Instance;
    static Dictionary<int, Dictionary<Locale, string>> PackNames = new Dictionary<int, Dictionary<Locale, string>>() {
      {
        1, new Dictionary<Locale, string>() {
          { Locale.enUS, "Classic" },
          { Locale.enGB, "Classic" },
          { Locale.deDE, "Klassik" },
          { Locale.zhCN, "经典" },
        }
      },
      {
        9, new Dictionary<Locale, string>() {
          { Locale.enUS, "Goblins vs Gnomes" },
          { Locale.enGB, "Goblins vs Gnomes" },
          { Locale.frFR, "Gobelins et Gnomes" },
          { Locale.deDE, "Goblins gegen Gnome" },
          { Locale.koKR, "고블린 대 노움" },
          { Locale.esES, "Goblins vs Gnomos" },
          { Locale.esMX, "Goblins vs Gnomos" },
          { Locale.ruRU, "Гоблины и гномы" },
          { Locale.zhTW, "《哥哥打地地》" },
          { Locale.zhCN, "地精大战侏儒" },
          { Locale.itIT, "Goblin vs Gnomi" },
          { Locale.ptBR, "Goblins vs Gnomos" },
          { Locale.plPL, "Gobliny vs Gnomy" },
          { Locale.ptPT, "Goblins vs Gnomos" },
          { Locale.jaJP, "ゴブリンvsノーム" },
          { Locale.thTH, "Goblins vs Gnomes" },
        }
      },
      {
        10, new Dictionary<Locale, string>() {
          { Locale.enUS, "The Grand Tournament" },
          { Locale.enGB, "The Grand Tournament" },
          { Locale.frFR, "Le Grand Tournoi" },
          { Locale.deDE, "Das Große Turnier" },
          { Locale.koKR, "대 마상시합" },
          { Locale.esES, "El Gran Torneo" },
          { Locale.esMX, "El Gran Torneo" },
          { Locale.ruRU, "Большой турнир" },
          { Locale.zhTW, "《銀白聯賽》" },
          { Locale.zhCN, "冠军的试炼" },
          { Locale.itIT, "Gran Torneo" },
          { Locale.ptBR, "O Grande Torneio" },
          { Locale.plPL, "Wielki Turniej" },
          { Locale.ptPT, "O Grande Torneio" },
          { Locale.jaJP, "グランド・トーナメント" },
          { Locale.thTH, "The Grand Tournament" },
        }
      },
      {
        11, new Dictionary<Locale, string>() {
          { Locale.enUS, "Whispers of the Old Gods" },
          { Locale.enGB, "Whispers of the Old Gods" },
          { Locale.frFR, "Les murmures des Dieux très anciens" },
          { Locale.deDE, "Das Flüstern der Alten Götter" },
          { Locale.koKR, "고대 신의 속삭임" },
          { Locale.esES, "Susurros de los Dioses Antiguos" },
          { Locale.esMX, "Susurros de los Dioses Antiguos" },
          { Locale.ruRU, "Пробуждение древних богов" },
          { Locale.zhTW, "《古神碎碎念》" },
          { Locale.zhCN, "上古之神的低语" },
          { Locale.itIT, "Sussurri degli Dei Antichi" },
          { Locale.ptBR, "Sussurros dos Deuses Antigos" },
          { Locale.plPL, "Przedwieczni Bogowie" },
          { Locale.ptPT, "Sussurros dos Deuses Antigos" },
          { Locale.jaJP, "旧神のささやき" },
          { Locale.thTH, "Whispers of the Old Gods" },
        }
      },
      {
        19, new Dictionary<Locale, string>() {
          { Locale.enUS, "Mean Streets of Gadgetzan" },
          { Locale.enGB, "Mean Streets of Gadgetzan" },
          { Locale.frFR, "Main basse sur Gadgetzan" },
          { Locale.deDE, "Die Straßen von Gadgetzan" },
          { Locale.koKR, "비열한 거리의 가젯잔" },
          { Locale.esES, "Mafias de Gadgetzan" },
          { Locale.esMX, "Mafias de Gadgetzan" },
          { Locale.ruRU, "Злачный город Прибамбасск" },
          { Locale.zhTW, "《黑街英雄之加基森風雲》" },
          { Locale.zhCN, "龙争虎斗加基森" },
          { Locale.itIT, "I bassifondi di Meccania" },
          { Locale.ptBR, "As Gangues de Geringontzan" },
          { Locale.plPL, "Ciemne zaułki Gadżetonu" },
          { Locale.ptPT, "As Gangues de Geringontzan" },
          { Locale.jaJP, "仁義なきガジェッツァン" },
          { Locale.thTH, "Mean Streets of Gadgetzan" },
        }
      },
      {
        20, new Dictionary<Locale, string>() {
          { Locale.enUS, "Journey to Un'Goro" },
          { Locale.enGB, "Journey to Un'Goro" },
          { Locale.frFR, "Voyage au centre d’Un’Goro" },
          { Locale.deDE, "Reise nach Un'Goro" },
          { Locale.koKR, "운고로를 향한 여정" },
          { Locale.esES, "Viaje a Un'Goro" },
          { Locale.esMX, "Viaje a Un'Goro" },
          { Locale.ruRU, "Экспедиция в Ун'Горо" },
          { Locale.zhTW, "《安戈洛歷險記》" },
          { Locale.zhCN, "勇闯安戈洛" },
          { Locale.itIT, "Viaggio a Un'Goro" },
          { Locale.ptBR, "Jornada a Un'Goro" },
          { Locale.plPL, "Podróż do wnętrza Un'Goro" },
          { Locale.ptPT, "Jornada a Un'Goro" },
          { Locale.jaJP, "大魔境ウンゴロ" },
          { Locale.thTH, "Journey to Un'Goro" },
        }
      },
      {
        21, new Dictionary<Locale, string>() {
          { Locale.enUS, "Knights of the Frozen Throne" },
          { Locale.enGB, "Knights of the Frozen Throne" },
          { Locale.frFR, "Chevaliers du Trône de glace" },
          { Locale.deDE, "Ritter des Frostthrons" },
          { Locale.koKR, "얼어붙은 왕좌의 기사들" },
          { Locale.esES, "Caballeros del Trono Helado" },
          { Locale.esMX, "Caballeros del Trono Helado" },
          { Locale.ruRU, "Рыцари Ледяного Трона" },
          { Locale.zhTW, "《冰封王座》" },
          { Locale.zhCN, "冰封王座的骑士" },
          { Locale.itIT, "Cavalieri del Trono di Ghiaccio" },
          { Locale.ptBR, "Cavaleiros do Trono Gélido" },
          { Locale.plPL, "Rycerze Mroźnego Tronu" },
          { Locale.ptPT, "Cavaleiros do Trono Gélido" },
          { Locale.jaJP, "凍てつく玉座の騎士団" },
          { Locale.thTH, "Knights of the Frozen Throne" },
        }
      },
      {
        23, new Dictionary<Locale, string>() {
          { Locale.enUS, "Classic (Gold)" },
          { Locale.enGB, "Classic  (Gold)" },
          { Locale.deDE, "Klassik  (Gold)" },
          { Locale.zhCN, "经典（金色）" },
        }
      },
      {
        30, new Dictionary<Locale, string>() {
          { Locale.enUS, "Kobolds and Catacombs" },
          { Locale.enGB, "Kobolds and Catacombs" },
          { Locale.frFR, "Kobolds et Catacombes" },
          { Locale.deDE, "Kobolde & Katakomben" },
          { Locale.koKR, "코볼트와 지하 미궁" },
          { Locale.esES, "Kóbolds & Catacumbas" },
          { Locale.esMX, "Kóbolds & Catacumbas" },
          { Locale.ruRU, "Кобольды и катакомбы" },
          { Locale.zhTW, "《狗頭人與地下城》" },
          { Locale.zhCN, "狗头人与地下世界" },
          { Locale.itIT, "Coboldi & Catacombe" },
          { Locale.ptBR, "Kobolds & Catacumbas" },
          { Locale.plPL, "Koboldy i katakumby" },
          { Locale.ptPT, "Kobolds & Catacumbas" },
          { Locale.jaJP, "コボルトと秘宝の迷宮" },
          { Locale.thTH, "Kobolds and Catacombs" }
        }
      },
      {
        31, new Dictionary<Locale, string>() {
          { Locale.enUS, "The Witchwood" },
          { Locale.enGB, "The Witchwood" },
          { Locale.frFR, "Le Bois Maudit" },
          { Locale.deDE, "Der Hexenwald" },
          { Locale.koKR, "마녀숲" },
          { Locale.esES, "El Bosque Embrujado" },
          { Locale.esMX, "El Bosque Embrujado" },
          { Locale.ruRU, "Ведьмин лес" },
          { Locale.zhTW, "《黑巫森林》" },
          { Locale.zhCN, "女巫森林" },
          { Locale.itIT, "Boscotetro" },
          { Locale.ptBR, "O Bosque das Bruxas" },
          { Locale.plPL, "Wiedźmi Las" },
          { Locale.ptPT, "O Bosque das Bruxas" },
          { Locale.jaJP, "妖の森ウィッチウッド" },
          { Locale.thTH, "The Witchwood" }
        }
      },
      {
        38, new Dictionary<Locale, string>() {
          { Locale.enUS, "The Boomsday Project" },
          { Locale.enGB, "The Boomsday Project" },
          { Locale.frFR, "Projet Armageboum" },
          { Locale.deDE, "Dr. Bumms Geheimlabor" },
          { Locale.koKR, "박사 붐의 폭심만만 프로젝트" },
          { Locale.esES, "El Proyecto Armagebum" },
          { Locale.esMX, "El Proyecto K-Bum" },
          { Locale.ruRU, "Проект Бумного дня" },
          { Locale.zhTW, "《爆爆計畫》" },
          { Locale.zhCN, "砰砰计划" },
          { Locale.itIT, "Operazione Apocalisse" },
          { Locale.ptBR, "O Projeto Cabum" },
          { Locale.plPL, "Projekt Hukatomba" },
          { Locale.ptPT, "O Projeto Cabum" },
          { Locale.jaJP, "博士のメカメカ大作戦" },
          { Locale.thTH, "The Boomsday Project" },
        }
      },
      {
        40, new Dictionary<Locale, string>() {
          { Locale.enUS, "Rastakhan's Rumble" },
          { Locale.enGB, "Rastakhan's Rumble" },
          { Locale.frFR, "Les Jeux de Rastakhan" },
          { Locale.deDE, "Rastakhans Rambazamba" },
          { Locale.koKR, "라스타칸의 대난투" },
          { Locale.esES, "La Arena de Rastakhan" },
          { Locale.esMX, "El Reto de Rastakhan" },
          { Locale.ruRU, "Растахановы игрища" },
          { Locale.zhTW, "《拉斯塔哈大混戰》" },
          { Locale.zhCN, "拉斯塔哈的大乱斗" },
          { Locale.itIT, "La sfida di Rastakhan" },
          { Locale.ptBR, "Ringue do Rastakhan" },
          { Locale.plPL, "Rozróba Rastakana" },
          { Locale.ptPT, "Ringue do Rastakhan" },
          { Locale.jaJP, "天下一ヴドゥ祭" },
          { Locale.thTH, "Rastakhan's Rumble" },
        }
      },
      {
        49, new Dictionary<Locale, string>() {
          { Locale.enUS, "Rise of Shadows" },
          { Locale.enGB, "Rise of Shadows" },
          { Locale.frFR, "Éveil des ombres" },
          { Locale.deDE, "Verschwörung der Schatten" },
          { Locale.koKR, "어둠의 반격" },
          { Locale.esES, "El Auge de las Sombras" },
          { Locale.esMX, "Ascenso de las Sombras" },
          { Locale.ruRU, "Возмездие теней" },
          { Locale.zhTW, "《反派大進擊》" },
          { Locale.zhCN, "暗影崛起" },
          { Locale.itIT, "L'ascesa delle ombre" },
          { Locale.ptBR, "Ascensão das Sombras" },
          { Locale. plPL, "Wyjście z cienia" },
          { Locale.ptPT, "Ascensão das Sombras" },
          { Locale.jaJP, "爆誕！悪党同盟" },
          { Locale.thTH, "Rise of Shadows" },
        }
      },
      {
        128, new Dictionary<Locale, string>() {
          { Locale.enUS, "Saviors of Uldum" },
          { Locale.enGB, "Saviors of Uldum" },
          { Locale.frFR, "Les Aventuriers d’Uldum" },
          { Locale.deDE, "Retter von Uldum" },
          { Locale.koKR, "울둠의 구원자" },
          { Locale.esES, "Salvadores de Uldum" },
          { Locale.esMX, "Defensores de Uldum" },
          { Locale.ruRU, "Спасители Ульдума" },
          { Locale.zhTW, "《奧丹姆守護者》" },
          { Locale.zhCN, "奧丹姆奇兵" },
          { Locale.itIT, "Salvatori di Uldum" },
          { Locale.ptBR, "Salvadores de Uldum" },
          { Locale. plPL, "Wybawcy Uldum" },
          { Locale.ptPT, "Salvadores de Uldum" },
          { Locale.jaJP, "突撃！探検同盟" },
          { Locale.thTH, "Saviors of Uldum" },
        }
      },
      {
        347, new Dictionary<Locale, string>() {
          { Locale.enUS, "Descent of Dragons" },
          { Locale.enGB, "Descent of Dragons" },
          { Locale.frFR, "L’Envol des Dragons" },
          { Locale.deDE, "Erbe der Drachen" },
          { Locale.koKR, "용의 강림" },
          { Locale.esES, "El Descenso de los Dragones" },
          { Locale.esMX, "Descenso de los Dragones" },
          { Locale.ruRU, "Натиск драконов" },
          { Locale.zhTW, "《降臨！遠古巨龍》" },
          { Locale.zhCN, "巨龙降临" },
          { Locale.itIT, "La Discesa dei Draghi" },
          { Locale.ptBR, "Despontar dos Dragões" },
          { Locale.plPL, "Wejście smoków" },
          { Locale.ptPT, "Despontar dos Dragões" },
          { Locale.jaJP, "激闘！ドラゴン大決戦" },
          { Locale.thTH, "Descent of Dragons" },
        }
      },
      {
        423, new Dictionary<Locale, string>() {
          { Locale.enUS, "Ashes of Outland" },
          { Locale.enGB, "Ashes of Outland" },
          { Locale.frFR, "Les Cendres de l’Outreterre" },
          { Locale.deDE, "Ruinen der Scherbenwelt" },
          { Locale.koKR, "황폐한 아웃랜드" },
          { Locale.esES, "Cenizas de Terrallende" },
          { Locale.esMX, "Cenizas de Terrallende" },
          { Locale.ruRU, "Руины Запределья" },
          { Locale.zhTW, "《外域之燼》" },
          { Locale.zhCN, "伊利丹的崛起" },
          { Locale.itIT, "Ceneri delle Terre Esterne" },
          { Locale.ptBR, "Cinzas de Terralém" },
          { Locale.plPL, "Popioły Rubieży" },
          { Locale.ptPT, "Cinzas de Terralém" },
          { Locale.jaJP, "灰に舞う降魔の狩人" },
          { Locale.thTH, "Ashes of Outland" },
        }
      },
      {
        468, new Dictionary<Locale, string>() {
          { Locale.enUS, "Scholomance Academy" },
          { Locale.enGB, "Scholomance Academy" },
          { Locale.frFR, "L’Académie Scholomance" },
          { Locale.deDE, "Akademie Scholomance" },
          { Locale.koKR, "스칼로맨스 아카데미" },
          { Locale.esES, "Academia Scholomance" },
          { Locale.esMX, "Academia Scholomance" },
          { Locale.ruRU, "Некроситет" },
          { Locale.zhTW, "通靈學院" },
          { Locale.zhCN, "通灵学园" },
          { Locale.itIT, "L'Accademia di Scholomance" },
          { Locale.ptBR, "Universidade de Scolomântia" },
          { Locale.plPL, "Scholomancjum" },
          { Locale.ptPT, "Universidade de Scolomântia" },
          { Locale.jaJP, "魔法学院スクロマンス" },
          { Locale.thTH, "Scholomance Academy" },
        }
      },
      {
        470, new Dictionary<Locale, string>() {
          { Locale.enUS, "Golden Pack" },
          { Locale.enGB, "Golden Pack" },
          { Locale.frFR, "Golden Pack" },
          { Locale.deDE, "Golden Pack" },
          { Locale.koKR, "Golden Pack" },
          { Locale.esES, "Golden Pack" },
          { Locale.esMX, "Golden Pack" },
          { Locale.ruRU, "Golden Pack" },
          { Locale.zhTW, "Golden Pack" },
          { Locale.zhCN, "Golden Pack" },
          { Locale.itIT, "Golden Pack" },
          { Locale.ptBR, "Golden Pack" },
          { Locale.plPL, "Golden Pack" },
          { Locale.ptPT, "Golden Pack" },
          { Locale.jaJP, "Golden Pack" },
          { Locale.thTH, "Golden Pack" },
        }
      },
      {
        616, new Dictionary<Locale, string>() {
          { Locale.enUS, "Madness at the Darkmoon Faire" },
          { Locale.enGB, "Madness at the Darkmoon Faire" },
          { Locale.frFR, "Folle journée à Sombrelune" },
          { Locale.deDE, "Der Dunkelmond-Wahnsinn" },
          { Locale.koKR, "광기의 다크문 축제" },
          { Locale.esES, "Locura en la Feria de la Luna Negra" },
          { Locale.esMX, "Locura en la Feria de la Luna Negra" },
          { Locale.ruRU, "Ярмарка безумия" },
          { Locale.zhTW, "《暗月馬戲團：古神也瘋狂》" },
          { Locale.zhCN, "疯狂的暗月马戏团" },
          { Locale.itIT, "Follia alla Fiera di Lunacupa" },
          { Locale.ptBR, "Delírios em Negraluna" },
          { Locale.ptPT, "Delírios em Negraluna" },
          { Locale.plPL, "Obłędny Festyn Lunomroku" },
          { Locale.jaJP, "ダークムーン・フェアへの招待状" },
          { Locale.thTH, "Madness at the Darkmoon Faire" },
        }
      },
      {
        1525, new Dictionary<Locale, string>() {
          { Locale.enUS, "Forged In The Barrens" },
          { Locale.enGB, "Forged In The Barrens" },
          { Locale.frFR, "Forgés dans les Tarides" },
          { Locale.deDE, "Geschmiedet im Brachland" },
          { Locale.koKR, "불모의 땅" },
          { Locale.esES, "Forjados en los Baldíos" },
          { Locale.esMX, "Forjados en Los Baldíos" },
          { Locale.ruRU, "Закаленные Степями" },
          { Locale.zhTW, "貧瘠之地" },
          { Locale.zhCN, "贫瘠之地的锤炼" },
          { Locale.itIT, "Forgiati nelle Savane" },
          { Locale.ptBR, "Forjado nos Sertões" },
          { Locale.ptPT, "Forjado nos Sertões" },
          { Locale.plPL, "Zahartowani przez Pustkowia" },
          { Locale.jaJP, "荒ぶる大地の強者たち" },
          { Locale.thTH, "Forged In The Barrens" },
        }
      },
    };

    public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture) {
      if(value == null) {
        return "";
      }

      if(int.TryParse(value.ToString(), out int id)) {
        if(Enum.TryParse(_config.SelectedLanguage, out Locale lang)) {
          string converted = Convert(id, lang);
          if(!string.IsNullOrEmpty(converted)) {
            return converted;
          }
        }

        if(PackNames.ContainsKey(id)) {
          if(PackNames[id].ContainsKey(Locale.enUS)) {
            return PackNames[id][Locale.enUS];
          }
        }
      }

      return value;
    }

    public static string Convert(int packId, Locale lang) {
      if(PackNames.ContainsKey(packId)) {
        if(PackNames[packId].ContainsKey(lang)) {
          return PackNames[packId][lang];
        }
      }

      return null;
    }

    public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }
  }
}
