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
          { Locale.frFR, "Classique" },
          { Locale.deDE, "Klassik" },
          { Locale.koKR, "오리지널" },
          { Locale.esES, "Clásico" },
          { Locale.esMX, "Clásico" },
          { Locale.ruRU, "Классический набор" },
          { Locale.zhTW, "經典" },
          { Locale.zhCN, "经典" },
          { Locale.itIT, "Classiche" },
          { Locale.ptBR, "Clássico" },
          { Locale.plPL, "Klasyczne" },
          { Locale.ptPT, "Clássico" },
          { Locale.jaJP, "クラシック" },
          { Locale.thTH, "คลาสสิค" },
        }
      },
      {
        9, new Dictionary<Locale, string>() {
          { Locale.enUS, "Goblins vs Gnomes" },
          { Locale.enGB, "Goblins vs Gnomes" },
          { Locale.frFR, "Gobelins et Gnomes" },
          { Locale.deDE, "Goblins gegen Gnome" },
          { Locale.koKR, "고블린 대 노움" },
          { Locale.esES, "Goblins vs. Gnomos" },
          { Locale.esMX, "Goblins versus Gnomos" },
          { Locale.ruRU, "Гоблины и гномы" },
          { Locale.zhTW, "哥哥打地地" },
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
          { Locale.zhTW, "銀白聯賽" },
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
          { Locale.frFR, "Murmures des Dieux très anciens" },
          { Locale.deDE, "Das Flüstern der Alten Götter" },
          { Locale.koKR, "고대 신의 속삭임" },
          { Locale.esES, "Susurros de los Dioses Antiguos" },
          { Locale.esMX, "Susurros de los Dioses Antiguos" },
          { Locale.ruRU, "Пробуждение древних богов" },
          { Locale.zhTW, "古神碎碎念" },
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
        17, new Dictionary<Locale, string>() {
          { Locale.enUS, "Welcome Bundle" },
          { Locale.enGB, "Welcome Bundle" },
          { Locale.frFR, "Pack de bienvenue" },
          { Locale.deDE, "Willkommens[d]paket" },
          { Locale.koKR, "여관주인의 환영 상품: " },
          { Locale.esES, "Pack de bienvenida" },
          { Locale.esMX, "Combo de bienvenida" },
          { Locale.ruRU, "Стартовый пакет" },
          { Locale.zhTW, "超值禮盒" },
          { Locale.zhCN, "迎新合集" },
          { Locale.itIT, "Pacchetto di Benvenuto" },
          { Locale.ptBR, "Pacote de Boas-vindas" },
          { Locale.plPL, "Zestaw powitalny" },
          { Locale.ptPT, "Pacote de Boas-vindas" },
          { Locale.jaJP, "歓迎バンドル" },
          { Locale.thTH, "ชุดต้อนรับ" },
        }
      },
      {
        18, new Dictionary<Locale, string>() {
          { Locale.enUS, "Classic" },
          { Locale.enGB, "Classic" },
          { Locale.frFR, "Classique" },
          { Locale.deDE, "Klassik" },
          { Locale.koKR, "오리지널" },
          { Locale.esES, "Clásico" },
          { Locale.esMX, "Clásico" },
          { Locale.ruRU, "Классический набор" },
          { Locale.zhTW, "經典" },
          { Locale.zhCN, "经典" },
          { Locale.itIT, "Classiche" },
          { Locale.ptBR, "Clássico" },
          { Locale.plPL, "Klasyczne" },
          { Locale.ptPT, "Clássico" },
          { Locale.jaJP, "クラシック" },
          { Locale.thTH, "คลาสสิค" },
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
          { Locale.esMX, "Los Callejones de Gadgetzan" },
          { Locale.ruRU, "Злачный город Прибамбасск" },
          { Locale.zhTW, "加基森風雲" },
          { Locale.zhCN, "龙争虎斗加基森" },
          { Locale.itIT, "Bassifondi di Meccania" },
          { Locale.ptBR, "Gangues de Geringontzan" },
          { Locale.plPL, "Ciemne zaułki Gadżetonu" },
          { Locale.ptPT, "Gangues de Geringontzan" },
          { Locale.jaJP, "仁義なきガジェッツァン" },
          { Locale.thTH, "Mean Streets of Gadgetzan" },
        }
      },
      {
        20, new Dictionary<Locale, string>() {
          { Locale.enUS, "Journey to Un'Goro" },
          { Locale.enGB, "Journey to Un'Goro" },
          { Locale.frFR, "Voyage au centre d’Un’Goro" },
          { Locale.deDE, "Reise nach Un’Goro" },
          { Locale.koKR, "운고로를 향한 여정" },
          { Locale.esES, "Viaje a Un'Goro" },
          { Locale.esMX, "Viaje a Un'Goro" },
          { Locale.ruRU, "Экспедиция в Ун'Горо" },
          { Locale.zhTW, "安戈洛歷險記" },
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
          { Locale.zhTW, "冰封王座" },
          { Locale.zhCN, "冰封王座的骑士" },
          { Locale.itIT, "Cavalieri del Trono di Ghiaccio" },
          { Locale.ptBR, "Cavaleiros do Trono de Gelo" },
          { Locale.plPL, "Rycerze Mroźnego Tronu" },
          { Locale.ptPT, "Cavaleiros do Trono de Gelo" },
          { Locale.jaJP, "凍てつく玉座の騎士団" },
          { Locale.thTH, "Knights of the Frozen Throne" },
        }
      },
      {
        23, new Dictionary<Locale, string>() {
          { Locale.enUS, "Golden Classic" },
          { Locale.enGB, "Golden Classic" },
          { Locale.frFR, "Classique dorée" },
          { Locale.deDE, "Klassik (Golden)" },
          { Locale.koKR, "황금 오리지널" },
          { Locale.esES, "Clásica dorada" },
          { Locale.esMX, "Clásica dorada" },
          { Locale.ruRU, "Классический набор (Золотой)" },
          { Locale.zhTW, "經典金卡" },
          { Locale.zhCN, "金色经典" },
          { Locale.itIT, "Classiche Dorate" },
          { Locale.ptBR, "Clássico Dourado" },
          { Locale.plPL, "Złote klasyczne" },
          { Locale.ptPT, "Clássico Dourado" },
          { Locale.jaJP, "ゴールデンクラシック" },
          { Locale.thTH, "คลาสสิคสีทอง" },
        }
      },
      {
        30, new Dictionary<Locale, string>() {
          { Locale.enUS, "Kobolds & Catacombs" },
          { Locale.enGB, "Kobolds & Catacombs" },
          { Locale.frFR, "Kobolds et Catacombes" },
          { Locale.deDE, "Kobolde & Katakomben" },
          { Locale.koKR, "코볼트와 지하 미궁" },
          { Locale.esES, "Kóbolds & Catacumbas" },
          { Locale.esMX, "Kóbolds & Catacumbas" },
          { Locale.ruRU, "Кобольды и катакомбы" },
          { Locale.zhTW, "狗頭人與地下城" },
          { Locale.zhCN, "狗头人与地下世界" },
          { Locale.itIT, "Coboldi & Catacombe" },
          { Locale.ptBR, "Kobolds & Catacumbas" },
          { Locale.plPL, "Koboldy i katakumby" },
          { Locale.ptPT, "Kobolds & Catacumbas" },
          { Locale.jaJP, "コボルトと秘宝の迷宮" },
          { Locale.thTH, "Kobolds & Catacombs" },
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
          { Locale.zhTW, "黑巫森林" },
          { Locale.zhCN, "女巫森林" },
          { Locale.itIT, "Boscotetro" },
          { Locale.ptBR, "Bosque das Bruxas" },
          { Locale.plPL, "Wiedźmi Las" },
          { Locale.ptPT, "Bosque das Bruxas" },
          { Locale.jaJP, "妖の森ウィッチウッド" },
          { Locale.thTH, "The Witchwood" },
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
          { Locale.zhTW, "爆爆計畫" },
          { Locale.zhCN, "砰砰计划" },
          { Locale.itIT, "Operazione Apocalisse" },
          { Locale.ptBR, "Projeto Cabum" },
          { Locale.plPL, "Projekt Hukatomba" },
          { Locale.ptPT, "Projeto Cabum" },
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
          { Locale.zhTW, "拉斯塔哈大混戰" },
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
        41, new Dictionary<Locale, string>() {
          { Locale.enUS, "Mammoth Bundle" },
          { Locale.enGB, "Mammoth Bundle" },
          { Locale.frFR, "Pack du Mammouth" },
          { Locale.deDE, "Mammutpaket" },
          { Locale.koKR, "매머드 묶음 상품" },
          { Locale.esES, "Pack del Mamut" },
          { Locale.esMX, "Combo del mamut" },
          { Locale.ruRU, "Пакет года Мамонта" },
          { Locale.zhTW, "猛瑪超值禮盒" },
          { Locale.zhCN, "猛犸年合集" },
          { Locale.itIT, "Pacchetto del Mammut" },
          { Locale.ptBR, "Oferta Mamute" },
          { Locale.plPL, "Mamuci zestaw" },
          { Locale.ptPT, "Oferta Mamute" },
          { Locale.jaJP, "マンモス・バンドル" },
          { Locale.thTH, "ชุดแมมมอธ" },
        }
      },
      {
        49, new Dictionary<Locale, string>() {
          { Locale.enUS, "Rise of Shadows" },
          { Locale.enGB, "Rise of Shadows" },
          { Locale.frFR, "L’Éveil des ombres" },
          { Locale.deDE, "Verschwörung der Schatten" },
          { Locale.koKR, "어둠의 반격" },
          { Locale.esES, "El Auge de las Sombras" },
          { Locale.esMX, "Ascenso de las sombras" },
          { Locale.ruRU, "Возмездие теней" },
          { Locale.zhTW, "反派大進擊" },
          { Locale.zhCN, "暗影崛起" },
          { Locale.itIT, "L'ascesa delle ombre" },
          { Locale.ptBR, "Ascensão das Sombras" },
          { Locale.plPL, "Wyjście z cienia" },
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
          { Locale.zhTW, "奧丹姆守護者" },
          { Locale.zhCN, "奥丹姆奇兵" },
          { Locale.itIT, "Salvatori di Uldum" },
          { Locale.ptBR, "Salvadores de Uldum" },
          { Locale.plPL, "Wybawcy Uldum" },
          { Locale.ptPT, "Salvadores de Uldum" },
          { Locale.jaJP, "突撃！探検同盟" },
          { Locale.thTH, "Saviors of Uldum" },
        }
      },
      {
        181, new Dictionary<Locale, string>() {
          { Locale.enUS, "Welcome Bundle" },
          { Locale.enGB, "Welcome Bundle" },
          { Locale.frFR, "Pack de bienvenue" },
          { Locale.deDE, "Willkommenspaket" },
          { Locale.koKR, "여관주인의 환영 상품: " },
          { Locale.esES, "Pack de bienvenida" },
          { Locale.esMX, "Combo de bienvenida" },
          { Locale.ruRU, "Стартовый пакет" },
          { Locale.zhTW, "超值禮盒" },
          { Locale.zhCN, "迎新合集" },
          { Locale.itIT, "Pacchetto di Benvenuto" },
          { Locale.ptBR, "Pacote de Boas-vindas" },
          { Locale.plPL, "Zestaw powitalny" },
          { Locale.ptPT, "Pacote de Boas-vindas" },
          { Locale.jaJP, "歓迎バンドル" },
          { Locale.thTH, "ชุดต้อนรับ" },
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
          { Locale.zhTW, "降臨！遠古巨龍" },
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
          { Locale.zhTW, "外域之燼" },
          { Locale.zhCN, "外域的灰烬" },
          { Locale.itIT, "Ceneri delle Terre Esterne" },
          { Locale.ptBR, "Cinzas de Terralém" },
          { Locale.plPL, "Popioły Rubieży" },
          { Locale.ptPT, "Cinzas de Terralém" },
          { Locale.jaJP, "灰に舞う降魔の狩人" },
          { Locale.thTH, "Ashes of Outland" },
        }
      },
      {
        465, new Dictionary<Locale, string>() {
          { Locale.enUS, "Quest Pack" },
          { Locale.enGB, "Quest Pack" },
          { Locale.frFR, "Paquet de quête" },
          { Locale.deDE, "Questpackung" },
          { Locale.koKR, "퀘스트 팩" },
          { Locale.esES, "Sobre de misión" },
          { Locale.esMX, "Paquete de misiones" },
          { Locale.ruRU, "Комплект заданий" },
          { Locale.zhTW, "任務包" },
          { Locale.zhCN, "任务包" },
          { Locale.itIT, "Busta Missione" },
          { Locale.ptBR, "Pacote de Missões" },
          { Locale.plPL, "Pakiet zadań" },
          { Locale.ptPT, "Pacote de Missões" },
          { Locale.jaJP, "クエストパック" },
          { Locale.thTH, "ซองเควสต์" },
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
          { Locale.itIT, "Accademia di Scholomance" },
          { Locale.ptBR, "Universidade de Scolomântia" },
          { Locale.plPL, "Scholomancjum" },
          { Locale.ptPT, "Universidade de Scolomântia" },
          { Locale.jaJP, "魔法学院スクロマンス" },
          { Locale.thTH, "Scholomance Academy" },
        }
      },
      {
        470, new Dictionary<Locale, string>() {
          { Locale.enUS, "Hunter Pack" },
          { Locale.enGB, "Hunter Pack" },
          { Locale.frFR, "Paquet de classe Chasseur" },
          { Locale.deDE, "Jägerpackung" },
          { Locale.koKR, "사냥꾼 팩" },
          { Locale.esES, "Sobre de Cazador" },
          { Locale.esMX, "Paquete de Cazador" },
          { Locale.ruRU, "Комплект карт Охотника" },
          { Locale.zhTW, "包獵人" },
          { Locale.zhCN, "包猎人" },
          { Locale.itIT, "Busta del Cacciatore" },
          { Locale.ptBR, "Pacote de Caçador" },
          { Locale.plPL, "Pakiet Łowcy" },
          { Locale.ptPT, "Pacote de Caçador" },
          { Locale.jaJP, "ハンターパック" },
          { Locale.thTH, "ซองการ์ดฮันเตอร์" },
        }
      },
      {
        498, new Dictionary<Locale, string>() {
          { Locale.enUS, "Year of the Dragon" },
          { Locale.enGB, "Year of the Dragon" },
          { Locale.frFR, "L'Année du Dragon" },
          { Locale.deDE, "Jahr des Drachen" },
          { Locale.koKR, "용의 해" },
          { Locale.esES, "Año del Dragón" },
          { Locale.esMX, "Año del Dragón" },
          { Locale.ruRU, "Год Дракона" },
          { Locale.zhTW, "巨龍年" },
          { Locale.zhCN, "巨龙年" },
          { Locale.itIT, "Anno del Drago" },
          { Locale.ptBR, "Ano do Dragão" },
          { Locale.plPL, "Roku Smoka" },
          { Locale.ptPT, "Ano do Dragão" },
          { Locale.jaJP, "ドラゴンの年" },
          { Locale.thTH, "ปีแห่งมังกร" },
        }
      },
      {
        545, new Dictionary<Locale, string>() {
          { Locale.enUS, "Mage Pack" },
          { Locale.enGB, "Mage Pack" },
          { Locale.frFR, "Paquet de classe Mage" },
          { Locale.deDE, "Magierpackung" },
          { Locale.koKR, "마법사 팩" },
          { Locale.esES, "Sobre de Mago" },
          { Locale.esMX, "Paquete de Mago" },
          { Locale.ruRU, "Комплект карт Мага" },
          { Locale.zhTW, "包法師" },
          { Locale.zhCN, "包法师" },
          { Locale.itIT, "Busta del Mago" },
          { Locale.ptBR, "Pacote de Mago" },
          { Locale.plPL, "Pakiet Maga" },
          { Locale.ptPT, "Pacote de Mago" },
          { Locale.jaJP, "メイジパック" },
          { Locale.thTH, "ซองการ์ดเมจ" },
        }
      },
      {
        553, new Dictionary<Locale, string>() {
          { Locale.enUS, "Forged in the Barrens" },
          { Locale.enGB, "Forged in the Barrens" },
          { Locale.frFR, "Forgés dans les Tarides" },
          { Locale.deDE, "Geschmiedet im Brachlan" },
          { Locale.koKR, "불모의 땅" },
          { Locale.esES, "Forjados en Los Baldíos" },
          { Locale.esMX, "Forjados en los Baldíos" },
          { Locale.ruRU, "Закаленные Степями" },
          { Locale.zhTW, "贫瘠之地的锤炼" },
          { Locale.zhCN, "貧瘠之地" },
          { Locale.itIT, "Forgiati nelle Savane" },
          { Locale.ptBR, "Forjado nos Sertões" },
          { Locale.plPL, "Zahartowani przez Pustkowia" },
          { Locale.ptPT, "Forjado nos Sertões" },
          { Locale.jaJP, "荒ぶる大地の強者たち" },
          { Locale.thTH, "Forged in the Barrens" },
        }
      },
      {
        603, new Dictionary<Locale, string>() {
          { Locale.enUS, "Golden Scholomance Academy" },
          { Locale.enGB, "Golden Scholomance Academy" },
          { Locale.frFR, "L’Académie Scholomance doré" },
          { Locale.deDE, "Goldene Akademie Scholomance" },
          { Locale.koKR, "황금 스칼로맨스 아카데미" },
          { Locale.esES, "Doradas de Academia Scholomance" },
          { Locale.esMX, "Academia Scholomance dorado" },
          { Locale.ruRU, "Некроситет (Золотой)" },
          { Locale.zhTW, "通靈學院金卡" },
          { Locale.zhCN, "金色通灵学园" },
          { Locale.itIT, "Accademia di Scholomance Dorata" },
          { Locale.ptBR, "Universidade de Scolomântia Dourado" },
          { Locale.plPL, "Złote Scholomancjum" },
          { Locale.ptPT, "Universidade de Scolomântia Dourado" },
          { Locale.jaJP, "ゴールデン魔法学院スクロマンス" },
          { Locale.thTH, "Scholomance Academy สีทอง" },
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
          { Locale.zhTW, "暗月馬戲團：古神也瘋狂" },
          { Locale.zhCN, "疯狂的暗月马戏团" },
          { Locale.itIT, "Follia alla Fiera di Lunacupa" },
          { Locale.ptBR, "Delírios em Negraluna" },
          { Locale.plPL, "Obłędny Festyn Lunomroku" },
          { Locale.ptPT, "Delírios em Negraluna" },
          { Locale.jaJP, "ダークムーン・フェアへの招待状" },
          { Locale.thTH, "Madness at the Darkmoon Faire" },
        }
      },
	  {
        631, new Dictionary<Locale, string>() {
          { Locale.enUS, "Druid Pack" },
          { Locale.enGB, "Druid Pack" },
          { Locale.frFR, "Paquet de classe Druide" },
          { Locale.deDE, "Druidepackung" },
          { Locale.koKR, "드루이드 팩" },
          { Locale.esES, "Sobre de Druida" },
          { Locale.esMX, "Paquete de Druida" },
          { Locale.ruRU, "Комплект карт Друида" },
          { Locale.zhTW, "包德魯伊" },
          { Locale.zhCN, "包德鲁伊" },
          { Locale.itIT, "Busta del Druido" },
          { Locale.ptBR, "Pacote de Druida" },
          { Locale.plPL, "Pakiet Druida" },
          { Locale.ptPT, "Pacote de Druida" },
          { Locale.jaJP, "ドルイドパック" },
          { Locale.thTH, "ซองการ์ดดรูอิด" },
        }
      },
      {
        632, new Dictionary<Locale, string>() {
          { Locale.enUS, "Paladin Pack" },
          { Locale.enGB, "Paladin Pack" },
          { Locale.frFR, "Paquet de classe paladin" },
          { Locale.deDE, "Paladinpackung" },
          { Locale.koKR, "성기사 팩" },
          { Locale.esES, "Sobre de paladín" },
          { Locale.esMX, "Paquete de Paladín" },
          { Locale.ruRU, "Набор карт Паладина" },
          { Locale.zhTW, "包聖騎士" },
          { Locale.zhCN, "包圣骑士" },
          { Locale.itIT, "Busta del Paladino" },
          { Locale.ptBR, "Pacote de Paladino" },
          { Locale.plPL, "Pakiet paladyna" },
          { Locale.ptPT, "Pacote de Paladino" },
          { Locale.jaJP, "パラディンパック" },
          { Locale.thTH, "ซองการ์ดพาลาดิน" },
        }
      },
      {
        633, new Dictionary<Locale, string>() {
          { Locale.enUS, "Warrior Pack" },
          { Locale.enGB, "Warrior Pack" },
          { Locale.frFR, "Paquet de classe guerrier" },
          { Locale.deDE, "Kriegerpackung" },
          { Locale.koKR, "전사 팩" },
          { Locale.esES, "Sobre de guerrero" },
          { Locale.esMX, "Paquete de Guerrero" },
          { Locale.ruRU, "Набор карт Воина" },
          { Locale.zhTW, "包戰士" },
          { Locale.zhCN, "包战士" },
          { Locale.itIT, "Busta del Guerriero" },
          { Locale.ptBR, "Pacote de Guerreiro" },
          { Locale.plPL, "Pakiet wojownika" },
          { Locale.ptPT, "Pacote de Guerreiro" },
          { Locale.jaJP, "ウォリアーパック" },
          { Locale.thTH, "ซองการ์ดวอริเออร์" },
        }
      },
      {
        634, new Dictionary<Locale, string>() {
          { Locale.enUS, "Priest Pack" },
          { Locale.enGB, "Priest Pack" },
          { Locale.frFR, "Paquet de classe Prêtre" },
          { Locale.deDE, "Priesterpackung" },
          { Locale.koKR, "사제 팩" },
          { Locale.esES, "Sobre de Sacerdote" },
          { Locale.esMX, "Paquete de Sacerdote" },
          { Locale.ruRU, "Набор карт Жреца" },
          { Locale.zhTW, "包牧師" },
          { Locale.zhCN, "包牧师" },
          { Locale.itIT, "Busta del Sacerdote" },
          { Locale.ptBR, "Pacote de Sacerdote" },
          { Locale.plPL, "Pakiet Kapłana" },
          { Locale.ptPT, "Pacote de Sacerdote" },
          { Locale.jaJP, "プリーストパック" },
          { Locale.thTH, "ซองการ์ดโร้ก" },
        }
      },
      {
        635, new Dictionary<Locale, string>() {
          { Locale.enUS, "Rogue Pack" },
          { Locale.enGB, "Rogue Pack" },
          { Locale.frFR, "Paquet de classe Voleur" },
          { Locale.deDE, "Schurkepackung" },
          { Locale.koKR, "도적 팩" },
          { Locale.esES, "Sobre de Picaro" },
          { Locale.esMX, "Paquete de Picaro" },
          { Locale.ruRU, "Набор карт Разбойника" },
          { Locale.zhTW, "包盜賊" },
          { Locale.zhCN, "包潜行者" },
          { Locale.itIT, "Busta del Ladro" },
          { Locale.ptBR, "Pacote de Ladino" },
          { Locale.plPL, "Pakiet Łotra" },
          { Locale.ptPT, "Pacote de Ladino" },
          { Locale.jaJP, "ローグパック" },
          { Locale.thTH, "ซองการ์ดชาแมน" },
        }
      },
      {
        636, new Dictionary<Locale, string>() {
          { Locale.enUS, "Shaman Pack" },
          { Locale.enGB, "Shaman Pack" },
          { Locale.frFR, "Paquet de classe Chaman" },
          { Locale.deDE, "Schamanepackung" },
          { Locale.koKR, "주술사 팩" },
          { Locale.esES, "Sobre de Chamán" },
          { Locale.esMX, "Paquete de Chamán" },
          { Locale.ruRU, "Набор карт Шамана" },
          { Locale.zhTW, "包薩滿" },
          { Locale.zhCN, "包萨满祭司" },
          { Locale.itIT, "Busta del Sciamano" },
          { Locale.ptBR, "Pacote de Xamã" },
          { Locale.plPL, "Pakiet Szamana" },
          { Locale.ptPT, "Pacote de Xamã" },
          { Locale.jaJP, "シャーマンパック" },
          { Locale.thTH, "ซองการ์ดวอร์ล็อค" },
        }
      },
      {
        637, new Dictionary<Locale, string>() {
          { Locale.enUS, "Warlock Pack" },
          { Locale.enGB, "Warlock Pack" },
          { Locale.frFR, "Paquet de classe Démoniste" },
          { Locale.deDE, "Hexenmeisterpackung" },
          { Locale.koKR, "흑마법사 팩" },
          { Locale.esES, "Sobre de Brujo" },
          { Locale.esMX, "Paquete de Brujo" },
          { Locale.ruRU, "Набор карт Чернокнижника" },
          { Locale.zhTW, "包術士" },
          { Locale.zhCN, "包术士" },
          { Locale.itIT, "Busta del Stregone" },
          { Locale.ptBR, "Pacote de Bruxo" },
          { Locale.plPL, "Pakiet Czarnoksiężnika" },
          { Locale.ptPT, "Pacote de Bruxo" },
          { Locale.jaJP, "ウォーロックパック" },
          { Locale.thTH, "ซองการ์ดวอริเออร์" },
        }
      },
      {
        638, new Dictionary<Locale, string>() {
          { Locale.enUS, "Demon Hunter Pack" },
          { Locale.enGB, "Demon Hunter Pack" },
          { Locale.frFR, "Paquet de classe Chasseur de démons" },
          { Locale.deDE, "Dämonenjägerpackung" },
          { Locale.koKR, "악마사냥꾼 팩" },
          { Locale.esES, "Sobre de Cazador de demonios" },
          { Locale.esMX, "Paquete de Cazador de demonios" },
          { Locale.ruRU, "Набор карт  Охотника на демонов" },
          { Locale.zhTW, "包惡魔獵人" },
          { Locale.zhCN, "包恶魔猎手" },
          { Locale.itIT, "Busta del Cacciatore di Demoni" },
          { Locale.ptBR, "Pacote de Caçador de Demônios" },
          { Locale.plPL, "Pakiet Łowcy demonów" },
          { Locale.ptPT, "Pacote de Caçador de Demônios" },
          { Locale.jaJP, "デーモンハンターパック" },
          { Locale.thTH, "ซองการ์ดดีมอนฮันเตอร์" },
        }
      },
      {
        643, new Dictionary<Locale, string>() {
          { Locale.enUS, "Golden Madness at the Darkmoon Faire" },
          { Locale.enGB, "Golden Madness at the Darkmoon Faire" },
          { Locale.frFR, "Folle journée dorée à Sombrelune" },
          { Locale.deDE, "Der Dunkelmond-Wahnsinn (Golden)" },
          { Locale.koKR, "황금 광기의 다크문 축제" },
          { Locale.esES, "Locura dorada en la Feria de la Luna Negra" },
          { Locale.esMX, "Locura en la Feria de la Luna Negra dorada" },
          { Locale.ruRU, "Ярмарка безумия (Золотой)" },
          { Locale.zhTW, "暗月馬戲團：古神也瘋狂金卡" },
          { Locale.zhCN, "金色疯狂的暗月马戏团" },
          { Locale.itIT, "Follia alla Fiera di Lunacupa Dorata" },
          { Locale.ptBR, "Delírios em Negraluna Dourado" },
          { Locale.plPL, "Złoty Obłędny Festyn Lunomroku" },
          { Locale.ptPT, "Delírios em Negraluna Dourado" },
          { Locale.jaJP, "ゴールデン・ダークムーン・フェアへの招待状" },
          { Locale.thTH, "Madness at the Darkmoon Faire สีทอง" },
        }
      },
      {
        686, new Dictionary<Locale, string>() {
          { Locale.enUS, "Golden Forged in the Barrens" },
          { Locale.enGB, "Golden Forged in the Barrens" },
          { Locale.frFR, "Forgés dans les Tarides doré" },
          { Locale.deDE, "Geschmiedet im Brachland (Golden)" },
          { Locale.koKR, "황금 불모의 땅" },
          { Locale.esES, "Doradas de Forjados en los Baldíos" },
          { Locale.esMX, "Dorado de Forjados en los Baldíos" },
          { Locale.ruRU, "Золотой комплект «Закаленных Степями»" },
          { Locale.zhTW, "贫瘠之地的锤炼金卡" },
          { Locale.zhCN, "貧瘠之地金卡" },
          { Locale.itIT, "Forgiati nelle Savane Dorata" },
          { Locale.ptBR, "Forjado nos Sertões Dourado" },
          { Locale.plPL, "Złoty zestaw Zahartowani przez Pustkowia" },
          { Locale.ptPT, "Forjado nos Sertões Dourado" },
          { Locale.jaJP, "ゴールデン荒ぶる大地の強者たち" },
          { Locale.thTH, "Forged in the Barrens สีทอง" },
	 }
       },
       {
        688, new Dictionary<Locale, string>() {
          { Locale.enUS, "Year of the Phoenix" },
          { Locale.enGB, "Year of the Phoenix" },
          { Locale.frFR, "L'Année Du Phénix" },
          { Locale.deDE, "Jahr des Phönix" },
          { Locale.koKR, "피닉스의 해" },
          { Locale.esES, "Año de Phoenix" },
          { Locale.esMX, "Año de Phoenix" },
          { Locale.ruRU, "Год Феникса" },
          { Locale.zhTW, "鳳凰年" },
          { Locale.zhCN, "凤凰年" },
          { Locale.itIT, "L'Anno Della Fenice" },
          { Locale.ptBR, "Ano Da Fênix" },
          { Locale.plPL, "Roku " },
          { Locale.ptPT, "Ano Da Fênix" },
          { Locale.jaJP, "フェニックスの年" },
          { Locale.thTH, "ปีของฟีนิกซ์" },
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
