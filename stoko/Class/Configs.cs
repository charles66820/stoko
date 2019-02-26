using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;

namespace stoko {
    static class Configs {

        //for get global config key
        //MessageBox.Show(Configs.Data.Global["foo"]);
        //for get section config key
        //MessageBox.Show(Configs.Data["test"]["name"]);

        public static IniData Data { get; set; }
        public static List<Lang> Langs = new List<Lang>(){
            new Lang("fr-FR", "Français"),
            new Lang("en-US", "English")
        };
        private static String configFileUri = AppDomain.CurrentDomain.BaseDirectory + @"config.ini";
        private static FileIniDataParser fileIniParser;

        /// <summary>
        /// config initialisation
        /// </summary>
        public static void Init() {
            fileIniParser = new FileIniDataParser();
            fileIniParser.Parser.Configuration.CommentString = "#";
            
            if (!File.Exists(configFileUri)) createNewConfFile();

            try {
                Data = fileIniParser.ReadFile(configFileUri);
            } catch (Exception e) {
                MessageBox.Show("erreur a la lectuer du ficher de configuration", e.Message);
            }
        }

        /// <summary>
        /// create configs file withdefault content
        /// </summary>
        private static void createNewConfFile() {
            (File.Create(configFileUri)).Close();

            IniData newData = new IniData();
            newData.Configuration.CommentString = "#";

            //for add key :
            //newData.Global.AddKey("foo", "bar");
            //for add comment to key :
            //newData.Global.GetKeyData("foo").Comments.Add("foo comment");
            //for add new section :
            //newData.Sections.AddSection("test");
            //for add comment to section :
            //newData.Sections.GetSectionData("test").Comments.Add("test comment section 1");
            //for add key to section :
            //newData.Sections.GetSectionData("test").Keys.AddKey("name", "teee");
            //for add comment to key of section :
            //newData.Sections.GetSectionData("test").Keys.GetKeyData("neuu").Comments.Add("comment neuu");

            newData.Global.AddKey("lang", Thread.CurrentThread.CurrentCulture.ToString());
            newData.Global.AddKey("darkMode", "0");
            newData.Global.AddKey("mainMenu", "0");

            fileIniParser.WriteFile(configFileUri, newData);
        }

        /// <summary>
        /// Edit confige file
        /// </summary>
        /// <param name="pKey">key name</param>
        /// <param name="pValue">new key value</param>
        /// <param name="pSection">section name</param>
        public static void EditConfigData(String pKey, String pValue, String pSection = null) {
            if (pSection == null) {
                Data.Global[pKey] = pValue;
            } else {
                Data[pSection][pKey] = pValue;
            }
            fileIniParser.WriteFile(configFileUri, Data);
        }

        /// <summary>
        /// Change app language
        /// </summary>
        /// <param name="pLang"></param>
        public static void SetLanguageDictionary(String pLang = null) {
            ResourceDictionary resDict = new ResourceDictionary();

            pLang = pLang == null ? Thread.CurrentThread.CurrentCulture.ToString() : pLang;

            switch (pLang) {
                case "en-US":
                    resDict.Source = new Uri("..\\Assets\\langs\\StringResources.en-US.xaml", UriKind.Relative);
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    break;
                case "fr-FR":
                    resDict.Source = new Uri("..\\Assets\\langs\\StringResources.fr-FR.xaml", UriKind.Relative);
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                    break;
                default:
                    resDict.Source = new Uri("..\\Assets\\langs\\StringResources.en-US.xaml", UriKind.Relative);
                    break;
            }
            Application.Current.Resources.MergedDictionaries.Add(resDict);
        }

        public static void SetDarkMode(bool p = true) {
            if (p) {
                ResourceDictionary resDict = new ResourceDictionary();
                resDict.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml", UriKind.Absolute);
                Application.Current.Resources.MergedDictionaries.Add(resDict);

                EditConfigData("darkMode", "1");
            } else {
                ResourceDictionary resDict = new ResourceDictionary();
                resDict.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml", UriKind.Absolute);
                Application.Current.Resources.MergedDictionaries.Add(resDict);

                EditConfigData("darkMode", "0");
            }
        }
    }
}
