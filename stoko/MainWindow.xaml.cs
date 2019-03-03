using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using stoko_class_BLL;
using stoko_db_BLL;

namespace stoko {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {

            //initialise la configuration
            Configs.Init();

            InitializeComponent();

            initSettings();
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

        private void bAddProduct_Click(object sender, RoutedEventArgs e) {
            addProductForm();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {

            if (connectDb()) {
                MessageBox.Show((mainTC.SelectedItem as TabItem).Name);
            } else {
                SettingPanel.Visibility = Visibility.Visible;
                bSettingsClose.IsEnabled = false;
            }
        }
    }
}
