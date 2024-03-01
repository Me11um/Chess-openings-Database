using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

[assembly: ExportFont("FiraSans-Regular.ttf", Alias = "FiraSansRegular")]
[assembly: ExportFont("FiraSans-Bold.ttf", Alias = "FiraSansBold")]
[assembly: ExportFont("FiraSans-SemiBold.ttf", Alias = "FiraSansSemiBold")]
[assembly: ExportFont("FiraSans-ExtraBold.ttf", Alias = "FiraSansExtraBold")]

namespace FinalProjectFR
{
    public class Opening : ContentPage
    {
        int curmove = 0;
        string game = "";
        string[,,] position = new string[20, 8, 8];

        Image board = new Image { Source = "Board.png" };
        AbsoluteLayout layout = new AbsoluteLayout();
        AbsoluteLayout chesspieces = new AbsoluteLayout { InputTransparent = true };
        AbsoluteLayout container = new AbsoluteLayout();
        List<Image> pieces = new List<Image>();

        Button prevbutton = new Button
        {
            Text = "<",
            BackgroundColor = Color.White,
            TextColor = Color.FromRgb(96, 170, 232),
            BorderColor = Color.FromRgb(96, 170, 232),
            CornerRadius = 5,
            FontFamily = "FiraSansBold",
            BorderWidth = 2,
            WidthRequest = 100,
            //HeightRequest = 40,
            FontSize = 20,
            IsEnabled = false
        };
            Button nextbutton = new Button
            {
                Text = ">",
                BackgroundColor = Color.White,
                TextColor = Color.FromRgb(96, 170, 232),
                BorderColor = Color.FromRgb(96, 170, 232),
                CornerRadius = 5,
                FontFamily = "FiraSansBold",
                BorderWidth = 2,
                WidthRequest = 100,
                //HeightRequest = 40,
                FontSize = 20
            };

        List<string> movelist = new List<string>();

