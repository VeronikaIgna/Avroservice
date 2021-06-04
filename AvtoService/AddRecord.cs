using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AvtoService
{
    public partial class AddRecord : Form
    {

        public static string ConnectionString = $"Server=localhost;Database=avtoservice;Uid=root;pwd={Settings.DataBasePassword};";
        Form f4;
        Record f3;


        private MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public AddRecord()
        {
            InitializeComponent();

        }

        public AddRecord(Form f3)
        {
            this.f3 = (Record)f3;
            InitializeComponent();
            Connection.Open();
            AddNameServices();
            AddStateNumber();
            AddSurname_Work();
            AddStatus();
        }
        private void AddStateNumber()
        {
            MySqlCommand obd4 = new MySqlCommand("SELECT StateNumber FROM Car", Connection);// SQL запрос для загрузки данных
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd4);
            DataSet dt1 = new DataSet();
            dataAdapter.Fill(dt1);// Загрузка 
            stateNumbersBox.DisplayMember = "StateNumber";
            stateNumbersBox.DataSource = dt1.Tables[0];
        }
        private void AddNameServices()
        {
            MySqlCommand obd4 = new MySqlCommand("SELECT `Name_Services` FROM services", Connection);// SQL запрос для загрузки данных
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd4);
            DataSet dt2 = new DataSet();
            dataAdapter.Fill(dt2);// Загрузка 
            comboBox2.DisplayMember = "Name_Services";
            comboBox2.DataSource = dt2.Tables[0];
        }

        private void AddSurname_Work()
        {
            MySqlCommand obd4 = new MySqlCommand("SELECT `Surname_Work` FROM worker", Connection);// SQL запрос для загрузки данных
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd4);
            DataSet dt3 = new DataSet();
            dataAdapter.Fill(dt3);// Загрузка 
            comboBox3.DisplayMember = "Surname_Work";
            comboBox3.DataSource = dt3.Tables[0];

        }
        private void AddStatus()
        {
            MySqlCommand obd4 = new MySqlCommand("SELECT `Name_Status` FROM status", Connection);// SQL запрос для загрузки данных
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd4);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);// Загрузка 
            comboBox4.DisplayMember = "Name_Status";
            comboBox4.DataSource = dt.Tables[0];
        }
        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)//Добавление записи
        {

            if ((dateTimePicker2.Text != "") && (textBox2.Text != "") && (comboBox4.Text != "") && (stateNumbersBox.Text != "") && (comboBox2.Text != "") && (comboBox3.Text != "") && (textBox3.Text != ""))
            {
                MySqlCommand cmd3 = new MySqlCommand("SELECT id_Worker FROM worker where Surname_Work = '" + comboBox3.Text + "'", Connection);
                int countWorker = Convert.ToInt32(cmd3.ExecuteScalar());
                if (!CheckWorkerForWorkTime(countWorker, dateTimePicker2.Value.Date.ToString("yyyy-MM-dd"), textBox2.Text.ToString()))
                {
                    MessageBox.Show("Мастер в это время занят!");
                    return;
                }

                MySqlCommand cmd1 = new MySqlCommand("SELECT id_Owner FROM Car  where StateNumber = '" + stateNumbersBox.Text + "'", Connection);
                int idOwner = Convert.ToInt32(cmd1.ExecuteScalar());
                MySqlCommand cmd2 = new MySqlCommand("SELECT id_Services FROM services where Name_Services = '" + comboBox2.Text + "'", Connection);
                int countServices = Convert.ToInt32(cmd2.ExecuteScalar());              
                MySqlCommand cmd4 = new MySqlCommand("SELECT id_Status FROM status where Name_status = '" + comboBox4.Text + "'", Connection);
                int counStatus = Convert.ToInt32(cmd4.ExecuteScalar());

                string query = $"Insert Into record (Date, Time_Work,  id_Status, id_Services,id_Worker,id_Owner, description ) values " +
                    $"('{dateTimePicker2.Value.Date.ToString("yyyy-MM-dd")}', " +
                    $"'{textBox2.Text.ToString()}'," +
                    $"'{counStatus}'," +
                    $"{countServices}," +
                    $"{countWorker}," +
                    $"{idOwner}," +
                    $"'{textBox3.Text.ToString()}')";
                MySqlCommand command = new MySqlCommand(query, Connection);
                command.ExecuteNonQuery(); //выполнение запроса
                MessageBox.Show("Данные добавлены!");
                this.f3.LoadDataFromTableRecord();

                Close();

            }
            else MessageBox.Show("Запись не добавлена! Пропущены поля!");
        }

        private bool CheckWorkerForWorkTime(int idWorker, string Date, string Time)
        {
            string checkWorkerQuery = $"SELECT * FROM record WHERE id_worker = {idWorker} AND date <= '{Date}' ORDER BY date DESC LIMIT 1";
            MySqlCommand com = new MySqlCommand(checkWorkerQuery, Connection);
            var reader = com.ExecuteReader();
            string timeWork = "";
            int idService = -1;
            while (reader.Read())
            {
                var dateWork = reader.GetDateTime("Date");
                var formattedDateWork = dateWork.ToString("yyyy-MM-dd");
                if (!formattedDateWork.Equals(Date))
                {
                    reader.Close();
                    return true;
                }
                timeWork = reader.GetString("Time_work");
                idService = reader.GetInt32("id_Services");
            }
            reader.Close();
            string CheckServiceTime = $"SELECT * FROM services WHERE id_services = {idService}";
            com = new MySqlCommand(CheckServiceTime, Connection);
            reader = com.ExecuteReader();
            string dateAndTime = Date + " " + timeWork;
            DateTime finalEndDateTime = DateTime.ParseExact(dateAndTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            DateTime currDateTime = DateTime.ParseExact(Date + " " + Time + ":00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            while (reader.Read())
            {
                int workTime = reader.GetInt32("workTime");
                TimeSpan t = TimeSpan.FromHours(workTime);
                DateTime d = new DateTime(t.Ticks);
                finalEndDateTime.Add(d.TimeOfDay);
                if (finalEndDateTime >= currDateTime)
                {
                    reader.Close();
                    return false;
                }
            }
            reader.Close();
            return true;
        }

    }
}
