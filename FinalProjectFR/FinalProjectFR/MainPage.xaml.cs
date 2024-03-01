using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: ExportFont("FiraSans-Regular.ttf", Alias = "FiraSansRegular")]
[assembly: ExportFont("FiraSans-Bold.ttf", Alias = "FiraSansBold")]
[assembly: ExportFont("FiraSans-SemiBold.ttf", Alias = "FiraSansSemiBold")]
[assembly: ExportFont("FiraSans-ExtraBold.ttf", Alias = "FiraSansExtraBold")]

namespace FinalProjectFR
{
    public partial class MainPage : ContentPage
    {
        Image logo = new Image { Source = "Logo.png" };
        Button opbutton = new Button
        {
            Text = "Дебюты",
            BackgroundColor = Color.White,
            TextColor = Color.FromRgb(96, 170, 232),
            BorderColor = Color.FromRgb(96, 170, 232),
            CornerRadius = 10,
            FontFamily = "FiraSansBold",
            BorderWidth = 2,
            WidthRequest = 225,
            HeightRequest = 75,
            FontSize = 24
        };
        Button settingsbutton = new Button
        {
            Text = "Настройки",
            BackgroundColor = Color.White,
            TextColor = Color.FromRgb(96, 170, 232),
            BorderColor = Color.FromRgb(96, 170, 232),
            CornerRadius = 10,
            FontFamily = "FiraSansBold",
            BorderWidth = 2,
            WidthRequest = 225,
            HeightRequest = 75,
            FontSize = 24
        };
        public MainPage()
        {
            if (!Application.Current.Properties.ContainsKey("Language"))
            {
                Application.Current.Properties["Language"] = "Russian";
            }

            if (!Application.Current.Properties.ContainsKey("Theme"))
            {
                Application.Current.Properties["Theme"] = "Light";
            }
            if (Application.Current.Properties.ContainsKey("Theme"))
            {
                if (Application.Current.Properties["Theme"] as string == "Light") BackgroundColor = Color.White;
                if (Application.Current.Properties["Theme"] as string == "Dark") BackgroundColor = Color.FromRgb(45, 45, 45);
            }
            InitializeComponent();
            WriteOpenings();
            settingsbutton.Clicked += OnSettingsButtonClicked;
            opbutton.Clicked += OnOpButtonClicked;
            AbsoluteLayout layout = new AbsoluteLayout();

            AbsoluteLayout.SetLayoutBounds(logo, new Rectangle(0.5, 75, 250, 250));
            AbsoluteLayout.SetLayoutFlags(logo, AbsoluteLayoutFlags.XProportional);
            layout.Children.Add(logo);
            AbsoluteLayout.SetLayoutBounds(opbutton, new Rectangle(0.5, 375, -1, -1));
            AbsoluteLayout.SetLayoutFlags(opbutton, AbsoluteLayoutFlags.XProportional);
            layout.Children.Add(opbutton);
            AbsoluteLayout.SetLayoutBounds(settingsbutton, new Rectangle(0.5, 500, -1, -1));
            AbsoluteLayout.SetLayoutFlags(settingsbutton, AbsoluteLayoutFlags.XProportional);
            layout.Children.Add(settingsbutton);

            Content = layout;
        }
        private async void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            var settingsPage = new Settings();

