﻿using System;
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

        private void setLanguageDictionary() {
            ResourceDictionary resDict = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString()) {
                case "en-US":
                    resDict.Source = new Uri("..\\Resources\\StringResources.en-US.xaml", UriKind.Relative);
                    break;
                case "fr-FR":
                    resDict.Source = new Uri("..\\Resources\\StringResources.fr-FR.xaml", UriKind.Relative);
                    break;
                default:
                    resDict.Source = new Uri("..\\Resources\\StringResources.en-US.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(resDict);
        }
    }
}
