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
    public class Settings : ContentPage
    {
        Label languageLabel = new Label { Text = "Язык:", FontFamily = "FiraSansRegular", FontSize = 16 };
        Picker languagePicker = new Picker { Title = "Выберите язык", FontFamily = "FiraSansRegular" };
        Label themeLabel = new Label { Text = "Тема:", FontFamily = "FiraSansRegular", FontSize = 16 };
        RadioButton lightRadioButton = new RadioButton { Content = "Светлая", GroupName = "Theme", FontFamily = "FiraSansRegular" };
        RadioButton darkRadioButton = new RadioButton { Content = "Темная", GroupName = "Theme", FontFamily = "FiraSansRegular" };
        public Settings()
        {
            if (Application.Current.Properties.ContainsKey("Theme"))
            {
                if (Application.Current.Properties["Theme"] as string == "Light") BackgroundColor = Color.White;
                if (Application.Current.Properties["Theme"] as string == "Dark") BackgroundColor = Color.FromRgb(45, 45, 45);
            }

            languagePicker.Items.Add("Русский");
            languagePicker.Items.Add("English");
            languagePicker.SelectedIndex = 0;
            if (Application.Current.Properties.ContainsKey("Language"))
            {
                if (Application.Current.Properties["Language"] as string == "Russian") languagePicker.SelectedIndex = 0;
                if (Application.Current.Properties["Language"] as string == "English") languagePicker.SelectedIndex = 1;
            } else
            {
                Application.Current.Properties["Language"] = "Russian";
                Application.Current.SavePropertiesAsync();
            }

            if (Application.Current.Properties.ContainsKey("Theme"))
            {
                if (Application.Current.Properties["Theme"] as string == "Light") lightRadioButton.IsChecked = true;
                if (Application.Current.Properties["Theme"] as string == "Dark") darkRadioButton.IsChecked = true;
            } else
            {
                Application.Current.Properties["Theme"] = "Light";
                Application.Current.SavePropertiesAsync();
                lightRadioButton.IsChecked = true;
            }

            if (Application.Current.Properties.ContainsKey("Language"))
            {
                if (Application.Current.Properties["Language"] as string == "Russian")
                {
                    languageLabel.Text = "Язык:";
                    languagePicker.Title = "Выберите язык";
                    lightRadioButton.Content = "Светлая";
                    darkRadioButton.Content = "Темная";
                    themeLabel.Text = "Тема:";
                } else
                {
                    languageLabel.Text = "Language";
                    languagePicker.Title = "Language";
                    lightRadioButton.Content = "Light";
                    darkRadioButton.Content = "Dark";
                    themeLabel.Text = "Theme";
                }
            }

            if (Application.Current.Properties.ContainsKey("Theme"))
            {
                if (Application.Current.Properties["Theme"] as string == "Light")
                {
                    languageLabel.TextColor = Color.Black;
                    languagePicker.TextColor = Color.Black;
                    lightRadioButton.TextColor = Color.Black;
                    darkRadioButton.TextColor = Color.Black;
                    themeLabel.TextColor = Color.Black;
                    BackgroundColor = Color.White;
                }
                else
                {
                    languageLabel.TextColor = Color.FromRgb(167, 167, 167);
                    languagePicker.TextColor = Color.FromRgb(167, 167, 167);
                    lightRadioButton.TextColor = Color.FromRgb(167, 167, 167);
                    darkRadioButton.TextColor = Color.FromRgb(167, 167, 167);
                    themeLabel.TextColor = Color.FromRgb(167, 167, 167);
                    BackgroundColor = Color.FromRgb(45, 45, 45);
                }
            }

            lightRadioButton.CheckedChanged += OnRadioButtonCheckedChanged;
            darkRadioButton.CheckedChanged += OnRadioButtonCheckedChanged;
            languagePicker.SelectedIndexChanged += OnPickerSelectedIndexChanged;

            var stackLayout = new StackLayout { Padding = 20 };
            stackLayout.Children.Add(languageLabel);
            stackLayout.Children.Add(languagePicker);
            stackLayout.Children.Add(themeLabel);
            stackLayout.Children.Add(lightRadioButton);
            stackLayout.Children.Add(darkRadioButton);



            Content = stackLayout;
        }
        void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton selectedRadioButton = sender as RadioButton;
            bool isChecked = e.Value;
            if (isChecked)
            {
                if (selectedRadioButton.Content as string == "Light" || selectedRadioButton.Content as string == "Светлая") {
                    languageLabel.TextColor = Color.Black;
                    languagePicker.TextColor = Color.Black;
                    lightRadioButton.TextColor = Color.Black;
                    darkRadioButton.TextColor = Color.Black;
                    themeLabel.TextColor = Color.Black;
                    BackgroundColor = Color.White;
                    Application.Current.Properties["Theme"] = "Light";
                    Application.Current.SavePropertiesAsync();
                    var app = Application.Current as App;
                    var m = app.m;
                    m.BarBackgroundColor = Color.FromRgb(96, 170, 232);
                } else
                {
                    languageLabel.TextColor = Color.FromRgb(167, 167, 167);
                    languagePicker.TextColor = Color.FromRgb(167, 167, 167);
                    lightRadioButton.TextColor = Color.FromRgb(167, 167, 167);
                    darkRadioButton.TextColor = Color.FromRgb(167, 167, 167);
                    themeLabel.TextColor = Color.FromRgb(167, 167, 167);
                    BackgroundColor = Color.FromRgb(45, 45, 45);
                    Application.Current.Properties["Theme"] = "Dark";
                    //Application.Current.UserAppTheme = OSAppTheme.Dark;
                    Application.Current.SavePropertiesAsync();
                    var app = Application.Current as App;
                    var m = app.m;
                    m.BarBackgroundColor = Color.FromRgb(45, 45, 45);
                }
                
            }
            else
            {

            }
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            Picker selectedPicker = sender as Picker;
            int selectedIndex = selectedPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                if (selectedPicker.SelectedIndex == 0)
                {
                    Application.Current.Properties["Language"] = "Russian";
                    languageLabel.Text = "Язык:";
                    languagePicker.Title = "Выберите язык";
                    lightRadioButton.Content = "Светлая";
                    darkRadioButton.Content = "Темная";
                    themeLabel.Text = "Тема:";
                    Application.Current.SavePropertiesAsync();
                } else
                {
                    Application.Current.Properties["Language"] = "English";
                    languageLabel.Text = "Language";
                    languagePicker.Title = "Language";
                    lightRadioButton.Content = "Light";
                    darkRadioButton.Content = "Dark";
                    themeLabel.Text = "Theme";
                    Application.Current.SavePropertiesAsync();
                }
            }
            else
            {

            }
        }

    }
}