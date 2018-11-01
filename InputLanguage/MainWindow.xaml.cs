using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InputLanguageTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InputLanguage[] langs;
        int langIndex = 0;

        public MainWindow()
        {
            InitializeComponent();

            langs = new InputLanguage[InputLanguage.InstalledInputLanguages.Count];
            InputLanguage.InstalledInputLanguages.CopyTo(langs, 0);
            InputLanguage.CurrentInputLanguage = langs[langIndex];
            langLebel.Content = InputLanguage.CurrentInputLanguage.LayoutName;
        }

        private void clrBtn_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
        }

        private void inputLangBtn_Click(object sender, RoutedEventArgs e)
        {
            langIndex++;
            if (langIndex >= InputLanguage.InstalledInputLanguages.Count)
            {
                langIndex = 0;
            }
            
            InputLanguage.CurrentInputLanguage = langs[langIndex];
            langLebel.Content = InputLanguage.CurrentInputLanguage.LayoutName;
        }
    }
}
