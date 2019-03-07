using Microsoft.Win32;
using stoko_class_BLL;
using stoko_db_BLL;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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

        private void Button_Click(object sender, RoutedEventArgs e) {
            Configs.EditConfigData("imgSrvUrl", accessToken.Text);
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

        private void AddressForm_SizeChanged(object sender, SizeChangedEventArgs e) {
            if (AddressForm.ActualHeight > 300) {
                AFWay.Width = 259;
                AFWay.Margin = new Thickness(0, 4, 0, 0);
                AFComplement.Width = 259;
                AFComplement.Margin = new Thickness(0, 69, 0, 0);
                AFZipCode.Width = 259;
                AFZipCode.Margin = new Thickness(0, 134, 0, 0);
                AFCity.Width = 259;
                AFCity.Margin = new Thickness(0, 199, 0, 0);
            } else {
                AFWay.Width = 129;
                AFWay.Margin = new Thickness(0, 4, 0, 0);
                AFComplement.Width = 125;
                AFComplement.Margin = new Thickness(134, 4, 0, 0);
                AFZipCode.Width = 129;
                AFZipCode.Margin = new Thickness(0, 69, 0, 0);
                AFCity.Width = 125;
                AFCity.Margin = new Thickness(134, 69, 0, 0);
            }
        }
        #endregion

        #region products tab
        private void bAddProduct_Click(object sender, RoutedEventArgs e) {
            addProductForm();
        }

        private void DgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            lbPictures.ItemsSource = null;
            if (dgProducts.SelectedItem as Product != null) {
                Product p = dgProducts.SelectedItem as Product;
                editProductForm(p);

                p.Pictures = DbProduct.GetPictures(p);
                lbPictures.ItemsSource = p.Pictures;
                bAddImage.IsEnabled = true;
            } else {
                resetProductForm();
                bAddImage.IsEnabled = false;
            }
            lbPictures.Items.Refresh();
        }

        private void LbPictures_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lbPictures.SelectedItem as Picture != null) {
                imgViewPicture.Source = new BitmapImage(
                    new Uri(Configs.Data.Global["imgSrvUrl"] + "img/products/" + (lbPictures.SelectedItem as Picture).PictureName, UriKind.Absolute)
                );
                bDeleteImages.IsEnabled = true;
            } else {
                imgViewPicture.Source = null;
                bDeleteImages.IsEnabled = false;
            }
        }

        private void BDeleteImages_Click(object sender, RoutedEventArgs e) {
            if (lbPictures.SelectedItem as Picture != null) {
                Picture p = lbPictures.SelectedItem as Picture;

                //delet image in the server
                try {
                    WebRequest req = WebRequest.Create(
                            Configs.Data.Global["imgSrvUrl"] + "product/picture?name=" +
                            p.PictureName + "&token=" +
                            Configs.Data.Global["accessToken"]
                        );
                    req.GetResponse();
                } catch { }

                DbProduct.DeletePicture(p);
                p.Product.Pictures.Remove(p);
                lbPictures.Items.Refresh();
            }
        }

        private void PFDone_Click(object sender, RoutedEventArgs e) {
            double price;
            double.TryParse(PFPriceHT.Text, out price);

            int qte;
            int.TryParse(PFQentity.Text, out qte);

            if (PFTitle.Text == String.Empty) {
                MessageBox.Show(Application.Current.Resources["mpfTitleNull"] as String);
            } else if (PFTitle.Text.Length < 2) {
                MessageBox.Show(Application.Current.Resources["mpfTitleMin"] as String);
            } else if (PFTitle.Text.Length > 100) {
                MessageBox.Show(Application.Current.Resources["mpfTitleMax"] as String);
            } else if (PFPriceHT.Text == String.Empty || price == 0) {
                MessageBox.Show(Application.Current.Resources["mpfPriceNull"] as String);
            } else if (PFRef.Text == String.Empty) {
                MessageBox.Show(Application.Current.Resources["mpfRefnull"] as String);
            } else if (PFRef.Text.Length < 6) {
                MessageBox.Show(Application.Current.Resources["mpfRefMin"] as String);
            } else if (PFRef.Text.Length > 100) {
                MessageBox.Show(Application.Current.Resources["mpfRefMax"] as String);
            } else if (PFDes.Text.Length > 1000) {
                MessageBox.Show(Application.Current.Resources["mpfDesMax"] as String);
            } else {
                Product product = dgProducts.SelectedIndex == -1? new Product() : dgProducts.SelectedItem as Product;
                product.Title = PFTitle.Text;
                product.PriceHT = price;
                product.Quantity = qte;
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
        }

        private void PFDelete_Click(object sender, RoutedEventArgs e) {
            if (dgProducts.SelectedIndex != -1) {
                Product p = dgProducts.SelectedItem as Product;
                //delete product images in the server
                foreach (Picture pic in p.Pictures) {
                    try {
                        WebRequest req = WebRequest.Create(
                            Configs.Data.Global["imgSrvUrl"] + "product/picture?name=" + 
                            pic.PictureName + "&token=" + 
                            Configs.Data.Global["accessToken"]
                        );
                        req.GetResponse();
                    } catch { }
                }
                
                Data.Products.Remove(p);
                DbProduct.DeleteProduct(p);
                dgProducts.Items.Refresh();
            }
            resetProductForm();
        }

        private void BAddImage_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images|*.png;*.jpg;*.jpeg;*.gif";

            if (openFileDialog.ShowDialog() == true) {
                if (dgProducts.SelectedIndex != -1) {
                    Product p = dgProducts.SelectedItem as Product;

                    //sende image to server
                    WebClient webCli = new WebClient();
                    try {
                        byte[] responseArray = webCli.UploadFile(
                            Configs.Data.Global["imgSrvUrl"] + "product/" + p.Id.ToString() + "/picture?token=" + Configs.Data.Global["accessToken"],
                            "POST", 
                            openFileDialog.FileName
                        );
                        String res = System.Text.Encoding.ASCII.GetString(responseArray);
                        if (res.Equals("none")) {
                            MessageBox.Show(Application.Current.Resources["sErrorImageSending"] as String);
                        } else {
                            Picture newPic = new Picture();
                            newPic.PictureName = res;
                            newPic.UpdateAt = DateTime.Now;
                            newPic.Product = p;

                            newPic.Id = DbProduct.CreatePicture(newPic);

                            p.Pictures.Add(newPic);
                            lbPictures.Items.Refresh();
                        }
                    } catch {
                        MessageBox.Show(Application.Current.Resources["sErrorImageSending"] as String);
                    }
                }
            }
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
            if (CFClientLogin.Text == String.Empty) {
                MessageBox.Show(Application.Current.Resources["mcfLoginNull"] as String);
            } else if (CFClientLogin.Text.Length < 2) {
                MessageBox.Show(Application.Current.Resources["mcfLoginMin"] as String);
            } else if (CFClientLogin.Text.Length > 20) {
                MessageBox.Show(Application.Current.Resources["mcfLoginMax"] as String);
            } else if (CFClientEmail.Text == String.Empty) {
                MessageBox.Show(Application.Current.Resources["mcfEmailNull"] as String);
            } else if (CFClientEmail.Text.Length < 5) {
                MessageBox.Show(Application.Current.Resources["mcfEmailMin"] as String);
            } else if (CFClientEmail.Text.Length > 100) {
                MessageBox.Show(Application.Current.Resources["mcfEmailMax"] as String);
            } else if (CFClientName.Text.Length > 50) {
                MessageBox.Show(Application.Current.Resources["mcfLastNameMax"] as String);
            } else if (CFClientFirstName.Text.Length > 50) {
                MessageBox.Show(Application.Current.Resources["mcfFirstNameMax"] as String);
            } else if (CFClientPhoneNumber.Text.Length > 13) {
                MessageBox.Show(Application.Current.Resources["mcfPhoneNumberMax"] as String);
            } else {

                Client client = dgClients.SelectedIndex == -1 ? new Client() : dgClients.SelectedItem as Client;
                client.Login = CFClientLogin.Text;
                client.Email = CFClientEmail.Text;
                client.LastName = CFClientName.Text;
                client.FirstName = CFClientFirstName.Text;
                client.PhoneNumber = CFClientPhoneNumber.Text;

                if (dgClients.SelectedIndex == -1) {
                    Data.Clients.Add(client);
                    client.Id = DbClient.CreateClient(client);

                    //send confirme email
                    try {
                        WebRequest req = WebRequest.Create(
                            Configs.Data.Global["imgSrvUrl"] + "login/new/" + client.Id.ToString() +
                            "/" + Configs.Data.Global["accessToken"]
                        );
                        req.GetResponse().ToString();
                    } catch { }

                    resetClientForm();
                } else {
                    DbClient.UpdateClient(client);
                }

                dgClients.Items.Refresh();
            }
        }

        private void CFDelete_Click(object sender, RoutedEventArgs e) {
            if (dgClients.SelectedIndex != -1) {
                Client c = dgClients.SelectedItem as Client;

                //delete image in server
                try {
                    WebRequest req = WebRequest.Create(
                            Configs.Data.Global["imgSrvUrl"] + "client/picture?name=" +
                            c.Avatar + "&token=" +
                            Configs.Data.Global["accessToken"]
                        );
                    req.GetResponse();
                } catch { }

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

        private void CFResetPassword_Click(object sender, RoutedEventArgs e) {
            if (dgClients.SelectedIndex != -1) {
                Client c = dgClients.SelectedItem as Client;
                try {
                    WebRequest req = WebRequest.Create(
                        Configs.Data.Global["imgSrvUrl"] + "password/reset/" + 
                        c.Id.ToString() + "/" + Configs.Data.Global["accessToken"]
                    );
                    req.GetResponse();
                } catch { }
            }
        }

        private void DgClients_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            resetAddressForm();
            dgAddresses.ItemsSource = null;
            bAddAddress.IsEnabled = false;
            if (dgClients.SelectedItem as Client != null) {
                editClientForm(dgClients.SelectedItem as Client);
                CFResetPassword.IsEnabled = true;
            } else {
                resetClientForm();
                bAddClient.IsEnabled = false;
                CFResetPassword.IsEnabled = false;
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

            if (AFWay.Text == String.Empty) {
                MessageBox.Show(Application.Current.Resources["mAfWayNull"] as String);
            } else if (AFWay.Text.Length < 10) {
                MessageBox.Show(Application.Current.Resources["mAfWayMin"] as String);
            } else if (AFWay.Text.Length > 100) {
                MessageBox.Show(Application.Current.Resources["mAfWayMax"] as String);
            } else if (AFComplement.Text.Length > 100) {
                MessageBox.Show(Application.Current.Resources["mAfComplementMax"] as String);
            } else if(AFZipCode.Text == String.Empty) {
                MessageBox.Show(Application.Current.Resources["mAfZipCodeNull"] as String);
            } else if (AFZipCode.Text.Length < 2) {
                MessageBox.Show(Application.Current.Resources["mAfZipCodeMin"] as String);
            } else if (AFZipCode.Text.Length > 10) {
                MessageBox.Show(Application.Current.Resources["mAfZipCodeMax"] as String);
            } else if (AFCity.Text == String.Empty) {
                MessageBox.Show(Application.Current.Resources["mAfCityNull"] as String);
            } else if (AFCity.Text.Length < 2) {
                MessageBox.Show(Application.Current.Resources["mAfCityMin"] as String);
            } else if (AFCity.Text.Length > 100) {
                MessageBox.Show(Application.Current.Resources["mAfCityMax"] as String);
            } else {
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

        private void OrderSearch_Click(object sender, RoutedEventArgs e) {
            dgOrderContent.ItemsSource = null;
            dgOrderContent.Items.Refresh();
            dgOrder.SelectedIndex = -1;
            OrderView.Refresh();
            dgOrder.Items.Refresh();
        }
        #endregion
    }
}
