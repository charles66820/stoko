using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace stoko {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {

            //initialise la configuration
            Configs.Init();
            Configs.SetLanguageDictionary(Configs.Data.Global["lang"]);

            InitializeComponent();
             
            //initialise les langues
            foreach(Lang l in Configs.Langs) {
                cbLang.Items.Add(l);
            }
            cbLang.DisplayMemberPath = "Name";

            //affiche la langue dans le combo box
            Lang curentLang = new Lang("en-US", "English");
            foreach (Lang l in Configs.Langs) {
                if (l.Code == Configs.Data.Global["lang"]) {
                    curentLang = l;
                }
            }
            cbLang.SelectedItem = curentLang;

            //coche la checkbox mainMenu
            bool c = false;
            if (Configs.Data.Global["mainMenu"] == "1") c = true;
            setMenu(c);
            allowMenu.IsChecked = c;

            //coche la checkbox darkMode
            c = false;
            if (Configs.Data.Global["darkMode"] == "1") c = true;
            Configs.SetDarkMode(c);
            allowDarkMode.IsChecked = c;
        }

        /// <summary>
        /// show/hide main menu
        /// </summary>
        /// <param name="p"></param>
        private void setMenu(bool p = true) {
            if (p) {
                MainMenu.Visibility = Visibility.Visible;
                MainGrid.Margin = new Thickness(0, 20, 0, 20);
                Configs.EditConfigData("mainMenu", "1");
            } else {
                MainMenu.Visibility = Visibility.Hidden;
                MainGrid.Margin = new Thickness(0, 0, 0, 20);
                Configs.EditConfigData("mainMenu", "0");
            }
        }

        private void Button_GotFocus(object sender, RoutedEventArgs e) {
            popMenu.Visibility = Visibility.Visible;
        }

        private void Button_LostFocus(object sender, RoutedEventArgs e) {
            popMenu.Visibility = Visibility.Hidden;
        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e) {
            SettingPanel.Visibility = Visibility.Visible;
        }

        private void Button_SettingsClose_Click(object sender, RoutedEventArgs e) {
            SettingPanel.Visibility = Visibility.Hidden;
        }

        private void Close_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void CheckBoxAllowMenu_Checked(object sender, RoutedEventArgs e) {
            setMenu();
        }
        private void CheckBoxUnAllowMenu_Checked(object sender, RoutedEventArgs e) {
            setMenu(false);
        }
        private void CheckBoxDarkTheme_Checked(object sender, RoutedEventArgs e) {
            Configs.SetDarkMode();
        }
        private void CheckBoxLightTheme_Checked(object sender, RoutedEventArgs e) {
            Configs.SetDarkMode(false);
        }

        private void ComboBoxLang_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if (((Lang)cbLang.SelectedItem).Code == "fr-FR") {
                Configs.SetLanguageDictionary("fr-FR");
                Configs.EditConfigData("lang", "fr-FR");
            } else {
                Configs.SetLanguageDictionary("en-US");
                Configs.EditConfigData("lang", "en-US");

            }
        }
    }
}
