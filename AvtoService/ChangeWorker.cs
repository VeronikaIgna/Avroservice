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
    public partial class ChangeWorker : Form
    {
        public static string ConnectionString = $"Server=localhost;Database={Settings.DataBaseName};" +
            $"Uid={Settings.DataBaseUsername};" +
            $"pwd={Settings.DataBasePassword};charset=koi8r";
        Worker workerForm;

        private MySqlConnection Connection = new MySqlConnection(ConnectionString);

        public ChangeWorker()
        {
            InitializeComponent();
        }

        string surname, name, lastname, phonenumber, position, login, password;

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (comboBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (textBox6.Text != ""))
            {
                MySqlCommand cmd1 = new MySqlCommand("SELECT id_position FROM position where Name_position = '" + comboBox1.Text + "'", Connection);
                int idPosition = Convert.ToInt32(cmd1.ExecuteScalar());
                
                string updateQuery = "update worker set " +
                    $"surname_work = '{textBox1.Text}'," +
                    $"name_work = '{textBox2.Text}'," +
                    $"middlename_work = '{textBox3.Text}'," +
                    $"phonenumber_work = '{textBox4.Text}'," +
                    $"id_position = '{idPosition}'," +
                    $"login = '{textBox5.Text}'," +
                    $"password = '{textBox6.Text}' where id_worker = {id_worker}";
                cmd1 = new MySqlCommand(updateQuery, Connection);
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Добавлен новый сотрудник!");
                workerForm.LoadDataFromTableWorker();
                Close();

            }
            else MessageBox.Show("Запись не добавлена! Пропущены поля!");
        }

        int id_worker;

        public ChangeWorker(Worker f7, int id_worker, string surname, string name, string lastname, string phonenumber, string position, string login, string password)
        {
            InitializeComponent();
            this.workerForm = f7;
            this.name = name;
            this.surname = surname;
            this.lastname = lastname ;
            this.phonenumber = phonenumber;
            this.position = position;
            this.login = login;
            this.password = password;
            this.id_worker = id_worker;
            ChangePosition();
            Connection.Open();

            comboBox1.SelectedIndex = comboBox1.FindStringExact(position);
            textBox1.Text = surname;
            textBox2.Text = name;
            textBox3.Text = lastname;
            textBox4.Text = phonenumber;
            textBox5.Text = login ;
            textBox6.Text = password;

        }
        private void ChangePosition()
        {
            MySqlCommand obd1 = new MySqlCommand("SELECT `Name_position` FROM position", Connection);// SQL запрос для загрузки данных
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd1);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);// Загрузка 
            comboBox1.DisplayMember = "Name_position";
            comboBox1.DataSource = dt.Tables[0];
        }
        private void AddWorker_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.Close();
        }

        private void ChangeWorker_Load(object sender, EventArgs e)
        {

        }
    }
}
