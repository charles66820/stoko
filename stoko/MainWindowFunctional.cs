using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using stoko_class_BLL;
using stoko_db_BLL;

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
                SettingPanel.Margin = new Thickness(0, 20, 0, 20);
                Configs.EditConfigData("mainMenu", "1");
            } else {
                MainMenu.Visibility = Visibility.Hidden;
                MainGrid.Margin = new Thickness(0, 0, 0, 20);
                SettingPanel.Margin = new Thickness(0, 0, 0, 20);
                Configs.EditConfigData("mainMenu", "0");
            }
        }

        /// <summary>
        /// Show or hide product form
        /// </summary>
        /// <param name="p"></param>
        private void setProductForm(bool p = true) {
            if (p) {
                ProductForm.Visibility = Visibility.Visible;
            } else {
                ProductForm.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Reset product form
        /// </summary>
        /// <param name="p">hide form after reset. true by default</param>
        private void resetProductForm(bool p = false) {
            PFTitle.Text = String.Empty;
            PFPriceHT.Text = String.Empty;
            PFQentity.Text = String.Empty;
            PFRef.Text = String.Empty;
            PFDes.Text = String.Empty;

            setProductForm(p);
        }

        /// <summary>
        /// Fill product info beafor edit
        /// </summary>
        /// <param name="product"></param>
        private void editProductForm(Product product) {
            PFTitle.Text = product.Title;
            PFPriceHT.Text = product.PriceHT.ToString();
            PFQentity.Text = product.Quantity.ToString();
            PFRef.Text = product.Reference;
            PFCat.SelectedItem = product.Category;
            PFDes.Text = product.Description;

            PFDone.SetResourceReference(ContentControl.ContentProperty, "bAdd");
            PFDelete.SetResourceReference(ContentControl.ContentProperty, "bCancel");
            PFDelete.SetResourceReference(ContentControl.StyleProperty, "MaterialDesignRaisedButton");

            setProductForm();
        }

        private void addProductForm() {
            resetProductForm(true);
            PFDone.SetResourceReference(ContentControl.ContentProperty, "bDone");
            PFDelete.SetResourceReference(ContentControl.ContentProperty, "bDelete");
            PFDelete.SetResourceReference(ContentControl.StyleProperty, "MaterialDesignRaisedAccentButton");
        }

        private bool connectDb() {
            Data.DbURL = "SERVER=" + Configs.Data.Global["dbHost"] + "; DATABASE=" + Configs.Data.Global["dbName"] + "; UID=" + Configs.Data.Global["dbUser"] + "; PASSWORD=" + Configs.Data.Global["dbPassword"];

            if (Data.DbConnect()) {
                msgStatut.SetResourceReference(ContentControl.ContentProperty, Data.DbConnStatus);
                msgStatut.SetResourceReference(ContentControl.ForegroundProperty, "MaterialDesignBody");
                return true;
            } else {
                MessageBox.Show(Data.DbConnMsg, (String)Application.Current.Resources[Data.DbConnStatus], MessageBoxButton.OK, MessageBoxImage.Stop);
                msgStatut.SetResourceReference(ContentControl.ContentProperty, Data.DbConnStatus);
                msgStatut.SetResourceReference(ContentControl.ForegroundProperty, "ValidationErrorBrush");
                return false;
            }
        }
    }
}