            await Navigation.PushAsync(settingsPage, false);
        }
        private async void OnOpButtonClicked(object sender, EventArgs e)
        {
            //var settingsPage = new Opening();
            var settingsPage = new OpeningList();

            await Navigation.PushAsync(settingsPage, false);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.Properties.ContainsKey("Theme"))
            {
                if (Application.Current.Properties["Theme"] as string == "Light")
                {
                    BackgroundColor = Color.White;
                    opbutton.BackgroundColor = Color.White;
                    opbutton.TextColor = Color.FromRgb(96, 170, 232);
                    opbutton.BorderColor = Color.FromRgb(96, 170, 232);
                    settingsbutton.BackgroundColor = Color.White;
                    settingsbutton.TextColor = Color.FromRgb(96, 170, 232);
                    settingsbutton.BorderColor = Color.FromRgb(96, 170, 232);
                    logo.Source = "Logo.png";
                    var app = Application.Current as App;
                    var m = app.m;
                    m.BarBackgroundColor = Color.FromRgb(96, 170, 232);
                }
                if (Application.Current.Properties["Theme"] as string == "Dark")
                {
                    BackgroundColor = Color.FromRgb(45, 45, 45);
                    opbutton.BackgroundColor = Color.FromRgb(45, 45, 45);
                    opbutton.TextColor = Color.FromRgb(167, 167, 167);
                    opbutton.BorderColor = Color.FromRgb(167, 167, 167);
                    settingsbutton.BackgroundColor = Color.FromRgb(45, 45, 45);
                    settingsbutton.TextColor = Color.FromRgb(167, 167, 167);
                    settingsbutton.BorderColor = Color.FromRgb(167, 167, 167);
                    logo.Source = "LogoGray.png";
                    var app = Application.Current as App;
                    var m = app.m;
                    m.BarBackgroundColor = Color.FromRgb(45, 45, 45);
                }
            }
            if (Application.Current.Properties.ContainsKey("Language"))
            {
                if (Application.Current.Properties["Language"] as string == "Russian")
                {
                    opbutton.Text = "Дебюты";
                    settingsbutton.Text = "Настройки";

                }
                if (Application.Current.Properties["Language"] as string == "English")
                {
                    opbutton.Text = "Openings";
                    settingsbutton.Text = "Settings";
                }
            }

        }
        async void WriteOpenings()
        {
            App.OpDataBase.CreateTable();
            List<Openings> op = await App.OpDataBase.GetOpeningsAsync();

            if (op.Count == 0)
            {

                await App.OpDataBase.SaveOpeningsAsync(new Openings
                {
                    Name = "Итальянская партия",
                    PGNMoves = "1.e4 e5 2.Кf3 Кc6 3.Сc4 Сc5",
                    Moves = "E2:E4;E7:E5;G1:F3;B8:C6;F1:C4;F8:C5",
                    Description = "Это один из старейших и самых популярных открытых дебютов среди начинающих и профессиональных шахматистов. Идея дебюта - быстро развить фигуры и атаковать слабый пункт f7 у черных с помощью слона c4 и коня f3. Белые могут также применять различные гамбитные варианты, связанные с жертвой пешки за инициативу (например, гамбит Эванса - 4.b4). Черные же стремятся удержать равновесие в центре и защитить своего слона c5.",
                    NameEn = "Italian Game",
                    DescriptionEn = "This is one of the oldest and most popular open chess openings among beginners and professional chess players. The idea of the chess opening is to quickly develop the pieces and attack the weak point f7 of the black with the help of the bishop c4 and the knight f3. White can also use various gambit options related to the sacrifice of a pawn for initiative (for example, Evans gambit - 4.b4). Black, on the other hand, strive to maintain balance in the center and protect their bishop c5."
                });
                await App.OpDataBase.SaveOpeningsAsync(new Openings
                {
                    Name = "Шотландская партия",
                    PGNMoves = "1.e4 e5 2.Кf3 Кc6 3.d4 exd4",
                    Moves = "E2:E4;E7:E5;G1:F3;B8:C6;D2:D4;E5:D4",
                    Description = "Это открытый дебют, в котором белые стремятся устранить центральную пешку черных и захватить центр ходом d4. После размена на d4 белые открывают диагонали для двух слонов, что дает им возможность быстро развернуть свои силы. Черные же могут контратаковать ходом Кb4+ или d5, создавая напряжение в центре.",
                    NameEn = "Scotch Game",
                    DescriptionEn = "This is an open chess opening in which white seeks to eliminate the central pawn of black and capture the center with d4. After exchanging on d4, white opens diagonals for both bishops, which gives them the opportunity to quickly deploy their forces. Black can counterattack with Kb4+ or d5, creating tension in the center."
                });
                await App.OpDataBase.SaveOpeningsAsync(new Openings
                {
                    Name = "Французская защита",
                    PGNMoves = "1.e4 e6 2.d4 d5",
                    Moves = "E2:E4;E7:E6;D2:D4;D7:D5",
                    Description = "Это полуоткрытый дебют, в котором черные защищают свой слабый пункт f7 ходом e6 и контратакуют в центре ходом d5. Недостатком этого дебюта является запертый на c8 белопольный слон, которого трудно ввести в игру. Белые могут выбирать между различными системами игры против французской защиты, такими как обменный вариант (3.exd5 exd5), вариант с Кf3 (3.Kf3), вариант с Kc3 (3.Kc3), вариант с Сd3 (3.Cd3) или вариант с Сg5 (3.Cg5).",
                    NameEn = "French Defense",
                    DescriptionEn = "This is a semi-open chess opening in which black defends their weak point f7 with e6 and counterattacks in the center with d5. The disadvantage of this chess opening is the locked on c8 white-field bishop, which is difficult to bring into play. White can choose between different systems of play against the French defense, such as exchange variant (3.exd5 exd5), variant with Kf3 (3.Kf3), variant with Kc3 (3.Kc3), variant with Cd3 (3.Cd3) or variant with Cg5 (3.Cg5)."
                });
                await App.OpDataBase.SaveOpeningsAsync(new Openings
                {
                    Name = "Скандинавская защита",
                    PGNMoves = "1.e4 d5 2.exd5 Qxd5",
                    Moves = "E2:E4;D7:D5;E4:D5;D8:D5",
                    Description = "Это полуоткрытый дебют, в котором черные жертвуют свою центральную пешку за быстрый вывод ферзя и создание фигурной контригры в центре. Риск этого дебюта заключается в том, что ферзь черных может быть атакован белыми фигурами и потерять темпы. Белые могут играть против скандинавской защиты разными способами, например, 3.Кf3, 3.Сc4, 3.Сb5+ или 3.d4.",
                    NameEn = "Scandinavian Defense",
                    DescriptionEn = "This is a semi-open chess opening in which black sacrifices their central pawn for a quick exit of the queen and creates a figurative counterplay in the center. The risk of this chess opening is that the black queen can be attacked by white pieces and lose tempo. White can play against Scandinavian defense in different ways, such as 3.Kf3, 3.Cc4, 3.Cb5+ or 3.d4."
                });
                await App.OpDataBase.SaveOpeningsAsync(new Openings
                {
                    Name = "Дебют четырех коней",
                    PGNMoves = "1.e4 e5 2.Кf3 Кc6 3.Kc3 Кf6",
                    Moves = "E2:E4;E7:E5;G1:F3;B8:C6;B1:C3;G8:F6",
                    Description = "Это открытый дебют, который очень прост для понимания. Он начинается с вывода четырех коней к центру поля, что дает обоим сторонам пространственное преимущество и возможность для различных тактических приемов. Белые могут продолжать игру ходами 4.Cc4 (итальянская система), 4.d4 (скотч), 4.g3 (система Глейга) или 4.Cd5 (вариант Белграда). Черные же могут отклониться от дебюта четырех коней ходами 3…Сb4 (испанская система) или 3…g6 (система Глейга).",
                    NameEn = "Four Knights Game",
                    DescriptionEn = "This is an open chess opening that is very easy to understand. It starts with bringing out four knights to the center of the field, which gives both sides spatial advantage and opportunity for various tactical techniques. White can continue playing moves 4.Cc4 (Italian system), 4.d4 (Scotch), 4.g3 (Gleig system) or 4.Cd5 (Belgrade variant). Black can deviate from the four knights chess opening moves 3…Cb4 (Spanish system) or 3…g6 (Gleig system)."
                });
                await App.OpDataBase.SaveOpeningsAsync(new Openings
                {
                    Name = "Славянская защита",
                    PGNMoves = "1.d4 d5 2.c4 c6",
                    Moves = "D2:D4;D7:D5;C2:C4;C7:C6",
                    Description = "Это полуоткрытый дебют, в котором черные укрепляют свою центральную пешку ходом c6 и готовятся к контратаке в центре или на ферзевом фланге. Преимуществом этого дебюта является прочная пешечная структура и незапертый белопольный слон. Недостатком - отставание в развитии и потенциальная слабость пешки c6. Белые могут играть против славянской защиты разными способами, например, 3.Kf3 (разменная система), 3.e3 (меранский вариант), 3.Kc3 (славянский гамбит) или 3.cxd5 cxd5 (разменный вариант)",
                    NameEn = "Slav Defense",
                    DescriptionEn = "This is a semi-open chess opening in which black strengthens their central pawn with c6 and prepares to counterattack in the center or on the queen side. The advantage of this chess opening is a solid pawn structure and an unblocked white-field bishop. The disadvantage is a lag in development and a potential weakness of the pawn c6. White can play against the Slav defense in different ways, such as 3.Kf3 (exchange system), 3.e3 (Meran variant), 3.Kc3 (Slav gambit) or 3.cxd5 cxd5 (exchange variant)."
                });
                await App.OpDataBase.SaveOpeningsAsync(new Openings
                {
                    Name = "Сицилианская защита",
                    PGNMoves = "1.e4 c5",
                    Moves = "E2:E4;C7:C5",
                    Description = "Это полуоткрытый дебют, в котором черные игнорируют борьбу за центральные поля и атакуют белую пешку e4 ходом c5. Это самый популярный дебют среди сильных шахматистов, так как он дает черным шансы на активную игру и компенсацию за отставание в развитии. Белые могут выбирать между множеством систем игры против сицилианской защиты, таких как открытая система (2.Kf3), закрытая система (2.Kc3), система Алапина (2.c3), система Морра (2.d4 cxd4 3.c3) или система Россолимо (2.Kf3 Kc6 3.Cb5)",
                    NameEn = "Sicilian Defense",
                    DescriptionEn = "This is a semi-open chess opening in which black ignores the fight for the central fields and attacks the white pawn e4 with c5. This is the most popular chess opening among strong chess players, as it gives black chances for active play and compensation for lagging in development. White can choose between many systems of play against the Sicilian defense, such as open system (2.Kf3), closed system (2.Kc3), Alapin system (2.c3), Morra system (2.d4 cxd4 3.c3) or Rossolimo system (2.Kf3 Kc6 3.Cb5)."
                });
                await App.OpDataBase.SaveOpeningsAsync(new Openings
                {
                    Name = "Королевский гамбит",
                    PGNMoves = "1.e4 e5 2.f4",
                    Moves = "E2:E4;E7:E5;F2:F4",
                    Description = "Это открытый дебют, в котором белые жертвуют свою королевскую пешку ходом f4 за быстрое развитие фигур и атаку на королевском фланге. Это один из старейших и самых агрессивных дебютов в шахматах, который был популярен в XIX веке, но потерял свою актуальность в XX веке из-за научного анализа и оборонительных методов черных. Черные могут принять гамбит ходом exf4 или отклонить его ходами d5 или Kf6",
                    NameEn = "King's Gambit",
                    DescriptionEn = "This is an open chess opening in which white sacrifices their king’s pawn with f4 for quick development of pieces and attack on the king’s side. This is one of the oldest and most aggressive chess openings, which was popular in the 19th century, but lost its relevance in the 20th century due to scientific analysis and defensive methods of black. Black can accept the gambit with exf4 or decline it with moves d5 or Kf6."
                });
                await App.OpDataBase.SaveOpeningsAsync(new Openings
                {
                    Name = "Испанская партия",
                    PGNMoves = "1.e4 e5 2.Kf3 Kc6 3.Cb5",
                    Moves = "E2:E4;E7:E5;G1:F3;B8:C6;F1:B5",
                    Description = "Это открытый дебют, в котором белые атакуют черного коня ходом Cb5 и готовятся захватить слабую пешку c6. Это один из самых теоретически разработанных и сложных дебютов в шахматах, который требует от обеих сторон точного знания идей и вариантов. Черные могут защищаться разными способами, например, 3…a6 (основной вариант), 3…Kf6 (берлинская защита), 3…d6 (стейницева защита), 3…g6 (система Смыслова) или 3…Kd4 (вариант Кордель)",
                    NameEn = "Ruy López Opening",
                    DescriptionEn = "This is an open chess opening in which white attacks the black knight with Cb5 and prepares to capture the weak pawn c6. This is one of the most theoretically developed and complex chess openings, which requires both sides to know the ideas and variants. Black can defend in different ways, such as 3…a6 (main variant), 3…Kf6 (Berlin defense), 3…d6 (Steinitz defense), 3…g6 (Smyslov system) or 3…Kd4 (Cordel variant)."
                });
                await App.OpDataBase.SaveOpeningsAsync(new Openings
                {
                    Name = "Защита двух коней",
                    PGNMoves = "1.e4 e5 2.Kf3 Kc6 3.Cc4 Kf6",
                    Moves = "E2:E4;E7:E5;G1:F3;B8:C6;F1:C4;G8:F6",
                    Description = "Это открытый дебют, в котором черные выводят своих коней к центру поля, не боясь угрозы белого слона c4. Это динамичный и рискованный дебют, в котором черные могут попасть под сильную атаку белых, но также имеют возможность контратаки. Белые могут играть против защиты двух коней разными способами, например, 4.d4 (вариант Лолли), 4.Kg5 (вариант Фрид-Ливер), 4.Cd5 (вариант Ульвестад) или 4.d3 (современный вариант)",
                    NameEn = "Italian Game: Two Knights Defense",
                    DescriptionEn = "This is an open chess opening in which black brings out their knights to the center of the field, not afraid of the threat of the white bishop c4. This is a dynamic and risky chess opening, in which black can fall under a strong attack of white, but also have a possibility of counterattack. White can play against the two knights defense in different ways, such as 4.d4 (Lolli variant), 4.Kg5 (Fried Liver variant), 4.Cd5 (Ulvestad variant) or 4.d3 (modern variant)."
                });
                await App.OpDataBase.SaveOpeningsAsync(new Openings
                {
                    Name = "Защита Каро-Канн",
                    PGNMoves = "1.e4 c6",
                    Moves = "E2:E4;C7:C6",
                    Description = "Это полуоткрытый дебют, в котором черные контролируют центральное поле d5 ходом c6 и готовятся к размену белой пешки e4. Это прочный и надежный дебют, который дает черным шансы на равную игру и компенсацию за отставание в развитии. Белые могут играть против защиты Каро-Канн разными способами, например, 2.d4 (атака Панова), 2.e5 (закрытая система), 2.Kc3 (система Нимцовича), 2.Kf3 (классическая система) или 2.d3 (система Петросяна-Смыслова)",
                    NameEn = "Caro-Kann Defense",
                    DescriptionEn = "This is a semi-open chess opening in which black controls the central field d5 with c6 and prepares to exchange the white pawn e4. This is a solid and reliable chess opening, which gives black chances for equal play and compensation for lagging in development. White can play against the Caro-Kann defense in different ways, such as 2.d4 (Panov attack), 2.e5 (closed system), 2.Kc3 (Nimzowitsch system), 2.Kf3 (classical system) or 2.d3 (Petrosian-Smyslov system)"
                });
                await App.OpDataBase.SaveOpeningsAsync(new Openings
                {
                    Name = "Ферзевый гамбит",
                    PGNMoves = "1.d4 d5 2.c4",
                    Moves = "D2:D4;D7:D5;C2:C4",
                    Description = "Это открытый дебют, в котором белые жертвуют свою королевскую пешку ходом c4 за быстрое развитие фигур и захват центра. Это один из самых классических и популярных дебютов в шахматах, который требует от обеих сторон знания теории и стратегии. Черные могут принять гамбит ходом dxc4 или отклонить его ходами e6 (защита Ортодокс), c6 (защита Славянская), Kf6 (защита Тартаковера) или e5 (защита Альбина)",
                    NameEn = "Queen's Gambit",
                    DescriptionEn = "This is an open chess opening in which white sacrifices their king’s pawn with c4 for quick development of pieces and capture of the center. This is one of the most classic and popular chess openings, which requires both sides to know theory and strategy. Black can accept the gambit with dxc4 or decline it with moves e6 (Orthodox defense), c6 (Slav defense), Kf6 (Tartakower defense) or e5 (Albin defense)"
                });
                await App.OpDataBase.SaveOpeningsAsync(new Openings
                {
                    Name = "Индийская защита",
                    PGNMoves = "1.d4 Kf6",
                    Moves = "D2:D4;G8:F6",
                    Description = "Это полуоткрытый дебют, в котором черные игнорируют борьбу за центральные поля и готовятся к контратаке с помощью своего ферзевого слона. Это современный и гибкий дебют, который позволяет черным адаптироваться к различным планам белых. Белые могут играть против индийской защиты разными способами, например, 2.c4 (закрытая система), 2.Kf3 (система Торре), 2.Cg5 (атака Тромповского), 2.Cf3 (система Лондон) или 2.g3 (система Каталония)",
                    NameEn = "Indian Game",
                    DescriptionEn = "This is a semi-open chess opening in which black ignores the fight for the central fields and prepares to counterattack with their queen’s bishop. This is a modern and flexible chess opening, which allows black to adapt to different plans of white. White can play against the Indian defense in different ways, such as 2.c4 (closed system), 2.Kf3 (Torre system), 2.Cg5 (Trompowsky attack), 2.Cf3 (London system) or 2.g3 (Catalan system)"
                });
                await App.OpDataBase.SaveOpeningsAsync(new Openings
                {
                    Name = "Защита Грюнфельда",
                    PGNMoves = "1.d4 Kf6 2.c4 g6 3.Kc3 d5",
                    Moves = "D2:D4;G8:F6;C2:C4;G7:G6;B1:C3;D7:D5",
                    Description = "Это полуоткрытый дебют, в котором черные атакуют белый центр ходом d5 и готовятся к размену белой пешки c4. Это динамичный и острый дебют, в котором черные рассчитывают на активную игру своих фигур и давление на длинных диагоналях. Белые могут играть против защиты Грюнфельда разными способами, например, 4.cxd5 Kxd5 5.e4 (обменный вариант), 4.Cf3 (система Русская), 4.e3 (система Самиша), 4.Cg5 (система Сочинская) или 4.g3 (система Флора)",
                    NameEn = "Grünfeld Defense",
                    DescriptionEn = "This is a semi-open chess opening in which black attacks the white center with d5 and prepares to exchange the white pawn c4. This is a dynamic and sharp chess opening, in which black rely on active play of their pieces and pressure on long diagonals. White can play against the Grunfeld defense in different ways, such as 4.cxd5 Kxd5 5.e4 (exchange variant), 4.Cf3 (Russian system), 4.e3 (Samisch system), 4.Cg5 (Sochi system) or 4.g3 (Flohr system)"
                });
                await App.OpDataBase.SaveOpeningsAsync(new Openings
                {
                    Name = "Защита Алехина",
                    PGNMoves = "1.e4 Kf6",
                    Moves = "E2:E4;G8:F6",
                    Description = "Это полуоткрытый дебют, в котором черные провоцируют белых на продвижение своих центральных пешек ходом Kf6 и готовятся к контратаке с помощью своих фигур. Это острый и рискованный дебют, который требует от черных точного знания теории и тактики. Белые могут играть против защиты Алехина разными способами, например, 2.e5 (основной вариант), 2.d4 (современный вариант), 2.Kc3 (вариант Чейса), 2.Cf3 (вариант Ларсена) или 2.Cc3 (вариант Фурмана)",
                    NameEn = "Alekhine's Defense",
                    DescriptionEn = "This is a semi-open chess opening in which black provokes white to advance their central pawns with Kf6 and prepares to counterattack with their pieces. This is a sharp and risky chess opening, which requires black to know theory and tactics precisely. White can play against the Alekhine defense in different ways, such as 2.e5 (main variant), 2.d4 (modern variant), 2.Kc3 (Chase variant), 2.Cf3 (Larsen variant) or 2.Cc3 (Furman variant)"
                });

            }

        }
        async void DeleteTable()
        {
            await App.OpDataBase.DeleteOpeningsAsync();
        }
        async void ClearTable()
        {
            await App.OpDataBase.ClearOpeningsAsync();
        }
        
    }
        

    }
