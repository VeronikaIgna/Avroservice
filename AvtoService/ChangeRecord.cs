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
    public partial class ChangeRecord : Form
    {
        public static string ConnectionString = $"Server=localhost;Database={Settings.DataBaseName};" +
            $"Uid={Settings.DataBaseUsername};" +
            $"pwd={Settings.DataBasePassword};charset=koi8r";
        Record f10;
        int SelectedRecordId;
        string SelectedService, SelectedWorker, SelectedCar, SelectedStatus;
        string timeWork, date;


        private MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public ChangeRecord()
        {
            InitializeComponent();
        }
        public ChangeRecord(Form f3, int selectedRecordId, string date, string time, string service, string worker,
            string carNubmer, string state)
        {
            InitializeComponent();
            SelectedService = service;
            timeWork = time;
            this.date = date;
            SelectedWorker = worker;
            SelectedCar = carNubmer;
            SelectedStatus = state;
            this.f10 = (Record)f3;
            Connection.Open();
            SelectedRecordId = selectedRecordId;
            LoadData();
        }

        public void LoadData()
        {
            dateTimePicker2.Value = Convert.ToDateTime(date);
            textBox2.Text = timeWork;
            
            AddStateNumber();
            AddNameServices();
            AddStatus();
            AddSurname_Work();

            comboBox1.SelectedIndex = comboBox1.FindStringExact(SelectedCar);
            comboBox2.SelectedIndex = comboBox2.FindStringExact(SelectedService);
            comboBox3.SelectedIndex = comboBox3.FindStringExact(SelectedWorker);
            comboBox4.SelectedIndex = comboBox4.FindStringExact(SelectedStatus);
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((dateTimePicker2.Text != "") && (textBox2.Text != "") && (comboBox4.Text != "") && (comboBox1.Text != "") && (comboBox2.Text != "") && (comboBox3.Text != ""))
            {
                MySqlCommand cmd1 = new MySqlCommand("SELECT id_Owner FROM Car  where StateNumber = '" + comboBox1.Text + "'", Connection);
                int idOwner = Convert.ToInt32(cmd1.ExecuteScalar());
                MySqlCommand cmd2 = new MySqlCommand("SELECT id_Services FROM services where Name_Services = '" + comboBox2.Text + "'", Connection);
                int countServices = Convert.ToInt32(cmd2.ExecuteScalar());
                MySqlCommand cmd3 = new MySqlCommand("SELECT id_Worker FROM worker where Surname_Work = '" + comboBox3.Text + "'", Connection);
                int countWorker = Convert.ToInt32(cmd3.ExecuteScalar());
                MySqlCommand cmd4 = new MySqlCommand("SELECT id_Status FROM status where Name_status = '" + comboBox4.Text + "'", Connection);
                int counStatus = Convert.ToInt32(cmd4.ExecuteScalar());

                string query = $"Update record SET Date = '{dateTimePicker2.Value.Date.ToString("yyyy-MM-dd")}'," +
                    $"Time_Work = '{textBox2.Text.ToString()}', " +
                    $"id_Status = {counStatus}, " +
                    $"id_Services = {countServices}," +
                    $"id_Worker = {countWorker},id_Owner = {idOwner} where id_record = {SelectedRecordId} ";
                MySqlCommand command = new MySqlCommand(query, Connection);
                command.ExecuteNonQuery(); //выполнение запроса
                MessageBox.Show("Данные добавлены!");
                f10.LoadDataFromTableRecord();

                Close();

            }
            else MessageBox.Show("Запись не добавлена! Пропущены поля!");
        }

        private void AddStateNumber()
        {
            MySqlCommand obd4 = new MySqlCommand("SELECT StateNumber FROM Car", Connection);// SQL запрос для загрузки данных
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd4);
            DataSet dt1 = new DataSet();
            dataAdapter.Fill(dt1);// Загрузка 
            comboBox1.DisplayMember = "StateNumber";
            comboBox1.DataSource = dt1.Tables[0];
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
    }
}
