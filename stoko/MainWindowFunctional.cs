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

        #region settings
        private void initSettings() {

            //initialise les langues
            cbLang.Items.Clear();
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

            DFhost.Text = Configs.Data.Global["dbHost"];
            DFname.Text = Configs.Data.Global["dbName"];
            DFuser.Text = Configs.Data.Global["dbUser"];
            DFpass.Text = Configs.Data.Global["dbPassword"];
        }

        private void setSettingsPanel(bool p = true) {
            if (p) {
                initSettings();
                SettingPanel.Visibility = Visibility.Visible;
            } else {
                SettingPanel.Visibility = Visibility.Hidden;
            }
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
        #endregion

        #region product
        #region actions
        private void loadProducts() {
            Data.Products = DbProduct.GetProducts();
            dgProducts.ItemsSource = Data.Products;
            List<Category> categories =  DbProduct.GetCategories();
            categories.Add(new Category(-1, Application.Current.Resources["cNone"] as String));

            PFCat.ItemsSource = categories;
            PFCat.DisplayMemberPath = "Title";

            bAddProduct.IsEnabled = true;
        }
        #endregion
        #region form
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
            PFCat.SelectedIndex = -1;
            PFDes.Text = String.Empty;

            setProductForm(p);
        }

        /// <summary>
        /// Fill product info befor edit
        /// </summary>
        /// <param name="product"></param>
        private void editProductForm(Product product) {
            PFTitle.Text = product.Title;
            PFPriceHT.Text = product.PriceHT.ToString();
            PFQentity.Text = product.Quantity.ToString();
            PFRef.Text = product.Reference;
            if (product.Category == null) {
                PFCat.SelectedIndex = -1;
            } else {
                PFCat.Text = product.Category.Title;
            }
            PFDes.Text = product.Description;

            PFDone.SetResourceReference(ContentControl.ContentProperty, "bDone");
            PFDelete.SetResourceReference(ContentControl.ContentProperty, "bDelete");
            PFDelete.SetResourceReference(ContentControl.StyleProperty, "MaterialDesignRaisedAccentButton");

            setProductForm();
        }

        private void addProductForm() {
            dgProducts.SelectedIndex = -1;
            resetProductForm(true);
            PFDone.SetResourceReference(ContentControl.ContentProperty, "bAdd");
            PFDelete.SetResourceReference(ContentControl.ContentProperty, "bCancel");
            PFDelete.SetResourceReference(ContentControl.StyleProperty, "MaterialDesignRaisedButton");
        }
        #endregion
        #endregion

        #region client
        #region actions
        private void loadClients() {
            Data.Clients = DbClient.GetClients();
            dgClients.ItemsSource = Data.Clients;

            bAddClient.IsEnabled = true;
        }
        #endregion
        #region form
        /// <summary>
        /// Show or hide client form
        /// </summary>
        /// <param name="p"></param>
        private void setClientForm(bool p = true) {
            if (p) {
                ClientForm.Visibility = Visibility.Visible;
            } else {
                ClientForm.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Reset client form
        /// </summary>
        /// <param name="p">hide form after reset. true by default</param>
        private void resetClientForm(bool p = false) {
            CFClientLogin.Text = String.Empty;
            CFClientEmail.Text = String.Empty;
            CFClientName.Text = String.Empty;
            CFClientFirstName.Text = String.Empty;
            CFClientPhoneNumber.Text = String.Empty;

            setClientForm(p);
        }

        /// <summary>
        /// Fill client info befor edit
        /// </summary>
        /// <param name="client"></param>
        private void editClientForm(Client client) {
            CFClientLogin.Text = client.Login;
            CFClientEmail.Text = client.Email;
            CFClientName.Text = client.LastName;
            CFClientFirstName.Text = client.FirstName;
            CFClientPhoneNumber.Text = client.PhoneNumber;

            CFDone.SetResourceReference(ContentControl.ContentProperty, "bDone");
            CFDelete.SetResourceReference(ContentControl.ContentProperty, "bDelete");
            CFDelete.SetResourceReference(ContentControl.StyleProperty, "MaterialDesignRaisedAccentButton");

            setClientForm();

            DbClient.GetAddresses(client);
            dgAddresses.ItemsSource = client.Addresses;

            bAddAddress.IsEnabled = true;
        }

        private void addClientForm() {
            dgClients.SelectedIndex = -1;
            dgAddresses.ItemsSource = null;
            resetAddressForm();
            resetClientForm(true);
            bAddClient.IsEnabled = true;

            CFDone.SetResourceReference(ContentControl.ContentProperty, "bAdd");
            CFDelete.SetResourceReference(ContentControl.ContentProperty, "bCancel");
            CFDelete.SetResourceReference(ContentControl.StyleProperty, "MaterialDesignRaisedButton");
        }

        /// <summary>
        /// Show or hide address form
        /// </summary>
        /// <param name="p"></param>
        private void setAddressForm(bool p = true) {
            if (p) {
                AddressForm.Visibility = Visibility.Visible;
            } else {
                AddressForm.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Reset address form
        /// </summary>
        /// <param name="p">hide form after reset. true by default</param>
        private void resetAddressForm(bool p = false) {
            AFWay.Text = String.Empty;
            AFComplement.Text = String.Empty;
            AFZipCode.Text = String.Empty;
            AFCity.Text = String.Empty;

            setAddressForm(p);
        }

        /// <summary>
        /// Fill address info befor edit
        /// </summary>
        /// <param name="address"></param>
        private void editAddressForm(Address address) {
            AFWay.Text = address.Way;
            AFComplement.Text = address.Complement;
            AFZipCode.Text = address.ZipCode;
            AFCity.Text = address.City;

            AFDone.SetResourceReference(ContentControl.ContentProperty, "bDone");
            AFDelete.SetResourceReference(ContentControl.ContentProperty, "bDelete");
            AFDelete.SetResourceReference(ContentControl.StyleProperty, "MaterialDesignRaisedAccentButton");

            setAddressForm();
        }

        private void addAddressForm() {
            dgAddresses.SelectedIndex = -1;
            resetAddressForm(true);
            AFDone.SetResourceReference(ContentControl.ContentProperty, "bAdd");
            AFDelete.SetResourceReference(ContentControl.ContentProperty, "bCancel");
            AFDelete.SetResourceReference(ContentControl.StyleProperty, "MaterialDesignRaisedButton");
        }
        #endregion
        #endregion

        #region order
        #region actions
        private void loadOrders() {
            Data.Orders = DbOrder.GetOrders();
            dgOrder.ItemsSource = Data.Orders;
        }
        #endregion
        #region form
        /// <summary>
        /// Show or hide order form
        /// </summary>
        /// <param name="p"></param>
        private void setChangeOrderAddressForm(bool p = true) {
            if (p) {
                ChangeOrderAddressForm.Visibility = Visibility.Visible;
            } else {
                ChangeOrderAddressForm.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Reset address form
        /// </summary>
        /// <param name="p">hide form after reset. true by default</param>
        private void resetChangeOrderAddressForm(bool p = false) {
            DAFAddress.ItemsSource = null;
            bShipTheOrder.IsEnabled = false;

            setChangeOrderAddressForm(p);
        }

        /// <summary>
        /// Fill address info befor edit
        /// </summary>
        /// <param name="order"></param>
        private void changeOrderAddressForm(Order order) {
            DAFAddress.ItemsSource = DbClient.GetAddresses(new Client(order.ClientId));
            DAFAddress.DisplayMemberPath = "FullAddress";
            foreach (Address a in DAFAddress.Items) {
                if (a.Id == order.Address.Id) {
                    DAFAddress.SelectedItem = a;
                }
            }

            DAFDone.SetResourceReference(ContentControl.ContentProperty, "bDone");
            DAFCancel.SetResourceReference(ContentControl.ContentProperty, "bCancel");

            setChangeOrderAddressForm();
        }
        #endregion
        #endregion

        /// <summary>
        /// Call method for load tab with tab name
        /// </summary>
        /// <param name="tabName"></param>
        private void loadTab(String tabName) {
            switch (tabName) {
                case "tiProduct":
                    loadProducts();
                    break;
                case "tiClient":
                    loadClients();
                    break;
                case "tiOrder":
                    loadOrders();
                    break;
            }
        }

        /// <summary>
        /// Connect to db
        /// </summary>
        /// <returns></returns>
        private bool connectDb() {
            Data.DbURL = "SERVER=" + Configs.Data.Global["dbHost"] + "; DATABASE=" + Configs.Data.Global["dbName"] + "; UID=" + Configs.Data.Global["dbUser"] + "; PASSWORD=" + Configs.Data.Global["dbPassword"];

            if (Data.DbConnect()) {
                msgStatut.SetResourceReference(ContentControl.ContentProperty, Data.DbConnStatus);
                msgStatut.SetResourceReference(ContentControl.ForegroundProperty, "MaterialDesignBody");
                return true;
            } else {
                MessageBox.Show(Data.DbConnMsg, Application.Current.Resources[Data.DbConnStatus] as String, MessageBoxButton.OK, MessageBoxImage.Stop);
                msgStatut.SetResourceReference(ContentControl.ContentProperty, Data.DbConnStatus);
                msgStatut.SetResourceReference(ContentControl.ForegroundProperty, "ValidationErrorBrush");
                return false;
            }
        }
    }
}
