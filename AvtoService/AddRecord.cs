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
        public static string ConnectionString = "Server=localhost;Database=avtoservice;Uid=root;pwd=MemoriesInHeart2020;";
        Form f4;

        private MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public AddRecord()
        {
            InitializeComponent();
           
        }
        public AddRecord(Form f3)
        {
            InitializeComponent();
            this.f4 = f4;
            Connection.Open();
            AddStateNumber();
            AddNameServices();
            AddSurname_Work();
            AddStatus();
        }
        private void AddStateNumber()
        {
            MySqlCommand obd4 = new MySqlCommand("SELECT `StateNumber` FROM Car", Connection);// SQL запрос для загрузки данных
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd4);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);// Загрузка 
            comboBox1.DisplayMember = "StateNumber";
            comboBox1.DataSource = dt.Tables[0];
        }
        private void AddNameServices()
        {
            MySqlCommand obd4 = new MySqlCommand("SELECT `Name_Services` FROM services", Connection);// SQL запрос для загрузки данных
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd4);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);// Загрузка 
            comboBox2.DisplayMember = "Name_Services";
            comboBox2.DataSource = dt.Tables[0];
        }

        private void AddSurname_Work()
        {
            MySqlCommand obd4 = new MySqlCommand("SELECT `Surname_Work` FROM worker", Connection);// SQL запрос для загрузки данных
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd4);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);// Загрузка 
            comboBox3.DisplayMember = "Surname_Work";
            comboBox3.DataSource = dt.Tables[0];

        }
        private void AddStatus()
        {
            MySqlCommand obd4 = new MySqlCommand("SELECT `Name_Status` FROM status", Connection);// SQL запрос для загрузки данных
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd4);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);// Загрузка 
            comboBox1.DisplayMember = "Name_Status";
            comboBox1.DataSource = dt.Tables[0];
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
            
                if ((dateTimePicker2.Text != "") && (textBox2.Text != "") && (comboBox4.Text != "") && (comboBox1.Text != "") && (comboBox2.Text != "") && (comboBox3.Text != ""))
                {
                    string query = "Insert Into record (Date, Time_Work, Status,) values ('" + dateTimePicker2.Text.ToString() + "', '" + textBox2.Text.ToString() + "', '" + comboBox4.Text.ToString() + "')";
                    MySqlCommand command = new MySqlCommand(query, Connection);
                    command.ExecuteNonQuery(); //выполнение запроса
                    MySqlCommand cmd = new MySqlCommand("SELECT id_Record FROM record  ORDER BY id_Owner DESC  LIMIT 1", Connection);
                    int countRecords = Convert.ToInt32(cmd.ExecuteScalar());
                    MySqlCommand cmd1 = new MySqlCommand("SELECT id_Car FROM Car  where StateNumber = '" + comboBox1.Text + "'", Connection);
                    int countStateNumber = Convert.ToInt32(cmd1.ExecuteScalar());
                    MySqlCommand cmd2 = new MySqlCommand("SELECT id_Services FROM services where Name_Services = '" + comboBox2.Text + "'", Connection);
                    int countServices = Convert.ToInt32(cmd2.ExecuteScalar());
                    MySqlCommand cmd3 = new MySqlCommand("SELECT id_Worker FROM worker where Surname_Work = '" + comboBox3.Text + "'", Connection);
                    int countWorker = Convert.ToInt32(cmd2.ExecuteScalar());
                    MySqlCommand cmd4 = new MySqlCommand("SELECT id_Status FROM status where Name_status = '" + comboBox4.Text + "'", Connection);
                     int counStatus = Convert.ToInt32(cmd.ExecuteScalar());
                MessageBox.Show("Данные добавлены!");
                    //this.f4.LoadDataFromTableRecord();

                    Close();

                }
                else MessageBox.Show("Запись не добавлена! Пропущены поля!");
            }

        }
    }
