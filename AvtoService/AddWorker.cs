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
    public partial class AddWorker : Form
    {
        public static string ConnectionString = $"Server=localhost;Database={Settings.DataBaseName};" +
            $"Uid={Settings.DataBaseUsername};" +
            $"pwd={Settings.DataBasePassword};charset=koi8r";
        Worker f8;

        private MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public AddWorker()
        {
            InitializeComponent();
        }
        public AddWorker(Worker f7)
        {
            InitializeComponent();
            this. f8= f7;
            AddPosition();
            Connection.Open();
            
        }
        private void AddPosition()
        {
            MySqlCommand obd1 = new MySqlCommand("SELECT `Name_position` FROM position", Connection);// SQL запрос для загрузки данных
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd1);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);// Загрузка 
            comboBox1.DisplayMember = "Name_position";
            comboBox1.DataSource = dt.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            {
                MySqlCommand cmd1 = new MySqlCommand("SELECT id_position FROM position where Name_position = '" + comboBox1.Text + "'", Connection);
                int idPosition = Convert.ToInt32(cmd1.ExecuteScalar());
                string insertQuery = "insert into worker(surname_work, name_work, middlename_work, phonenumber_work, id_position, login, password) " +
                    "values (" +
                    $"'{textBox1.Text}'," +
                    $"'{textBox2.Text}'," +
                    $"'{textBox3.Text}'," +
                    $"'{textBox4.Text}'," +
                    $"'{idPosition}'," +
                    $"'{textBox5.Text}'," +
                    $"'{textBox6.Text}')";
                cmd1 = new MySqlCommand(insertQuery, Connection);
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Добавлен новый сотрудник!");
                f8.LoadDataFromTableWorker();
                Close();
            }
            else
                MessageBox.Show("Не все поля заполнены!");
        }
     
        private void AddWorker_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