        //string OpeningName = "Испанская партия";
        //string OpeningMoves = "1.e2-e4 e7-e5 2.Кg1-f3 Кb8-c6 3.Сf1-b5";
        //string OpeningDescription = "Это открытый дебют, в котором белые атакуют черного коня ходом Cb5 и готовятся захватить слабую пешку c6. Это один из самых теоретически разработанных и сложных дебютов в шахматах, который требует от обеих сторон точного знания идей и вариантов. Черные могут защищаться разными способами, например, 3…a6 (основной вариант), 3…Kf6 (берлинская защита), 3…d6 (стейницева защита), 3…g6 (система Смыслова) или 3…Kd4 (вариант Кордель).";
        Label OpName = new Label { FontFamily = "FiraSansBold", TextColor = Color.FromRgb(96, 170, 232), FontSize = 26, Padding = new Thickness(25, 0) };
        Label OpMoves = new Label { FontFamily = "FiraSansSemiBold", TextColor = Color.FromRgb(96, 170, 232), FontSize = 18, Padding = new Thickness(25, 0) };
        Label OpDescription = new Label { FontFamily = "FiraSansRegular", TextColor = Color.FromRgb(96, 170, 232), FontSize = 18, Padding = new Thickness(25, 15) };
        public Opening(string opn, string opm, string opg, string opd)
        {
            game = opg;
            OpName.Text = opn;
            OpMoves.Text = opm;
            OpDescription.Text = opd;
            AbsoluteLayout.SetLayoutBounds(board, new Rectangle(0.5, 10, 350, 350));
            AbsoluteLayout.SetLayoutFlags(board, AbsoluteLayoutFlags.XProportional);
            layout.Children.Add(board);
            prevbutton.Clicked += OnPrevButtonClicked;
            nextbutton.Clicked += OnNextButtonClicked;
            AbsoluteLayout.SetLayoutBounds(prevbutton, new Rectangle(0.3, 375, -1, -1));
            AbsoluteLayout.SetLayoutFlags(prevbutton, AbsoluteLayoutFlags.XProportional);
            layout.Children.Add(prevbutton);
            AbsoluteLayout.SetLayoutBounds(nextbutton, new Rectangle(0.7, 375, -1, -1));
            AbsoluteLayout.SetLayoutFlags(nextbutton, AbsoluteLayoutFlags.XProportional);
            layout.Children.Add(nextbutton);

            //AbsoluteLayout.SetLayoutBounds(OpName, new Rectangle(25, 450, -1, -1));
            //AbsoluteLayout.SetLayoutFlags(OpName, AbsoluteLayoutFlags.None);
            
            //stackLayout.Children.Add(OpDescription);

            

            //stackLayout.Children.Add(scrollView);

            //container.Children.Add(stackLayout, new Rectangle(0, 450, -1, -1), AbsoluteLayoutFlags.XProportional);
            container.Children.Add(layout, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            container.Children.Add(chesspieces, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);

            SetStartPosition(curmove);
            InitLayout();
            SliceMovesToList();

            AbsoluteLayout.SetLayoutBounds(chesspieces, new Rectangle(0.5, 10, 350, 350));
            AbsoluteLayout.SetLayoutFlags(chesspieces, AbsoluteLayoutFlags.XProportional);
            //chesspieces.ForceLayout();

            StackLayout stackLayout = new StackLayout();
            stackLayout.Children.Add(OpName);
            stackLayout.Children.Add(OpMoves);
            stackLayout.Children.Add(container);
            stackLayout.Children.Add(OpDescription);

            var scrollView = new ScrollView();
            scrollView.Orientation = ScrollOrientation.Vertical;
            scrollView.Content = stackLayout;

            Content = scrollView;
        }
        private async void OnPrevButtonClicked(object sender, EventArgs e)
        {
            if (curmove > 0) {
                var button = sender as Button;
                button.IsEnabled = false;
                nextbutton.IsEnabled = false;
                var moves = movelist[curmove - 1].Split(':');
                var temp = position[curmove, GetIFromCell(moves[1]), GetJFromCell(moves[1])];
                //position[curmove, GetIFromCell(moves[1]), GetJFromCell(moves[1])] = "";
                //position[curmove, GetIFromCell(moves[0]), GetJFromCell(moves[0])] = "";
                var image = chesspieces.Children[GetIFromCell(moves[1]) * 8 + GetJFromCell(moves[1])] as Image;
                image.Source = position[curmove - 1, GetIFromCell(moves[1]), GetJFromCell(moves[1])];
                image = chesspieces.Children[GetIFromCell(moves[0]) * 8 + GetJFromCell(moves[0])] as Image;
                image.Source = "";
                var piecemove = new Image { Source = temp };
                AbsoluteLayout.SetLayoutBounds(piecemove, new Rectangle(40 / 2 + 39 * GetJFromCell(moves[1]) - 2, 29 / 2 + 39 * GetIFromCell(moves[1]) + 4, 40, 40));
                AbsoluteLayout.SetLayoutFlags(piecemove, AbsoluteLayoutFlags.None);
                chesspieces.Children.Add(piecemove);
                await chesspieces.Children.Last().TranslateTo((40 / 2 + 39 * GetJFromCell(moves[0]) - 2) - (40 / 2 + 39 * GetJFromCell(moves[1]) - 2), (29 / 2 + 39 * GetIFromCell(moves[0]) + 4) - (29 / 2 + 39 * GetIFromCell(moves[1]) + 4), 150);
                //position[curmove, GetIFromCell(moves[0]), GetJFromCell(moves[0])] = temp;
                image = chesspieces.Children[GetIFromCell(moves[0]) * 8 + GetJFromCell(moves[0])] as Image;
                image.Source = temp;
                chesspieces.Children.Remove(chesspieces.Children.Last());
                curmove -= 1;
                button.IsEnabled = true;
                nextbutton.IsEnabled = true;
                if (curmove == 0) button.IsEnabled = false;
            }
        }

        private async void OnNextButtonClicked(object sender, EventArgs e)
        {
            if (movelist.Count > curmove) {
                var button = sender as Button;
                button.IsEnabled = false;
                prevbutton.IsEnabled = false;
                curmove += 1;
                var moves = movelist[curmove - 1].Split(':');
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        position[curmove, i, j] = position[curmove - 1, i, j];
                    }
                }
                var temp = position[curmove, GetIFromCell(moves[0]), GetJFromCell(moves[0])];
                position[curmove, GetIFromCell(moves[1]), GetJFromCell(moves[1])] = "";
                position[curmove, GetIFromCell(moves[0]), GetJFromCell(moves[0])] = "";
                var image = chesspieces.Children[GetIFromCell(moves[1]) * 8 + GetJFromCell(moves[1])] as Image;
                image.Source = "";
                image = chesspieces.Children[GetIFromCell(moves[0]) * 8 + GetJFromCell(moves[0])] as Image;
                image.Source = "";
                var piecemove = new Image { Source = temp };
                AbsoluteLayout.SetLayoutBounds(piecemove, new Rectangle(40 / 2 + 39 * GetJFromCell(moves[0]) - 2, 29 / 2 + 39 * GetIFromCell(moves[0]) + 4, 40, 40));
                AbsoluteLayout.SetLayoutFlags(piecemove, AbsoluteLayoutFlags.None);
                chesspieces.Children.Add(piecemove);
                await chesspieces.Children.Last().TranslateTo((40 / 2 + 39 * GetJFromCell(moves[1]) - 2) - (40 / 2 + 39 * GetJFromCell(moves[0]) - 2), (29 / 2 + 39 * GetIFromCell(moves[1]) + 4) - (29 / 2 + 39 * GetIFromCell(moves[0]) + 4), 150);
                position[curmove, GetIFromCell(moves[1]), GetJFromCell(moves[1])] = temp;
                image = chesspieces.Children[GetIFromCell(moves[1]) * 8 + GetJFromCell(moves[1])] as Image;
                image.Source = temp;
                chesspieces.Children.Remove(chesspieces.Children.Last());

                button.IsEnabled = true;
                prevbutton.IsEnabled = true;
                if (movelist.Count <= curmove) button.IsEnabled = false;
            }
        }

