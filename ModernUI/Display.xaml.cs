using System.Windows;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace ModernUI
{
    /// <summary>
    /// Interaction logic for Display.xaml
    /// </summary>
    public partial class Display : Window
    {
        string connectionString = @"Data Source=isi-mtl-clara.omnivox.ca;Initial Catalog=CLARA_ISI_PROD;Persist Security Info=True;User ID=selisha;Password=)XpqlVY$&Y!Cg2t";
       public string[] names { get; set; }
     

        
        public Display()
        {
           InitializeComponent();            
           names = new string[] { "Hiver 2021", "Automne 2020", "Été 2020", "Hiver 2020" };
           DataContext = this;

           
        }

       
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                if (cb.SelectedIndex == 0)
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter("select distinct  cohorte.Titre as Cohorte ,bac.Numero,bac.TitreCourt as 'Titre du cours',ipr.IDGroupe, Échec, count(ipr.IDInscription) as Inscriptions, concat(Échec, ' / ', count(ipr.IDInscription)) as Total,(Échec * 100/ count(ipr.IDInscription)) as Pourcentage from Groupes.CohorteFC as cohorte inner join Groupes.Groupe as grp on cohorte.IDCohorteFC = grp.IDCohorteFC inner join BanqueCours.Cours as bac on grp.IDCours = bac.IDCours inner join Inscriptions.inscription as ipr on grp.IDGroupe = ipr.IDGroupe inner join Etudiants.EtudiantSession as etuSess on etuSess.IDEtudiantSession = ipr.IDEtudiantSession inner join BanqueCours.Cours on cours.IDCours = ipr.IDCours left join (select new.IDCours, count(new.IDInscription) as Échec, new.IDGroupe from Inscriptions.Inscription as new where new.CodeRemarque = 'EC' and new.IndicateurSupprime = 0 group by new.IDCours, new.IDGroupe) as temp on ipr.IDGroupe = temp.IDGroupe where grp.IDUniteOrg = 1281 and etuSess.AnSession = 20211 and ipr.IndicateurSupprime = 0 and Échec is not NULL group by cohorte.Titre, bac.Numero, bac.TitreCourt, ipr.IDGroupe, Échec", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dgv1.ItemsSource = dtbl.DefaultView;
                }
                else if (cb.SelectedIndex == 1)
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter("select distinct  cohorte.Titre as Cohorte ,bac.Numero,bac.TitreCourt as 'Titre du cours',ipr.IDGroupe, Échec, count(ipr.IDInscription) as Inscriptions, concat(Échec, ' / ', count(ipr.IDInscription)) as Total,(Échec * 100/ count(ipr.IDInscription)) as Pourcentage from Groupes.CohorteFC as cohorte inner join Groupes.Groupe as grp on cohorte.IDCohorteFC = grp.IDCohorteFC inner join BanqueCours.Cours as bac on grp.IDCours = bac.IDCours inner join Inscriptions.inscription as ipr on grp.IDGroupe = ipr.IDGroupe inner join Etudiants.EtudiantSession as etuSess on etuSess.IDEtudiantSession = ipr.IDEtudiantSession inner join BanqueCours.Cours on cours.IDCours = ipr.IDCours left join (select new.IDCours, count(new.IDInscription) as Échec, new.IDGroupe from Inscriptions.Inscription as new where new.CodeRemarque = 'EC' and new.IndicateurSupprime = 0 group by new.IDCours, new.IDGroupe) as temp on ipr.IDGroupe = temp.IDGroupe where grp.IDUniteOrg = 1281 and etuSess.AnSession = 20203 and ipr.IndicateurSupprime = 0 and Échec is not NULL group by cohorte.Titre, bac.Numero, bac.TitreCourt, ipr.IDGroupe, Échec", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dgv1.ItemsSource = dtbl.DefaultView;
                }
                else if (cb.SelectedIndex == 2)
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter("select distinct  cohorte.Titre as Cohorte ,bac.Numero,bac.TitreCourt as 'Titre du cours',ipr.IDGroupe, Échec, count(ipr.IDInscription) as Inscriptions, concat(Échec, ' / ', count(ipr.IDInscription)) as Total,(Échec * 100/ count(ipr.IDInscription)) as Pourcentage from Groupes.CohorteFC as cohorte inner join Groupes.Groupe as grp on cohorte.IDCohorteFC = grp.IDCohorteFC inner join BanqueCours.Cours as bac on grp.IDCours = bac.IDCours inner join Inscriptions.inscription as ipr on grp.IDGroupe = ipr.IDGroupe inner join Etudiants.EtudiantSession as etuSess on etuSess.IDEtudiantSession = ipr.IDEtudiantSession inner join BanqueCours.Cours on cours.IDCours = ipr.IDCours left join (select new.IDCours, count(new.IDInscription) as Échec, new.IDGroupe from Inscriptions.Inscription as new where new.CodeRemarque = 'EC' and new.IndicateurSupprime = 0 group by new.IDCours, new.IDGroupe) as temp on ipr.IDGroupe = temp.IDGroupe where grp.IDUniteOrg = 1281 and etuSess.AnSession = 20202 and ipr.IndicateurSupprime = 0 and Échec is not NULL group by cohorte.Titre, bac.Numero, bac.TitreCourt, ipr.IDGroupe, Échec", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dgv1.ItemsSource = dtbl.DefaultView;
                }
                else
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter("select distinct  cohorte.Titre as Cohorte ,bac.Numero,bac.TitreCourt as 'Titre du cours',ipr.IDGroupe, Échec, count(ipr.IDInscription) as Inscriptions, concat(Échec, ' / ', count(ipr.IDInscription)) as Total,(Échec * 100/ count(ipr.IDInscription)) as Pourcentage from Groupes.CohorteFC as cohorte inner join Groupes.Groupe as grp on cohorte.IDCohorteFC = grp.IDCohorteFC inner join BanqueCours.Cours as bac on grp.IDCours = bac.IDCours inner join Inscriptions.inscription as ipr on grp.IDGroupe = ipr.IDGroupe inner join Etudiants.EtudiantSession as etuSess on etuSess.IDEtudiantSession = ipr.IDEtudiantSession inner join BanqueCours.Cours on cours.IDCours = ipr.IDCours left join (select new.IDCours, count(new.IDInscription) as Échec, new.IDGroupe from Inscriptions.Inscription as new where new.CodeRemarque = 'EC' and new.IndicateurSupprime = 0 group by new.IDCours, new.IDGroupe) as temp on ipr.IDGroupe = temp.IDGroupe where grp.IDUniteOrg = 1281 and etuSess.AnSession = 20201 and ipr.IndicateurSupprime = 0 and Échec is not NULL group by cohorte.Titre, bac.Numero, bac.TitreCourt, ipr.IDGroupe, Échec", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dgv1.ItemsSource = dtbl.DefaultView;
                }


            }

        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            //Close Button
            this.Close();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Menu men = new Menu();
            men.Show();
            this.Close();
        }
    }
}
