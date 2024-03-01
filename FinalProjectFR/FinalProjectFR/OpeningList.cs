using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using SQLite;


[assembly: ExportFont("FiraSans-Regular.ttf", Alias = "FiraSansRegular")]
[assembly: ExportFont("FiraSans-Bold.ttf", Alias = "FiraSansBold")]
[assembly: ExportFont("FiraSans-SemiBold.ttf", Alias = "FiraSansSemiBold")]
[assembly: ExportFont("FiraSans-ExtraBold.ttf", Alias = "FiraSansExtraBold")]

namespace FinalProjectFR
{
    public class OpeningList : ContentPage
    {
        List<Openings> Op = new List<Openings>();
        List<Button> Buttons = new List<Button>();
        AbsoluteLayout absoluteLayout = new AbsoluteLayout();
        StackLayout stackLayout = new StackLayout();
        ScrollView scrollView = new ScrollView();
        public OpeningList()
        {
            scrollView.Orientation = ScrollOrientation.Vertical;
            scrollView.Content = stackLayout;


            //WriteOpenings();
            InitOpenings();
            //DeleteTable();
            if (Application.Current.Properties["Theme"] as string == "Dark")
            {
                BackgroundColor = Color.FromRgb(45, 45, 45);
            }

            stackLayout.Children.Add(absoluteLayout);
            Content = scrollView;
        }
        async void OnButtonClicked(object sender, EventArgs e)
        {
            int i = Buttons.IndexOf(sender as Button);
            Opening settingsPage = null;
            if (Application.Current.Properties["Language"] as string == "English")
            {
                settingsPage = new Opening(Op[i].NameEn, Op[i].PGNMoves, Op[i].Moves, Op[i].DescriptionEn);
            }
            else { settingsPage = new Opening(Op[i].Name, Op[i].PGNMoves, Op[i].Moves, Op[i].Description); }

            await Navigation.PushAsync(settingsPage, false);
        }
        async void InitOpenings()
        {
            Op = await App.OpDataBase.GetOpeningsAsync();

            for (int i = 0; i < Op.Count; i++)
            {
                Button op = new Button
                {
                    Text = "",
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 70,
                    BackgroundColor = Color.White,
                    TextColor = Color.FromRgb(96, 170, 232),
                    BorderColor = Color.FromRgb(96, 170, 232),
                    BorderWidth = 1,
                    CornerRadius = 0,
                    Padding = 0,
                    Margin = 0,
                    Opacity = 0.01
                };
                if (Application.Current.Properties["Theme"] as string == "Dark")
                {
                    //op.BackgroundColor = Color.White;
                }
                op.Clicked += OnButtonClicked;

                Buttons.Add(op);

                AbsoluteLayout.SetLayoutBounds(op, new Rectangle(0, 0 + 71 * i, 1, -1));
                AbsoluteLayout.SetLayoutFlags(op, AbsoluteLayoutFlags.WidthProportional);
                absoluteLayout.Children.Add(op);

                Label lname = new Label
                {
                    FontFamily = "FiraSansBold",
                    TextColor = Color.FromRgb(96, 170, 232),
                    FontSize = 26,
                    Text = Op[i].Name,
                    HorizontalTextAlignment = TextAlignment.Start,
                    InputTransparent = true
                };
                if (Application.Current.Properties["Theme"] as string == "Dark")
                {
                    lname.TextColor = Color.FromRgb(167, 167, 167);
                }
                if (Application.Current.Properties["Language"] as string == "English")
                {
                    lname.Text = Op[i].NameEn;
                    if (lname.Text == "Italian Game: Two Knights Defense") lname.Text = "Two Knights Defense";
                }
                AbsoluteLayout.SetLayoutBounds(lname, new Rectangle(15, 5 + 71 * i, -1, -1));
                AbsoluteLayout.SetLayoutFlags(lname, AbsoluteLayoutFlags.None);
                absoluteLayout.Children.Add(lname);

                Label lmoves = new Label
                {
                    FontFamily = "FiraSansBold",
                    TextColor = Color.FromRgb(96, 170, 232),
                    FontSize = 14,
                    Text = Op[i].PGNMoves,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Opacity = 0.5,
                    InputTransparent = true
                };
                if (Application.Current.Properties["Theme"] as string == "Dark")
                {
                    lmoves.TextColor = Color.FromRgb(167, 167, 167);
                }
                AbsoluteLayout.SetLayoutBounds(lmoves, new Rectangle(15, 40 + 71 * i, -1, -1));
                AbsoluteLayout.SetLayoutFlags(lmoves, AbsoluteLayoutFlags.None);
                absoluteLayout.Children.Add(lmoves);

                BoxView line = new BoxView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 1,
                    Color = Color.FromRgb(96, 170, 232)
                };
                if (Application.Current.Properties["Theme"] as string == "Dark")
                {
                    line.Color = Color.FromRgb(167, 167, 167);
                }
                AbsoluteLayout.SetLayoutBounds(line, new Rectangle(0, 71 + 71 * i, 1, -1));
                AbsoluteLayout.SetLayoutFlags(line, AbsoluteLayoutFlags.WidthProportional);
                absoluteLayout.Children.Add(line);
            }
            absoluteLayout.ForceLayout();
            stackLayout.ForceLayout();
            scrollView.ForceLayout();
        }
    }
}