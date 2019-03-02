using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace stoko {
    /// <summary>
    /// File with Functional method for MainWindow.xaml.cs
    /// </summary>
    public partial class MainWindow : Window {
        private void initSettings() {

            //initialise les langues
            foreach (Lang l in Configs.Langs) {
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
    }
}
