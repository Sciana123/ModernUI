using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace ModernUI
{
    /// <summary>
    /// Interaction logic for Liste4.xaml
    /// </summary>
    public partial class Liste4 : Window
    {
        string connectionString = @"Data Source=isi-mtl-clara.omnivox.ca;Initial Catalog=CLARA_ISI_PROD;Persist Security Info=True;User ID=selisha;Password=)XpqlVY$&Y!Cg2t";
        public string[] names { get; set; }
        public float[] meet { get; set; }
        public int[] hours { get; set; }

        private DateTime dateTime = DateTime.Now;

        List<items> it = new List<items>();


        public Liste4()
        {
            InitializeComponent();
            fill_combo();

            //Blackout past dates
            start.BlackoutDates.AddDatesInPast();
            end.BlackoutDates.AddDatesInPast();

            meet = new float[] { 7.5F, 7, 6, 5, 4 };
            names = new string[] { "Lun, Mar, Mer", "Jeu, Ven, Sam", "Lun, Mar", "Lun, Mar, Mer, Jeu, Ven", "Mer, Jeu", "Ven, Sam" };
            hours = new int[] { 270, 90, 75, 60 };
            DataContext = this;


        }
        public class items
        {

            public string itcours { get; set; }
            public string itgroupe { get; set; }
            public DateTime itdebut { get; set; }
            public DateTime itend { get; set; }
            public string itday { get; set; }
            public float ittime { get; set; }

            public int ithours { get; set; }

            public DateTime itcancel { get; set; }
            public DateTime notefin { get; set; }
            public DateTime itrecense { get; set; }

            public string sourcefin { get; set; }

        }

        void fill_combo()
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);
            sqlCon.Open();
            string query = "select distinct baC.Numero as 'Groupe et Heures' from BanqueCours.Cours as baC inner join Groupes.Groupe as grp on baC.IDCours = grp.IDCours where grp.AnSession like '2022%' group by baC.Numero,baC.NbHeure";
            SqlCommand command = new SqlCommand(query, sqlCon);
            //command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string name = reader.GetString(0);
                cours.Items.Add(name);
            }
            sqlCon.Close();

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Menu men = new Menu();
            men.Show();
            this.Close();

        }
        //Add new Data
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Add to .csv           
            this.CreateDG.SelectAllCells();
            this.CreateDG.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, this.CreateDG);
            this.CreateDG.UnselectAllCells();
            String result = ((string)Clipboard.GetData(DataFormats.CommaSeparatedValue)).Replace(",", ";");
            try
            {
                StreamWriter sw = new StreamWriter("export.csv");
                sw.WriteLine(result);
                sw.Close();
                Process.Start("export.csv");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddGrid_Click(object sender, RoutedEventArgs e)
        {
            if ((groupe.Text.Length == 0) || (string.IsNullOrEmpty(cours.Text)) || (string.IsNullOrEmpty(totalhours.Text)) || (string.IsNullOrEmpty(time.Text)) || (string.IsNullOrEmpty(day.Text)))
            {
                MessageBox.Show("Remplissez toutes les cases s'il vous plait");

            }
            else
            {
                items tempitems = new items();
                tempitems.itcours = cours.Text;
                tempitems.itgroupe = groupe.Text;

                DateTime startdate = DateTime.Parse(start.Text);
                tempitems.itdebut = startdate;


                DateTime enddate = DateTime.Parse(end.Text);
                tempitems.itend = enddate;

                tempitems.itday = day.Text;


                float dayweek = float.Parse(time.Text);
                tempitems.ittime = dayweek;

                int tothour = int.Parse(totalhours.Text);
                tempitems.ithours = tothour;

                tempitems.sourcefin = "50";


                if ((startdate.DayOfWeek != 0) && (enddate.DayOfWeek != 0) && (enddate > startdate))
                {
                    if ((day.SelectedIndex == 0) && ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Tuesday) || (startdate.DayOfWeek == DayOfWeek.Wednesday)) && ((enddate.DayOfWeek == DayOfWeek.Monday) || (enddate.DayOfWeek == DayOfWeek.Tuesday) || (enddate.DayOfWeek == DayOfWeek.Wednesday)) ||
                        (day.SelectedIndex == 1) && ((startdate.DayOfWeek == DayOfWeek.Thursday) || (startdate.DayOfWeek == DayOfWeek.Friday) || (startdate.DayOfWeek == DayOfWeek.Saturday)) && ((enddate.DayOfWeek == DayOfWeek.Thursday) || (enddate.DayOfWeek == DayOfWeek.Friday) || (enddate.DayOfWeek == DayOfWeek.Saturday)) ||
                        (day.SelectedIndex == 2) && ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Tuesday)) && ((enddate.DayOfWeek == DayOfWeek.Monday) || (enddate.DayOfWeek == DayOfWeek.Tuesday)) ||
                        (day.SelectedIndex == 3) && ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Tuesday) || (startdate.DayOfWeek == DayOfWeek.Wednesday) || (startdate.DayOfWeek == DayOfWeek.Thursday) || (startdate.DayOfWeek == DayOfWeek.Friday)) && ((enddate.DayOfWeek == DayOfWeek.Monday) || (enddate.DayOfWeek == DayOfWeek.Tuesday) || (enddate.DayOfWeek == DayOfWeek.Wednesday) || (enddate.DayOfWeek == DayOfWeek.Thursday) || (enddate.DayOfWeek == DayOfWeek.Friday)) ||
                        (day.SelectedIndex == 4) && ((startdate.DayOfWeek == DayOfWeek.Wednesday) || (startdate.DayOfWeek == DayOfWeek.Thursday)) && ((enddate.DayOfWeek == DayOfWeek.Wednesday) || (enddate.DayOfWeek == DayOfWeek.Thursday)) ||
                        (day.SelectedIndex == 5) && ((startdate.DayOfWeek == DayOfWeek.Friday) || (startdate.DayOfWeek == DayOfWeek.Saturday)) && ((enddate.DayOfWeek == DayOfWeek.Friday) || (enddate.DayOfWeek == DayOfWeek.Saturday)))
                    {

                        //Remise notes finale
                        if ((enddate.DayOfWeek == DayOfWeek.Monday) || (enddate.DayOfWeek == DayOfWeek.Tuesday))
                        {
                            tempitems.notefin = enddate.AddDays(11);
                        }
                        else
                        {
                            tempitems.notefin = enddate.AddDays(12);
                        }

                        CreateDG.Items.Add(tempitems);
                        // Validation
                        switch (totalhours.SelectedIndex)
                        {
                            //Validation for 270 hours
                            case 0:
                                {   //Validation for 3 days
                                    if ((day.SelectedIndex == 0) || (day.SelectedIndex == 1))
                                    {

                                        switch (time.SelectedIndex)
                                        {
                                            // Validation for 7.5 hous and 7 hours -- Cancellation is 8th day
                                            case 0:
                                            case 1:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Wednesday) || (startdate.DayOfWeek == DayOfWeek.Saturday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(19);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(15);
                                                    }
                                                }
                                                break;
                                            // Validation for 6 hours -- Cancellation is 9th day
                                            case 2:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Thursday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(16);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(20);
                                                    }

                                                }
                                                break;
                                            // Validation for 5 hours -- Cancellation is 11th day
                                            case 3:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Wednesday) || (startdate.DayOfWeek == DayOfWeek.Saturday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(26);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(22);
                                                    }

                                                }
                                                break;
                                            // Validation for 4 hours -- Cancellation is 14th day
                                            case 4:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Wednesday) || (startdate.DayOfWeek == DayOfWeek.Saturday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(33);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(29);
                                                    }

                                                }
                                                break;
                                        }
                                    }
                                    //Validation for 2 days
                                    else if ((day.SelectedIndex == 2) || (day.SelectedIndex == 4) || (day.SelectedIndex == 5))
                                    {

                                        switch (time.SelectedIndex)
                                        {
                                            // Validation for 7.5 hous and 7 hours -- Cancellation is 8th day
                                            case 0:
                                            case 1:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Wednesday) || (startdate.DayOfWeek == DayOfWeek.Friday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(22);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(27);
                                                    }
                                                }
                                                break;
                                            // Validation for 6 hours -- Cancellation is 9th day
                                            case 2:
                                                {
                                                    tempitems.itcancel = startdate.AddDays(28);
                                                }
                                                break;
                                            // Validation for 5 hours -- Cancellation is 11th day
                                            case 3:
                                                {
                                                    tempitems.itcancel = startdate.AddDays(35);
                                                }
                                                break;
                                            // Validation for 4 hours -- Cancellation is 14th day
                                            case 4:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Wednesday) || (startdate.DayOfWeek == DayOfWeek.Friday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(43);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(48);
                                                    }
                                                }
                                                break;
                                        }
                                    }

                                    //Validation for 5 days
                                    else
                                    {
                                        switch (time.SelectedIndex)
                                        {
                                            // Validation for 7.5 hous and 7 hours -- Cancellation is 8th day
                                            case 0:
                                            case 1:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Tuesday) || (startdate.DayOfWeek == DayOfWeek.Wednesday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(9);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(11);
                                                    }
                                                }
                                                break;
                                            // Validation for 6 hours -- Cancellation is 9th day
                                            case 2:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Tuesday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(10);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(12);
                                                    }
                                                }
                                                break;
                                            // Validation for 5 hours -- Cancellation is 11th day
                                            case 3:
                                                {
                                                    tempitems.itcancel = startdate.AddDays(14);
                                                }
                                                break;
                                            // Validation for 4 hours -- Cancellation is 14th day
                                            case 4:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Tuesday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(14);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(19);
                                                    }
                                                }
                                                break;
                                        }
                                    }

                                }
                                break;

                            //Validation for 90 hours
                            case 1:
                                {
                                    //Validation for 3 days
                                    if ((day.SelectedIndex == 0) || (day.SelectedIndex == 1))
                                    {

                                        switch (time.SelectedIndex)
                                        {
                                            // Validation for 7.5 hours, 7 hours, 6 hours -- Cancellation is 3rd day
                                            case 0:
                                            case 1:
                                            case 2:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Thursday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(2);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(6);
                                                    }
                                                }
                                                break;
                                            // Validation for 5 hours -- Cancellation is 4th day

                                            case 3:
                                                {
                                                    tempitems.itcancel = startdate.AddDays(7);

                                                }
                                                break;
                                            // Validation for 4 hours -- Cancellation is 5th day
                                            case 4:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Wednesday) || (startdate.DayOfWeek == DayOfWeek.Saturday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(12);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(8);
                                                    }

                                                }
                                                break;
                                        }
                                    }
                                    //Validation for 2 days
                                    else if ((day.SelectedIndex == 2) || (day.SelectedIndex == 4) || (day.SelectedIndex == 5))
                                    {

                                        switch (time.SelectedIndex)
                                        {
                                            // Validation for 7.5 hours, 7 hours, 6 hours -- Cancellation is 3rd day
                                            case 0:
                                            case 1:
                                            case 2:
                                                {
                                                    tempitems.itcancel = startdate.AddDays(7);
                                                }
                                                break;
                                            // Validation for 5 hours -- Cancellation is 4th day

                                            case 3:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Wednesday) || (startdate.DayOfWeek == DayOfWeek.Friday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(8);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(13);
                                                    }

                                                }
                                                break;
                                            // Validation for 4 hours -- Cancellation is 5th day
                                            case 4:
                                                {
                                                    tempitems.itcancel = startdate.AddDays(14);
                                                }
                                                break;
                                        }
                                    }

                                    //Validation for 5 days
                                    else
                                    {
                                        switch (time.SelectedIndex)
                                        {
                                            // Validation for 7.5 hours, 7 hours, 6 hours -- Cancellation is 3rd day
                                            case 0:
                                            case 1:
                                            case 2:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Thursday) || (startdate.DayOfWeek == DayOfWeek.Friday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(4);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(2);
                                                    }
                                                }
                                                break;
                                            // Validation for 5 hours -- Cancellation is 4th day

                                            case 3:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Tuesday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(3);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(5);
                                                    }

                                                }
                                                break;
                                            // Validation for 4 hours -- Cancellation is 5th day
                                            case 4:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(4);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(6);
                                                    }
                                                }
                                                break;
                                        }
                                    }

                                }
                                break;

                            //Validation for 75 hours
                            case 2:
                                {
                                    //Validation for 3 days
                                    if ((day.SelectedIndex == 0) || (day.SelectedIndex == 1))
                                    {

                                        switch (time.SelectedIndex)
                                        {
                                            // Validation for 7.5 hours -- Cancellation is 2nd day
                                            case 0:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Wednesday) || (startdate.DayOfWeek == DayOfWeek.Saturday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(5);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(1);
                                                    }

                                                }
                                                break;
                                            // Validation for 7, 6, 5 hours -- Cancellation is 3rd day
                                            case 1:
                                            case 2:
                                            case 3:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Thursday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(2);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(6);
                                                    }
                                                }
                                                break;

                                            // Validation for 4 hours -- Cancellation is 4th day
                                            case 4:
                                                {
                                                    tempitems.itcancel = startdate.AddDays(7);
                                                }
                                                break;
                                        }
                                    }
                                    //Validation for 2 days
                                    else if ((day.SelectedIndex == 2) || (day.SelectedIndex == 4) || (day.SelectedIndex == 5))
                                    {

                                        switch (time.SelectedIndex)
                                        {
                                            // Validation for 7.5 hours -- Cancellation is 2nd day
                                            case 0:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Wednesday) || (startdate.DayOfWeek == DayOfWeek.Friday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(1);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(6);
                                                    }

                                                }
                                                break;
                                            // Validation for 7, 6, 5 hours -- Cancellation is 3rd day
                                            case 1:
                                            case 2:
                                            case 3:
                                                {
                                                    tempitems.itcancel = startdate.AddDays(7);
                                                }
                                                break;

                                            // Validation for 4 hours -- Cancellation is 4th day
                                            case 4:
                                                {

                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Wednesday) || (startdate.DayOfWeek == DayOfWeek.Friday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(8);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(13);
                                                    }
                                                }
                                                break;
                                        }
                                    }

                                    //Validation for 5 days
                                    else
                                    {
                                        switch (time.SelectedIndex)
                                        {
                                            // Validation for 7.5 hours -- Cancellation is 2nd day
                                            case 0:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Friday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(3);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(1);
                                                    }

                                                }
                                                break;
                                            // Validation for 7, 6, 5 hours -- Cancellation is 3rd day
                                            case 1:
                                            case 2:
                                            case 3:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Thursday) || (startdate.DayOfWeek == DayOfWeek.Friday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(4);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(2);
                                                    }
                                                }
                                                break;

                                            // Validation for 4 hours -- Cancellation is 4th day
                                            case 4:
                                                {

                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(3);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(5);
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;

                            //Validation for 60 hours
                            case 3:
                                {
                                    //Validation for 3 days
                                    if ((day.SelectedIndex == 0) || (day.SelectedIndex == 1))
                                    {


                                        switch (time.SelectedIndex)
                                        {
                                            // Validation for 7.5, 7, 6 hours -- Cancellation is 2nd day
                                            case 0:
                                            case 1:
                                            case 2:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Wednesday) || (startdate.DayOfWeek == DayOfWeek.Saturday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(5);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(1);
                                                    }
                                                }
                                                break;

                                            // Validation for 5,4 hours -- Cancellation is 3rd day
                                            case 3:
                                            case 4:
                                                {

                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Thursday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(2);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(6);
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    //Validation for 2 days
                                    else if ((day.SelectedIndex == 2) || (day.SelectedIndex == 4) || (day.SelectedIndex == 5))
                                    {

                                        switch (time.SelectedIndex)
                                        {
                                            // Validation for 7.5, 7, 6 hours -- Cancellation is 2nd day
                                            case 0:
                                            case 1:
                                            case 2:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Monday) || (startdate.DayOfWeek == DayOfWeek.Wednesday) || (startdate.DayOfWeek == DayOfWeek.Friday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(1);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(6);
                                                    }
                                                }
                                                break;

                                            // Validation for 5,4 hours -- Cancellation is 3rd day
                                            case 3:
                                            case 4:
                                                {
                                                    tempitems.itcancel = startdate.AddDays(7);
                                                }
                                                break;
                                        }
                                    }
                                    //Validation for 5 days
                                    else
                                    {
                                        switch (time.SelectedIndex)
                                        {
                                            // Validation for 7.5, 7, 6 hours -- Cancellation is 2nd day
                                            case 0:
                                            case 1:
                                            case 2:
                                                {
                                                    if ((startdate.DayOfWeek == DayOfWeek.Friday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(3);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(1);
                                                    }
                                                }
                                                break;

                                            // Validation for 5,4 hours -- Cancellation is 3rd day
                                            case 3:
                                            case 4:
                                                {

                                                    if ((startdate.DayOfWeek == DayOfWeek.Thursday) || (startdate.DayOfWeek == DayOfWeek.Friday))
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(4);
                                                    }
                                                    else
                                                    {
                                                        tempitems.itcancel = startdate.AddDays(2);
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Veuillez sélectionner la bonne date");
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner la bonne date");
                }

                if (tempitems.itcancel.DayOfWeek == DayOfWeek.Saturday)
                {
                    tempitems.itrecense = tempitems.itcancel.AddDays(2);
                }
                else
                {
                    tempitems.itrecense = tempitems.itcancel.AddDays(1);
                }


            }

        }
    }
}
