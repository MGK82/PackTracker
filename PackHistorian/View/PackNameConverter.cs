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
          { Locale.enUS, "Rexxar's Book of Heroes" },
          { Locale.enGB, "Rexxar's Book of Heroes" },
          { Locale.frFR, "Le Livre des héros de Rexxar" },
          { Locale.deDE, "Rexxars Buch der Helden" },
          { Locale.koKR, "렉사르의 영웅 책" },
          { Locale.esES, "El libro de los héroes de Rexxar" },
          { Locale.esMX, "El libro de los héroes de Rexxar" },
          { Locale.ruRU, "«Книга героев» Рексара" },
          { Locale.zhTW, "雷克薩英雄錄" },
          { Locale.zhCN, "雷克萨英雄录" },
          { Locale.itIT, "Il Libro degli Eroi di Rexxar" },
          { Locale.ptBR, "O Livro dos Heróis de Rexxar" },
          { Locale.plPL, "Księga bohaterów Rexxara" },
          { Locale.ptPT, "O Livro dos Heróis de Rexxar" },
          { Locale.jaJP, "レクサールのヒーローズブック" },
          { Locale.thTH, "Rexxar's Book of Heroes" },
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
          { Locale.thTH, "Year of the Dragon" },
        }
      },
	  	        {
        545, new Dictionary<Locale, string>() {
          { Locale.enUS, "Jaina's Book of Heroes" },
          { Locale.enGB, "Jaina's Book of Heroes" },
          { Locale.frFR, "Le Livre des héros de Jaina" },
          { Locale.deDE, "Jainas Buch der Helden" },
          { Locale.koKR, "제이나의 영웅 책" },
          { Locale.esES, "El libro de los héroes de Jaina" },
          { Locale.esMX, "El libro de los héroes de Jaina" },
          { Locale.ruRU, "«Книга героев» Джайны" },
          { Locale.zhTW, "吉安娜的英雄書" },
          { Locale.zhCN, "吉安娜的英雄书" },
          { Locale.itIT, "Il Libro degli Eroi di Jaina" },
          { Locale.ptBR, "O Livro dos Heróis de Jaina" },
          { Locale.plPL, "Księga bohaterów Jainy" },
          { Locale.ptPT, "O Livro dos Heróis de Jaina" },
          { Locale.jaJP, "ジャイナの英雄の本" },
          { Locale.thTH, "Jaina's Book of Heroes" },
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
          { Locale.enUS, "Malfurion's Book of Heroes" },
          { Locale.enGB, "Malfurion's Book of Heroes" },
          { Locale.frFR, "Le Livre des héros de Malfurion" },
          { Locale.deDE, "Malfurions Buch der Helden" },
          { Locale.koKR, "말푸리온의 영웅 책" },
          { Locale.esES, "El libro de los héroes de Malfurion" },
          { Locale.esMX, "El libro de los héroes de Malfurion" },
          { Locale.ruRU, "«Книга героев» Малфуриона" },
          { Locale.zhTW, "瑪法里奧的英雄錄" },
          { Locale.zhCN, "玛法里奥的英雄录" },
          { Locale.itIT, "Il Libro degli Eroi di Malfurion" },
          { Locale.ptBR, "O Livro dos Heróis de Malfurion" },
          { Locale.plPL, "Księga bohaterów Malfuriona" },
          { Locale.ptPT, "O Livro dos Heróis de Malfurion" },
          { Locale.jaJP, "マルフリオンの英雄伝説" },
          { Locale.thTH, "Malfurion's Book of Heroes" },
        }
      },
      {
        632, new Dictionary<Locale, string>() {
          { Locale.enUS, "Uther's Book of Heroes" },
          { Locale.enGB, "Uther's Book of Heroes" },
          { Locale.frFR, "Le Livre des héros de Uther" },
          { Locale.deDE, "Uthers Buch der Helden" },
          { Locale.koKR, "우서의 영웅 책" },
          { Locale.esES, "El libro de los héroes de Uther" },
          { Locale.esMX, "El libro de los héroes de Uther" },
          { Locale.ruRU, "«Книга героев» Утера" },
          { Locale.zhTW, "烏瑟爾英雄錄" },
          { Locale.zhCN, "乌瑟尔英雄录" },
          { Locale.itIT, "Il Libro degli Eroi di Uther" },
          { Locale.ptBR, "O Livro dos Heróis de Uther" },
          { Locale.plPL, "Księga bohaterów Uthera" },
          { Locale.ptPT, "O Livro dos Heróis de Uther" },
          { Locale.jaJP, "ユーザーズブックオブヒーローズ" },
          { Locale.thTH, "Uther's Book of Heroes" },
        }
      },
      {
        633, new Dictionary<Locale, string>() {
          { Locale.enUS, "Garrosh's Book of Heroes" },
          { Locale.enGB, "Garrosh's Book of Heroes" },
          { Locale.frFR, "Le Livre des héros de Garrosh" },
          { Locale.deDE, "Garroshs Buch der Helden" },
          { Locale.koKR, "영웅 개로스의 책" },
          { Locale.esES, "El libro de los héroes de Garrosh" },
          { Locale.esMX, "El libro de los héroes de Garrosh" },
          { Locale.ruRU, "«Книга героев» Гарроша" },
          { Locale.zhTW, "加爾魯什英雄錄" },
          { Locale.zhCN, "加尔鲁什的英雄录" },
          { Locale.itIT, "Il Libro degli Eroi di Garrosh" },
          { Locale.ptBR, "O Livro dos Heróis de Garrosh" },
          { Locale.plPL, "Księga bohaterów Garrosha" },
          { Locale.ptPT, "O Livro dos Heróis de Garrosh" },
          { Locale.jaJP, "ガローシュの英雄伝説" },
          { Locale.thTH, "Garrosh's Book of Heroes" },
        }
      },
      {
        634, new Dictionary<Locale, string>() {
          { Locale.enUS, "Anduin's Book of Heroes" },
          { Locale.enGB, "Anduin's Book of Heroes" },
          { Locale.frFR, "Le Livre des héros de Anduin" },
          { Locale.deDE, "Anduins Buch der Helden" },
          { Locale.koKR, "안두인의 영웅 책" },
          { Locale.esES, "El libro de los héroes de Anduin" },
          { Locale.esMX, "El libro de los héroes de Anduin" },
          { Locale.ruRU, "«Книга героев» Андуина" },
          { Locale.zhTW, "安度因英雄錄" },
          { Locale.zhCN, "安度因英雄录" },
          { Locale.itIT, "Il Libro degli Eroi di Anduin" },
          { Locale.ptBR, "O Livro dos Heróis de Anduin" },
          { Locale.plPL, "Księga bohaterów Anduina" },
          { Locale.ptPT, "O Livro dos Heróis de Anduin" },
          { Locale.jaJP, "アンドゥインの英雄伝説" },
          { Locale.thTH, "Anduin's Book of Heroes" },
        }
      },
      {
        635, new Dictionary<Locale, string>() {
          { Locale.enUS, "Valeera's Book of Heroes" },
          { Locale.enGB, "Valeera's Book of Heroes" },
          { Locale.frFR, "Le Livre des héros de Valeera" },
          { Locale.deDE, "Valeeras Buch der Helden" },
          { Locale.koKR, "발레라의 영웅 서적" },
          { Locale.esES, "El libro de los héroes de Valeera" },
          { Locale.esMX, "El libro de los héroes de Valeera" },
          { Locale.ruRU, "«Книга героев» Валиры" },
          { Locale.zhTW, "瓦萊拉的英雄錄" },
          { Locale.zhCN, "瓦莉拉的英雄录" },
          { Locale.itIT, "Il Libro degli Eroi di Valeera" },
          { Locale.ptBR, "O Livro dos Heróis de Valeera" },
          { Locale.plPL, "Księga bohaterów Valeery" },
          { Locale.ptPT, "O Livro dos Heróis de Valeera" },
          { Locale.jaJP, "ヴァレーラの英雄伝説" },
          { Locale.thTH, "Valeera's Book of Heroes" },
        }
      },
      {
        636, new Dictionary<Locale, string>() {
          { Locale.enUS, "Thrall's Book of Heroes" },
          { Locale.enGB, "Thrall's Book of Heroes" },
          { Locale.frFR, "Le Livre des héros de Thrall" },
          { Locale.deDE, "Thralls Buch der Helden" },
          { Locale.koKR, "트롤의 영웅 책" },
          { Locale.esES, "El libro de los héroes de Thrall" },
          { Locale.esMX, "El libro de los héroes de Thrall" },
          { Locale.ruRU, "«Книга героев» Тралла" },
          { Locale.zhTW, "薩爾英雄錄" },
          { Locale.zhCN, "萨尔英雄录" },
          { Locale.itIT, "Il Libro degli Eroi di Thrall" },
          { Locale.ptBR, "O Livro dos Heróis de Thrall" },
          { Locale.plPL, "Księga bohaterów Thralla" },
          { Locale.ptPT, "O Livro dos Heróis de Thrall" },
          { Locale.jaJP, "スロールズ-ブック-オブ-ヒー" },
          { Locale.thTH, "Thrall's Book of Heroes" },
        }
      },
      {
        637, new Dictionary<Locale, string>() {
          { Locale.enUS, "Gul'dan's Book of Heroes" },
          { Locale.enGB, "Gul'dan's Book of Heroes" },
          { Locale.frFR, "Le Livre des héros de Gul'dan" },
          { Locale.deDE, "Gul'dans Buch der Helden" },
          { Locale.koKR, "굴단의 영웅 책" },
          { Locale.esES, "El libro de los héroes de Gul'dan" },
          { Locale.esMX, "El libro de los héroes de Gul'dan" },
          { Locale.ruRU, "«Книга героев» Гул'дана" },
          { Locale.zhTW, "古爾丹英雄錄" },
          { Locale.zhCN, "古尔丹英雄录" },
          { Locale.itIT, "Il Libro degli Eroi di Gul'dan" },
          { Locale.ptBR, "O Livro dos Heróis de Gul'dan" },
          { Locale.plPL, "Księga bohaterów Gul'dana" },
          { Locale.ptPT, "O Livro dos Heróis de Gul'dan" },
          { Locale.jaJP, "Gul'danの英雄の本" },
          { Locale.thTH, "Gul'dan's Book of Heroes" },
        }
      },
      {
        638, new Dictionary<Locale, string>() {
          { Locale.enUS, "Illidan's Book of Heroes" },
          { Locale.enGB, "Illidan's Book of Heroes" },
          { Locale.frFR, "Le Livre des héros de Illidan" },
          { Locale.deDE, "Gul'dans Buch der Illidan" },
          { Locale.koKR, "일리단의 영웅 책" },
          { Locale.esES, "El libro de los héroes de Illidan" },
          { Locale.esMX, "El libro de los héroes de Illidan" },
          { Locale.ruRU, "«Книга героев» Иллидана" },
          { Locale.zhTW, "伊利丹的英雄錄" },
          { Locale.zhCN, "伊利丹的英雄录" },
          { Locale.itIT, "Il Libro degli Eroi di Illidan" },
          { Locale.ptBR, "O Livro dos Heróis de Illidan" },
          { Locale.plPL, "Księga bohaterów Illidana" },
          { Locale.ptPT, "O Livro dos Heróis de Illidan" },
          { Locale.jaJP, "イリダンのヒーローズブック" },
          { Locale.thTH, "Illidan's Book of Heroes" },
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
