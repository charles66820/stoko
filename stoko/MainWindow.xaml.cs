using stoko_class_BLL;
using stoko_db_BLL;
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

            InitializeComponent();

            initSettings();
        }

        #region UI
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

        private void imgSrvUrlButton_Click(object sender, RoutedEventArgs e) {
            Configs.EditConfigData("imgSrvUrl", imgSrvUrl.Text);
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

        private void MainTC_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (mainTC.IsLoaded && e.Source as TabControl != null) {
                loadTab((mainTC.SelectedItem as TabItem).Name);
            }
        }
        #endregion

        #region products tab
        private void bAddProduct_Click(object sender, RoutedEventArgs e) {
            addProductForm();
        }

        private void DgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (dgProducts.SelectedItem as Product != null) {
                editProductForm(dgProducts.SelectedItem as Product);
            } else {
                resetProductForm();
            }
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

            if (dgProducts.SelectedIndex == -1) {
                Data.Products.Add(product);
                product.Id = DbProduct.CreateProduct(product);
                resetProductForm();
            } else {
                DbProduct.UpdateProduct(product);
            }

            dgProducts.Items.Refresh();
        }

        private void PFDelete_Click(object sender, RoutedEventArgs e) {
            if (dgProducts.SelectedIndex != -1) {
                Product p = dgProducts.SelectedItem as Product;
                Data.Products.Remove(p);
                DbProduct.DeleteProduct(p);
                dgProducts.Items.Refresh();
            }
            resetProductForm();
        }
        #endregion

        #region clients tab
        private void BAddClient_Click(object sender, RoutedEventArgs e) {
            addClientForm();
        }

        private void BAddAddress_Click(object sender, RoutedEventArgs e) {
            addAddressForm();
        }

        private void CFDone_Click(object sender, RoutedEventArgs e) {
            //TODO: unit test

            Client client = dgClients.SelectedIndex == -1 ? new Client() : dgClients.SelectedItem as Client;
            client.Login = CFClientLogin.Text;
            client.Email = CFClientEmail.Text;
            client.LastName = CFClientName.Text;
            client.FirstName = CFClientFirstName.Text;
            client.PhoneNumber = CFClientPhoneNumber.Text;

            if (dgClients.SelectedIndex == -1) {
                Data.Clients.Add(client);
                client.Id = DbClient.CreateClient(client);
                resetClientForm();
            } else {
                DbClient.UpdateClient(client);
            }

            dgClients.Items.Refresh();
        }

        private void CFDelete_Click(object sender, RoutedEventArgs e) {
            if (dgClients.SelectedIndex != -1) {
                Client c = dgClients.SelectedItem as Client;
                Data.Clients.Remove(c);
                DbClient.DeleteClient(c);

                dgClients.Items.Refresh();
                dgAddresses.ItemsSource = null;
                dgAddresses.Items.Refresh();
            }
            resetClientForm();
            resetAddressForm();
            bAddClient.IsEnabled = true;
        }

        private void DgClients_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            resetAddressForm();
            dgAddresses.ItemsSource = null;
            bAddAddress.IsEnabled = false;
            if (dgClients.SelectedItem as Client != null) {
                editClientForm(dgClients.SelectedItem as Client);
            } else {
                resetClientForm();
                bAddClient.IsEnabled = false;
            }
        }

        private void DgAddresses_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (dgAddresses.SelectedItem as Address != null) {
                editAddressForm(dgAddresses.SelectedItem as Address);
            } else {
                resetAddressForm();
            }
        }

        private void AFDone_Click(object sender, RoutedEventArgs e) {
            //TODO: unit test

            Address address = dgAddresses.SelectedIndex == -1 ? new Address() : dgAddresses.SelectedItem as Address;
            address.Way = AFWay.Text;
            address.Complement = AFComplement.Text;
            address.ZipCode = AFZipCode.Text;
            address.City = AFCity.Text;

            if (dgAddresses.SelectedIndex == -1) {
                Client c = dgClients.SelectedItem as Client;
                c.Addresses.Add(address);
                address.Client = c;
                address.Id = DbClient.CreateAddress(address);
                resetAddressForm();
            } else {
                DbClient.UpdateAddress(address);
            }

            dgAddresses.Items.Refresh();
        }

        private void AFDelete_Click(object sender, RoutedEventArgs e) {
            if (dgAddresses.SelectedIndex != -1) {
                Address a = dgAddresses.SelectedItem as Address;
                (dgClients.SelectedItem as Client).Addresses.Remove(a);
                DbClient.DeleteAddress(a);
                dgAddresses.Items.Refresh();
            }
            resetAddressForm();
        }
        #endregion

        #region orders tab
        private void DgOrder_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            dgOrderContent.ItemsSource = null;
            dgOrderContent.Items.Refresh();
            Order o = dgOrder.SelectedItem as Order;
            if (o != null) {
                changeOrderAddressForm(o);
                if (o.Shipped == 0) {
                    bShipTheOrder.IsEnabled = true;
                } else {
                    bShipTheOrder.IsEnabled = false;
                }
                if (o.Address.Id != -1) {
                    DAFDone.IsEnabled = true;
                } else {
                    DAFDone.IsEnabled = false;
                }

                if (o.OrderContents == null) o.OrderContents = DbOrder.GetOrdersContent(o);

                dgOrderContent.ItemsSource = o.OrderContents;
                dgOrderContent.Items.Refresh();
            } else {
                resetChangeOrderAddressForm();
            }
        }

        private void DAFAddress_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DAFAddress.SelectedIndex != -1) {
                DAFDone.IsEnabled = true;
            } else {
                DAFDone.IsEnabled = false;
            }
        }

        private void DAFDone_Click(object sender, RoutedEventArgs e) {
            Order o = dgOrder.SelectedItem as Order;
            o.Address = DAFAddress.SelectedItem as Address;
            DbOrder.UpdateOrderedAddress(o);
            dgOrder.Items.Refresh();
        }

        private void DAFCancel_Click(object sender, RoutedEventArgs e) {
            changeOrderAddressForm(dgOrder.SelectedItem as Order);
        }

        private void BShipTheOrder_Click(object sender, RoutedEventArgs e) {
            Order o = dgOrder.SelectedItem as Order;
            o.Shipped = 1;
            bShipTheOrder.IsEnabled = false;
            DbOrder.UpdateShipOrder(o);
            dgOrder.Items.Refresh();
        }
        #endregion
    }
}
