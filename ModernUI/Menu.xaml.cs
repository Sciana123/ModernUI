using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ModernUI
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void quitter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Liste1_Click(object sender, RoutedEventArgs e)
        {
            Display dash = new Display();
            dash.Show();
            this.Close();
        }

        private void Liste2_Click(object sender, RoutedEventArgs e)
        {
            Liste2 list2 = new Liste2();
            list2.Show();
            this.Close();
        }

        private void Liste3_Click(object sender, RoutedEventArgs e)
        {
            Liste3 list3 = new Liste3();
            list3.Show();
            this.Close();
        }

        private void Liste4_Click(object sender, RoutedEventArgs e)
        {
            Liste4 list4 = new Liste4();
            list4.Show();
            this.Close();
        }
    }
}
