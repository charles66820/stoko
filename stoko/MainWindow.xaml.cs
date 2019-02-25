using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace stoko {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            setLanguageDictionary();
            InitializeComponent();
        }

        private void setLanguageDictionary(String pLang = null) {
            ResourceDictionary resDict = new ResourceDictionary();

            pLang = pLang == null? Thread.CurrentThread.CurrentCulture.ToString() : pLang;

            switch (pLang) {
                case "en-US":
                    resDict.Source = new Uri("..\\Assets\\langs\\StringResources.en-US.xaml", UriKind.Relative);
                    break;
                case "fr-FR":
                    resDict.Source = new Uri("..\\Assets\\langs\\StringResources.fr-FR.xaml", UriKind.Relative);
                    break;
                default:
                    resDict.Source = new Uri("..\\Assets\\langs\\StringResources.en-US.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(resDict);
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
            MainMenu.Visibility = Visibility.Visible;
            MainGrid.Margin = new Thickness(0,20,0,20);
        }
        private void CheckBoxUnAllowMenu_Checked(object sender, RoutedEventArgs e) {
            MainMenu.Visibility = Visibility.Hidden;
            MainGrid.Margin = new Thickness(0, 0, 0, 20);
        }
        private void CheckBoxDarkTheme_Checked(object sender, RoutedEventArgs e) {
            ResourceDictionary resDict = new ResourceDictionary();
            resDict.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml", UriKind.Absolute);
            this.Resources.MergedDictionaries.Add(resDict);
        }
        private void CheckBoxLightTheme_Checked(object sender, RoutedEventArgs e) {
            ResourceDictionary resDict = new ResourceDictionary();
            resDict.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml", UriKind.Absolute);
            this.Resources.MergedDictionaries.Add(resDict);
        }

        private void ComboBoxLang_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cbLang.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Français") {
                setLanguageDictionary("fr-FR");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            } else {
                setLanguageDictionary("en-US");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            }
        }
    }
}
