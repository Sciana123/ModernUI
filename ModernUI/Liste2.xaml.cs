using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Data.SqlClient;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace ModernUI
{
    /// <summary>
    /// Interaction logic for Liste2.xaml
    /// </summary>
    public partial class Liste2 : Window
    {
        string connectionString = @"Data Source=isi-mtl-clara.omnivox.ca;Initial Catalog=CLARA_ISI_PROD;Persist Security Info=True;User ID=selisha;Password=)XpqlVY$&Y!Cg2t";
        public string[] names { get; set; }
        public Liste2()
        {
            InitializeComponent();
            names = new string[] { "Automne 2021", "Été 2021", "Hiver 2021", "Automne 2020", };
            DataContext = this;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Menu men = new Menu();
            men.Show();
            this.Close();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                if (cb.SelectedIndex == 0)
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter("select distinct etu.NomPrenom as Nom,etu.CodePermanent as 'Code Permanent' from etudiants.etudiant as etu inner join etudiants.EtudiantSession as etuSess on etuSess.IDEtudiant = etu.IDEtudiant and etuSess.AnSession = '20213' left join etudiants.EtudiantAdresse as etuAdr on etuSess.IDEtudiant = etuAdr.IDEtudiant WHERE etuAdr.IDAdresse NOT IN (select etuAdr.IDAdresse from etudiants.EtudiantAdresse where etuAdr.IDAdresse is not null)", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dgv2.ItemsSource = dtbl.DefaultView;
                }
                else if (cb.SelectedIndex == 1)
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter("select distinct etu.NomPrenom as Nom,etu.CodePermanent  as 'Code Permanent' from etudiants.etudiant as etu inner join etudiants.EtudiantSession as etuSess on etuSess.IDEtudiant = etu.IDEtudiant and etuSess.AnSession = '20212' left join etudiants.EtudiantAdresse as etuAdr on etuSess.IDEtudiant = etuAdr.IDEtudiant WHERE etuAdr.IDAdresse NOT IN (select etuAdr.IDAdresse from etudiants.EtudiantAdresse where etuAdr.IDAdresse is not null)", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dgv2.ItemsSource = dtbl.DefaultView;
                }
                else if (cb.SelectedIndex == 2)
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter("select distinct etu.NomPrenom as Nom,etu.CodePermanent  as 'Code Permanent' from etudiants.etudiant as etu inner join etudiants.EtudiantSession as etuSess on etuSess.IDEtudiant = etu.IDEtudiant and etuSess.AnSession = '20211' left join etudiants.EtudiantAdresse as etuAdr on etuSess.IDEtudiant = etuAdr.IDEtudiant WHERE etuAdr.IDAdresse NOT IN (select etuAdr.IDAdresse from etudiants.EtudiantAdresse where etuAdr.IDAdresse is not null)", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dgv2.ItemsSource = dtbl.DefaultView;
                }
                else
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter("select distinct etu.NomPrenom as Nom,etu.CodePermanent  as 'Code Permanent' from etudiants.etudiant as etu inner join etudiants.EtudiantSession as etuSess on etuSess.IDEtudiant = etu.IDEtudiant and etuSess.AnSession = '20203' left join etudiants.EtudiantAdresse as etuAdr on etuSess.IDEtudiant = etuAdr.IDEtudiant WHERE etuAdr.IDAdresse NOT IN (select etuAdr.IDAdresse from etudiants.EtudiantAdresse where etuAdr.IDAdresse is not null)", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dgv2.ItemsSource = dtbl.DefaultView;
                }


            }
        }
    }
}
