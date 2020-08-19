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
          { Locale.deDE, "Klassik" },
          { Locale.esES, "Clásico" },
          { Locale.esMX, "Clásico" },
          { Locale.frFR, "Classique" },
          { Locale.itIT, "Classiche" },
          { Locale.jaJP, "クラシック" },
          { Locale.koKR, "오리지널" },
          { Locale.plPL, "Klasyczne" },
          { Locale.ptBR, "Clássico" },
          { Locale.ruRU, "Классический набор" },
          { Locale.thTH, "คลาสสิค" },
          { Locale.zhCN, "经典" },
          { Locale.zhTW, "經典" }
        }
      },
      {
        9, new Dictionary<Locale, string>() {
          { Locale.enUS, "Goblins vs Gnomes" },
          { Locale.deDE, "Goblins gegen Gnome" },
          { Locale.esES, "Goblins vs. Gnomos" },
          { Locale.esMX, "Goblins versus Gnomos" },
          { Locale.frFR, "Gobelins et Gnomes" },
          { Locale.itIT, "Goblin vs Gnomi" },
          { Locale.jaJP, "ゴブリンvsノーム" },
          { Locale.koKR, "고블린 대 노움" },
          { Locale.plPL, "Gobliny vs Gnomy" },
          { Locale.ptBR, "Goblins vs Gnomos" },
          { Locale.ruRU, "Гоблины и гномы" },
          { Locale.thTH, "Goblins vs Gnomes" },
          { Locale.zhCN, "地精大战侏儒" },
          { Locale.zhTW, "哥哥打地地" }
        }
      },
      {
        10, new Dictionary<Locale, string>() {
          { Locale.enUS, "The Grand Tournament" },
          { Locale.deDE, "Das Große Turnier" },
          { Locale.esES, "El Gran Torneo" },
          { Locale.esMX, "El Gran Torneo" },
          { Locale.frFR, "Le Grand Tournoi" },
          { Locale.itIT, "Gran Torneo" },
          { Locale.jaJP, "グランド・トーナメント" },
          { Locale.koKR, "대 마상시합" },
          { Locale.plPL, "Wielki Turniej" },
          { Locale.ptBR, "O Grande Torneio" },
          { Locale.ruRU, "Большой турнир" },
          { Locale.thTH, "The Grand Tournament" },
          { Locale.zhCN, "冠军的试炼" },
          { Locale.zhTW, "銀白聯賽" }
        }
      },
      {
        11, new Dictionary<Locale, string>() {
          { Locale.enUS, "Whispers of the Old Gods" },
          { Locale.deDE, "Das Flüstern der Alten Götter" },
          { Locale.esES, "Susurros de los Dioses Antiguos" },
          { Locale.esMX, "Susurros de los Dioses Antiguos" },
          { Locale.frFR, "Murmures des Dieux très anciens" },
          { Locale.itIT, "Sussurri degli Dei Antichi" },
          { Locale.jaJP, "旧神のささやき" },
          { Locale.koKR, "고대 신의 속삭임" },
          { Locale.plPL, "Przedwieczni Bogowie" },
          { Locale.ptBR, "Sussurros dos Deuses Antigos" },
          { Locale.ruRU, "Пробуждение древних богов" },
          { Locale.thTH, "Whispers of the Old Gods" },
          { Locale.zhCN, "上古之神的低语" },
          { Locale.zhTW, "古神碎碎念" }
        }
      },
      {
        17, new Dictionary<Locale, string>() {
          { Locale.enUS, "Welcome Bundle" },
          { Locale.deDE, "Willkommens[d]paket" },
          { Locale.esES, "Pack de bienvenida" },
          { Locale.esMX, "Combo de bienvenida" },
          { Locale.frFR, "Pack de bienvenue" },
          { Locale.itIT, "Pacchetto di Benvenuto" },
          { Locale.jaJP, "歓迎バンドル" },
          { Locale.koKR, "여관주인의 환영 상품: " },
          { Locale.plPL, "Zestaw powitalny" },
          { Locale.ptBR, "Pacote de Boas-vindas" },
          { Locale.ruRU, "Стартовый пакет" },
          { Locale.thTH, "ชุดต้อนรับ" },
          { Locale.zhCN, "迎新合集" },
          { Locale.zhTW, "超值禮盒" }
        }
      },
      {
        18, new Dictionary<Locale, string>() {
          { Locale.enUS, "Classic" },
          { Locale.deDE, "Klassik" },
          { Locale.esES, "Clásico" },
          { Locale.esMX, "Clásico" },
          { Locale.frFR, "Classique" },
          { Locale.itIT, "Classiche" },
          { Locale.jaJP, "クラシック" },
          { Locale.koKR, "오리지널" },
          { Locale.plPL, "Klasyczne" },
          { Locale.ptBR, "Clássico" },
          { Locale.ruRU, "Классический набор" },
          { Locale.thTH, "คลาสสิค" },
          { Locale.zhCN, "经典" },
          { Locale.zhTW, "經典" }
        }
      },
      {
        19, new Dictionary<Locale, string>() {
          { Locale.enUS, "Mean Streets of Gadgetzan" },
          { Locale.deDE, "Die Straßen von Gadgetzan" },
          { Locale.esES, "Mafias de Gadgetzan" },
          { Locale.esMX, "Los Callejones de Gadgetzan" },
          { Locale.frFR, "Main basse sur Gadgetzan" },
          { Locale.itIT, "Bassifondi di Meccania" },
          { Locale.jaJP, "仁義なきガジェッツァン" },
          { Locale.koKR, "비열한 거리의 가젯잔" },
          { Locale.plPL, "Ciemne zaułki Gadżetonu" },
          { Locale.ptBR, "Gangues de Geringontzan" },
          { Locale.ruRU, "Злачный город Прибамбасск" },
          { Locale.thTH, "Mean Streets of Gadgetzan" },
          { Locale.zhCN, "龙争虎斗加基森" },
          { Locale.zhTW, "加基森風雲" }
        }
      },
      {
        20, new Dictionary<Locale, string>() {
          { Locale.enUS, "Journey to Un'Goro" },
          { Locale.deDE, "Reise nach Un’Goro" },
          { Locale.esES, "Viaje a Un'Goro" },
          { Locale.esMX, "Viaje a Un'Goro" },
          { Locale.frFR, "Voyage au centre d’Un’Goro" },
          { Locale.itIT, "Viaggio a Un'Goro" },
          { Locale.jaJP, "大魔境ウンゴロ" },
          { Locale.koKR, "운고로를 향한 여정" },
          { Locale.plPL, "Podróż do wnętrza Un'Goro" },
          { Locale.ptBR, "Jornada a Un'Goro" },
          { Locale.ruRU, "Экспедиция в Ун'Горо" },
          { Locale.thTH, "Journey to Un'Goro" },
          { Locale.zhCN, "勇闯安戈洛" },
          { Locale.zhTW, "安戈洛歷險記" }
        }
      },
      {
        21, new Dictionary<Locale, string>() {
          { Locale.enUS, "Knights of the Frozen Throne" },
          { Locale.deDE, "Ritter des Frostthrons" },
          { Locale.esES, "Caballeros del Trono Helado" },
          { Locale.esMX, "Caballeros del Trono Helado" },
          { Locale.frFR, "Chevaliers du Trône de glace" },
          { Locale.itIT, "Cavalieri del Trono di Ghiaccio" },
          { Locale.jaJP, "凍てつく玉座の騎士団" },
          { Locale.koKR, "얼어붙은 왕좌의 기사들" },
          { Locale.plPL, "Rycerze Mroźnego Tronu" },
          { Locale.ptBR, "Cavaleiros do Trono de Gelo" },
          { Locale.ruRU, "Рыцари Ледяного Трона" },
          { Locale.thTH, "Knights of the Frozen Throne" },
          { Locale.zhCN, "冰封王座的骑士" },
          { Locale.zhTW, "冰封王座" }
        }
      },
      {
        23, new Dictionary<Locale, string>() {
          { Locale.enUS, "Golden Classic" },
          { Locale.deDE, "Klassik (Golden)" },
          { Locale.esES, "Clásica dorada" },
          { Locale.esMX, "Clásica dorada" },
          { Locale.frFR, "Classique dorée" },
          { Locale.itIT, "Classiche Dorate" },
          { Locale.jaJP, "ゴールデンクラシック" },
          { Locale.koKR, "황금 오리지널" },
          { Locale.plPL, "Złote klasyczne" },
          { Locale.ptBR, "Clássico Dourado" },
          { Locale.ruRU, "Золотая классика" },
          { Locale.thTH, "คลาสสิคสีทอง" },
          { Locale.zhCN, "金色经典" },
          { Locale.zhTW, "經典金卡" }
        }
      },
      {
        30, new Dictionary<Locale, string>() {
          { Locale.enUS, "Kobolds & Catacombs" },
          { Locale.deDE, "Kobolde & Katakomben" },
          { Locale.esES, "Kóbolds & Catacumbas" },
          { Locale.esMX, "Kóbolds & Catacumbas" },
          { Locale.frFR, "Kobolds et Catacombes" },
          { Locale.itIT, "Coboldi & Catacombe" },
          { Locale.jaJP, "コボルトと秘宝の迷宮" },
          { Locale.koKR, "코볼트와 지하 미궁" },
          { Locale.plPL, "Koboldy i katakumby" },
          { Locale.ptBR, "Kobolds & Catacumbas" },
          { Locale.ruRU, "Кобольды и катакомбы" },
          { Locale.thTH, "Kobolds & Catacombs" },
          { Locale.zhCN, "狗头人与地下世界" },
          { Locale.zhTW, "狗頭人與地下城" }
        }
      },
      {
        31, new Dictionary<Locale, string>() {
          { Locale.enUS, "The Witchwood" },
          { Locale.deDE, "Der Hexenwald" },
          { Locale.esES, "El Bosque Embrujado" },
          { Locale.esMX, "El Bosque Embrujado" },
          { Locale.frFR, "Le Bois Maudit" },
          { Locale.itIT, "Boscotetro" },
          { Locale.jaJP, "妖の森ウィッチウッド" },
          { Locale.koKR, "마녀숲" },
          { Locale.plPL, "Wiedźmi Las" },
          { Locale.ptBR, "Bosque das Bruxas" },
          { Locale.ruRU, "Ведьмин лес" },
          { Locale.thTH, "The Witchwood" },
          { Locale.zhCN, "女巫森林" },
          { Locale.zhTW, "黑巫森林" }
        }
      },
      {
        38, new Dictionary<Locale, string>() {
          { Locale.enUS, "The Boomsday Project" },
          { Locale.deDE, "Dr. Bumms Geheimlabor" },
          { Locale.esES, "El Proyecto Armagebum" },
          { Locale.esMX, "El Proyecto K-Bum" },
          { Locale.frFR, "Projet Armageboum" },
          { Locale.itIT, "Operazione Apocalisse" },
          { Locale.jaJP, "博士のメカメカ大作戦" },
          { Locale.koKR, "박사 붐의 폭심만만 프로젝트" },
          { Locale.plPL, "Projekt Hukatomba" },
          { Locale.ptBR, "Projeto Cabum" },
          { Locale.ruRU, "Проект Бумного дня" },
          { Locale.thTH, "The Boomsday Project" },
          { Locale.zhCN, "砰砰计划" },
          { Locale.zhTW, "爆爆計畫" }
        }
      },
      {
        40, new Dictionary<Locale, string>() {
          { Locale.enUS, "Rastakhan's Rumble" },
          { Locale.deDE, "Rastakhans Rambazamba" },
          { Locale.esES, "La Arena de Rastakhan" },
          { Locale.esMX, "El Reto de Rastakhan" },
          { Locale.frFR, "Les Jeux de Rastakhan" },
          { Locale.itIT, "La sfida di Rastakhan" },
          { Locale.jaJP, "天下一ヴドゥ祭" },
          { Locale.koKR, "라스타칸의 대난투" },
          { Locale.plPL, "Rozróba Rastakana" },
          { Locale.ptBR, "Ringue do Rastakhan" },
          { Locale.ruRU, "Растахановы игрища" },
          { Locale.thTH, "Rastakhan's Rumble" },
          { Locale.zhCN, "拉斯塔哈的大乱斗" },
          { Locale.zhTW, "拉斯塔哈大混戰" }
        }
      },
      {
        41, new Dictionary<Locale, string>() {
          { Locale.enUS, "Mammoth Bundle" },
          { Locale.deDE, "Mammutpaket" },
          { Locale.esES, "Pack del Mamut" },
          { Locale.esMX, "Combo del mamut" },
          { Locale.frFR, "Pack du Mammouth" },
          { Locale.itIT, "Pacchetto del Mammut" },
          { Locale.jaJP, "マンモス・バンドル" },
          { Locale.koKR, "매머드 묶음 상품" },
          { Locale.plPL, "Mamuci zestaw" },
          { Locale.ptBR, "Oferta Mamute" },
          { Locale.ruRU, "Пакет года Мамонта" },
          { Locale.thTH, "ชุดแมมมอธ" },
          { Locale.zhCN, "猛犸年合集" },
          { Locale.zhTW, "猛瑪超值禮盒" }
        }
      },
      {
        49, new Dictionary<Locale, string>() {
          { Locale.enUS, "Rise of Shadows" },
          { Locale.deDE, "Verschwörung der Schatten" },
          { Locale.esES, "El Auge de las Sombras" },
          { Locale.esMX, "Ascenso de las sombras" },
          { Locale.frFR, "L’Éveil des ombres" },
          { Locale.itIT, "L'ascesa delle ombre" },
          { Locale.jaJP, "爆誕！悪党同盟" },
          { Locale.koKR, "어둠의 반격" },
          { Locale.plPL, "Wyjście z cienia" },
          { Locale.ptBR, "Ascensão das Sombras" },
          { Locale.ruRU, "Возмездие теней" },
          { Locale.thTH, "Rise of Shadows" },
          { Locale.zhCN, "暗影崛起" },
          { Locale.zhTW, "反派大進擊" }
        }
      },
      {
        128, new Dictionary<Locale, string>() {
          { Locale.enUS, "Saviors of Uldum" },
          { Locale.deDE, "Retter von Uldum" },
          { Locale.esES, "Salvadores de Uldum" },
          { Locale.esMX, "Defensores de Uldum" },
          { Locale.frFR, "Les Aventuriers d’Uldum" },
          { Locale.itIT, "Salvatori di Uldum" },
          { Locale.jaJP, "突撃！探検同盟" },
          { Locale.koKR, "울둠의 구원자" },
          { Locale.plPL, "Wybawcy Uldum" },
          { Locale.ptBR, "Salvadores de Uldum" },
          { Locale.ruRU, "Спасители Ульдума" },
          { Locale.thTH, "Saviors of Uldum" },
          { Locale.zhCN, "奥丹姆奇兵" },
          { Locale.zhTW, "奧丹姆守護者" }
        }
      },
      {
        181, new Dictionary<Locale, string>() {
          { Locale.enUS, "Welcome Bundle" },
          { Locale.deDE, "Willkommenspaket" },
          { Locale.esES, "Pack de bienvenida" },
          { Locale.esMX, "Combo de bienvenida" },
          { Locale.frFR, "Pack de bienvenue" },
          { Locale.itIT, "Pacchetto di Benvenuto" },
          { Locale.jaJP, "歓迎バンドル" },
          { Locale.koKR, "여관주인의 환영 상품: " },
          { Locale.plPL, "Zestaw powitalny" },
          { Locale.ptBR, "Pacote de Boas-vindas" },
          { Locale.ruRU, "Стартовый пакет" },
          { Locale.thTH, "ชุดต้อนรับ" },
          { Locale.zhCN, "迎新合集" },
          { Locale.zhTW, "超值禮盒" }
        }
      },
      {
        347, new Dictionary<Locale, string>() {
          { Locale.enUS, "Descent of Dragons" },
          { Locale.deDE, "Erbe der Drachen" },
          { Locale.esES, "El Descenso de los Dragones" },
          { Locale.esMX, "Descenso de los Dragones" },
          { Locale.frFR, "L’Envol des Dragons" },
          { Locale.itIT, "La Discesa dei Draghi" },
          { Locale.jaJP, "激闘！ドラゴン大決戦" },
          { Locale.koKR, "용의 강림" },
          { Locale.plPL, "Wejście smoków" },
          { Locale.ptBR, "Despontar dos Dragões" },
          { Locale.ruRU, "Натиск драконов" },
          { Locale.thTH, "Descent of Dragons" },
          { Locale.zhCN, "巨龙降临" },
          { Locale.zhTW, "降臨！遠古巨龍" }
        }
      },
      {
        423, new Dictionary<Locale, string>() {
          { Locale.enUS, "Ashes of Outland" },
          { Locale.deDE, "Ruinen der Scherbenwelt" },
          { Locale.esES, "Cenizas de Terrallende" },
          { Locale.esMX, "Cenizas de Terrallende" },
          { Locale.frFR, "Les Cendres de l’Outreterre" },
          { Locale.itIT, "Ceneri delle Terre Esterne" },
          { Locale.jaJP, "灰に舞う降魔の狩人" },
          { Locale.koKR, "황폐한 아웃랜드" },
          { Locale.plPL, "Popioły Rubieży" },
          { Locale.ptBR, "Cinzas de Terralém" },
          { Locale.ruRU, "Руины Запределья" },
          { Locale.thTH, "Ashes of Outland" },
          { Locale.zhCN, "外域的灰烬" },
          { Locale.zhTW, "外域之燼" }
        }
      },
      {
        465, new Dictionary<Locale, string>() {
          { Locale.enUS, "Quest Pack" },
          { Locale.deDE, "Questpackung" },
          { Locale.esES, "Sobre de misión" },
          { Locale.esMX, "Paquete de misiones" },
          { Locale.frFR, "Paquet de quête" },
          { Locale.itIT, "Busta Missione" },
          { Locale.jaJP, "クエストパック" },
          { Locale.koKR, "퀘스트 팩" },
          { Locale.plPL, "Pakiet zadań" },
          { Locale.ptBR, "Pacote de Missões" },
          { Locale.ruRU, "Комплект заданий" },
          { Locale.thTH, "ซองเควสต์" },
          { Locale.zhCN, "任务包" },
          { Locale.zhTW, "任務包" }
        }
      },
      {
        468, new Dictionary<Locale, string>() {
          { Locale.enUS, "Scholomance Academy" },
          { Locale.deDE, "Akademie Scholomance" },
          { Locale.esES, "Academia Scholomance" },
          { Locale.esMX, "Academia Scholomance" },
          { Locale.frFR, "L’Académie Scholomance" },
          { Locale.itIT, "Accademia di Scholomance" },
          { Locale.jaJP, "魔法学院スクロマンス" },
          { Locale.koKR, "스칼로맨스 아카데미" },
          { Locale.plPL, "Scholomancjum" },
          { Locale.ptBR, "Universidade de Scolomântia" },
          { Locale.ruRU, "Некроситет" },
          { Locale.thTH, "Scholomance Academy" },
          { Locale.zhCN, "通灵学园" },
          { Locale.zhTW, "通靈學院" }
        }
      },
      {
        603, new Dictionary<Locale, string>() {
          { Locale.enUS, "Golden Scholomance Academy" },
          { Locale.deDE, "Goldene Akademie Scholomance" },
          { Locale.esES, "Doradas de Academia Scholomance" },
          { Locale.esMX, "Academia Scholomance dorado" },
          { Locale.frFR, "L’Académie Scholomance doré" },
          { Locale.itIT, "Accademia di Scholomance Dorata" },
          { Locale.jaJP, "ゴールデン魔法学院スクロマンス" },
          { Locale.koKR, "황금 스칼로맨스 아카데미" },
          { Locale.plPL, "Złote Scholomancjum" },
          { Locale.ptBR, "Universidade de Scolomântia Dourado" },
          { Locale.ruRU, "Золотой Некроситет" },
          { Locale.thTH, "Scholomance Academy สีทอง" },
          { Locale.zhCN, "金色通灵学园" },
          { Locale.zhTW, "通靈學院金卡" }
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
