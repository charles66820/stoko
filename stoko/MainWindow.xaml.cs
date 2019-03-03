﻿using System;
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
            setSettingsPanel();
        }

        private void Button_SettingsClose_Click(object sender, RoutedEventArgs e) {
            setSettingsPanel(false);
        }

        private void Close_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            Data.CloseConn();
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
            if (cbLang.SelectedItem != null) {
                if (((Lang)cbLang.SelectedItem).Code == "fr-FR") {
                    Configs.SetLanguageDictionary("fr-FR");
                    Configs.EditConfigData("lang", "fr-FR");
                } else {
                    Configs.SetLanguageDictionary("en-US");
                    Configs.EditConfigData("lang", "en-US");
                }
            }
        }

        private void bAddProduct_Click(object sender, RoutedEventArgs e) {
            addProductForm();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            if (connectDb()) {
                loadTab((mainTC.SelectedItem as TabItem).Name);
                bSettingsClose.IsEnabled = true;
            } else {
                setSettingsPanel();
                bSettingsClose.IsEnabled = false;
            }
        }

        private void bDbConnect_Click(object sender, RoutedEventArgs e) {
            Configs.EditConfigData("dbHost", DFhost.Text);
            Configs.EditConfigData("dbName", DFname.Text);
            Configs.EditConfigData("dbUser", DFuser.Text);
            Configs.EditConfigData("dbPassword", DFpass.Text);

            if (connectDb()) {
                loadTab((mainTC.SelectedItem as TabItem).Name);
                bSettingsClose.IsEnabled = true;
            } else {
                bSettingsClose.IsEnabled = false;
            }
        }

        private void MainTC_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (mainTC.IsLoaded && e.Source as TabControl != null) {
                loadTab((mainTC.SelectedItem as TabItem).Name);
            }
        }

        private void DgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (dgProducts.SelectedItem as Product != null) editProductForm(dgProducts.SelectedItem as Product);
        }

        private void PFDone_Click(object sender, RoutedEventArgs e) {
            //TODO: unit test

            Product product = dgProducts.SelectedIndex == -1? new Product() : dgProducts.SelectedItem as Product;
            product.Title = PFTitle.Text;
            product.PriceHT = int.Parse(PFPriceHT.Text);
            product.Quantity = int.Parse(PFQentity.Text);
            product.Reference = PFRef.Text;
            product.Category = PFCat.SelectedItem as Category;
            product.Description = PFDes.Text;

            if (dgProducts.SelectedIndex == -1) Data.Products.Add(product);

            dgProducts.Items.Refresh();
        }

        private void PFDelete_Click(object sender, RoutedEventArgs e) {
            if (dgProducts.SelectedIndex != -1) {
                Data.Products.Remove(dgProducts.SelectedItem as Product);
                dgProducts.Items.Refresh();
            }
            resetProductForm();
        }
    }
}
