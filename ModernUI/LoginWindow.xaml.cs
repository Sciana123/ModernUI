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
using System.Data.SqlClient;

namespace ModernUI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=isi-mtl-clara.omnivox.ca;Initial Catalog=CLARA_ISI_PROD;Persist Security Info=True;User ID=selisha;Password=)XpqlVY$&Y!Cg2t");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();
                String query = "select count(1) from Employes.Employe as emp where emp.Nom=@Username and emp.Numero=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", user.Text);
                sqlCmd.Parameters.AddWithValue("@Password", pass.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if(count==1)
                {
                    Menu men = new Menu();
                    men.Show();
                    this.Close();
                    
                }
                else
                {
                    PopUp pop = new PopUp();
                    pop.Show();
                    this.Close();
                }
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