        int GetIFromCell(string cell)
        {
            int i = 0;
            i = 8 - int.Parse(cell[1].ToString());
            return i;
        }

        int GetJFromCell(string cell)
        {
            int j = 0;
            if (cell[0] == 'A') j = 0;
            if (cell[0] == 'B') j = 1;
            if (cell[0] == 'C') j = 2;
            if (cell[0] == 'D') j = 3;
            if (cell[0] == 'E') j = 4;
            if (cell[0] == 'F') j = 5;
            if (cell[0] == 'G') j = 6;
            if (cell[0] == 'H') j = 7;
            return j;
        }

        void SetStartPosition(int move)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    position[move, i, j] = "";
                }
            }

            position[move, 0, 0] = "br.png";
            position[move, 0, 1] = "bn.png";
            position[move, 0, 2] = "bb.png";
            position[move, 0, 3] = "bq.png";
            position[move, 0, 4] = "bk.png";
            position[move, 0, 5] = "bb.png";
            position[move, 0, 6] = "bn.png";
            position[move, 0, 7] = "br.png";

            position[move, 1, 0] = "bp.png";
            position[move, 1, 1] = "bp.png";
            position[move, 1, 2] = "bp.png";
            position[move, 1, 3] = "bp.png";
            position[move, 1, 4] = "bp.png";
            position[move, 1, 5] = "bp.png";
            position[move, 1, 6] = "bp.png";
            position[move, 1, 7] = "bp.png";

            position[move, 6, 0] = "wp.png";
            position[move, 6, 1] = "wp.png";
            position[move, 6, 2] = "wp.png";
            position[move, 6, 3] = "wp.png";
            position[move, 6, 4] = "wp.png";
            position[move, 6, 5] = "wp.png";
            position[move, 6, 6] = "wp.png";
            position[move, 6, 7] = "wp.png";

            position[move, 7, 0] = "wr.png";
            position[move, 7, 1] = "wn.png";
            position[move, 7, 2] = "wb.png";
            position[move, 7, 3] = "wq.png";
            position[move, 7, 4] = "wk.png";
            position[move, 7, 5] = "wb.png";
            position[move, 7, 6] = "wn.png";
            position[move, 7, 7] = "wr.png";
        }

        void SliceMovesToList()
        {
            var moves = game.Split(';');

            foreach (var move in moves)
            {
                movelist.Add(move);
            }
        }

        void InitLayout()
        {
            pieces.Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                        var image = new Image { Source = position[0, i, j] };
                        AbsoluteLayout.SetLayoutBounds(image, new Rectangle(40 / 2 + 39 * j - 2, 29 / 2 + 39 * i + 4, 40, 40));
                        AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.None);
                        chesspieces.Children.Add(image);
                }
            }
            layout.ForceLayout();

        }

        void RefreshLayout(int move)
        {
            //chesspieces.Children.Clear();
            //pieces.Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var image = chesspieces.Children[i * 8 + j] as Image;
                    image.Source = position[move, i, j];
                }
            }
            //chesspieces.ForceLayout();
        }

        void DrawPiecesFromPosition(int move)
        {
            chesspieces.Children.Clear();
            pieces.Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (position[move, i, j] != "")
                    {
                        pieces.Add(new Image { Source = position[move, i, j] });
                        AbsoluteLayout.SetLayoutBounds(pieces[pieces.Count - 1], new Rectangle(40 + 39 * j, 29 + 39 * i, 40, 40));
                        AbsoluteLayout.SetLayoutFlags(pieces[pieces.Count - 1], AbsoluteLayoutFlags.None);
                        chesspieces.Children.Add(pieces[pieces.Count - 1]);
                    }
                }
            }
            chesspieces.ForceLayout();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.Properties.ContainsKey("Theme"))
            {
                if (Application.Current.Properties["Theme"] as string == "Dark")
                {
                    BackgroundColor = Color.FromRgb(45, 45, 45);
                    prevbutton.BackgroundColor = Color.FromRgb(45, 45, 45);
                    prevbutton.TextColor = Color.FromRgb(167, 167, 167);
                    prevbutton.BorderColor = Color.FromRgb(167, 167, 167);
                    nextbutton.BackgroundColor = Color.FromRgb(45, 45, 45);
                    nextbutton.TextColor = Color.FromRgb(167, 167, 167);
                    nextbutton.BorderColor = Color.FromRgb(167, 167, 167);
                    OpName.TextColor = Color.FromRgb(167, 167, 167);
                    OpMoves.TextColor = Color.FromRgb(167, 167, 167);
                    OpDescription.TextColor = Color.FromRgb(167, 167, 167);
                    board.Source = "BoardBlack.png";
                }
            }
            if (Application.Current.Properties.ContainsKey("Language"))
            {
                if (Application.Current.Properties["Language"] as string == "English")
                {
                    //OpName.Text = "Spanish Game";
                    //OpDescription.Text = "This is an open debut, in which white attacks the black knight with Cb5 and prepares to capture the weak c6 pawn. This is one of the most theoretically developed and complex debuts in chess, which requires both sides to have precise knowledge of ideas and variants. Black can defend in different ways, for example, 3…a6 (main variant), 3…Kf6 (Berlin defense), 3…d6 (Steinitz defense), 3…g6 (Smyslov system) or 3…Kd4 (Cordel variant).";
                }
            }

        }
    }
}