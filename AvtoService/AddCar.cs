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
    public partial class AddCar : Form
    {
        public static string ConnectionString = "Server=localhost;Database=avtoservice;Uid=root;pwd=MemoriesInHeart2020;charset=koi8r";
        MainForm f1;

        private MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public AddCar()
        {
            InitializeComponent();
          
        }
        public AddCar(MainForm f1)
        {
            InitializeComponent();
            this.f1 = f1;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Connection.Open();
            AddMarka();
            AddModel(1);
        }
        
        private void AddMarka() { 
        MySqlCommand obd1 = new MySqlCommand("SELECT `Name_Marka` FROM Marka", Connection);// SQL запрос для загрузки данных
        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd1);
        DataSet dt = new DataSet();
        dataAdapter.Fill(dt);// Загрузка 
        comboBox1.DisplayMember = "Name_Marka";
        comboBox1.DataSource = dt.Tables[0];
        }

        private void AddModel(int idMarka)
        {
            MySqlCommand obd1 = new MySqlCommand("SELECT `Name_Model` FROM Model where id_marka = '" +  idMarka + "'", Connection);// SQL запрос для загрузки данных
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd1);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);// Загрузка 
            comboBox2.DisplayMember = "Name_Model";
            comboBox2.DataSource = dt.Tables[0];

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.Close();
        }

        private void button1_Click(object sender, EventArgs e) // добавление данных о клиенте и автомобиле
        {
            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (textBox6.Text != "") && (textBox7.Text != ""))
            {
                string query = "Insert Into Owner (Surname_Owner, Name_Owner, MiddleName_Owner,PhoneNumber_Owner) values ('" + textBox1.Text.ToString() + "', '" + textBox2.Text.ToString() + "', '" + textBox3.Text.ToString() + "', '" + textBox4.Text.ToString() + "')";
                MySqlCommand command = new MySqlCommand(query, Connection);
                command.ExecuteNonQuery(); //выполнение запроса
                MySqlCommand cmd = new MySqlCommand("SELECT id_Owner FROM owner  ORDER BY id_Owner DESC  LIMIT 1", Connection);
                int countOwners = Convert.ToInt32(cmd.ExecuteScalar());
                MySqlCommand cmd1 = new MySqlCommand("SELECT id_Marka FROM marka  where Name_marka = '" + comboBox1.Text + "'", Connection);
                int countMarka = Convert.ToInt32(cmd1.ExecuteScalar());
                MySqlCommand cmd2 = new MySqlCommand("SELECT id_Model FROM model where Name_Model = '" + comboBox2.Text + "'", Connection);
                int countModel = Convert.ToInt32(cmd2.ExecuteScalar());
                string query2 = "Insert Into `Car` (StateNumber,Run, YearOfManufacture,id_Owner,id_Marka,id_Model) VALUES ('" + textBox7.Text.ToString() + "', '" + textBox5.Text.ToString() + "','" + textBox6.Text.ToString() + "','" + (countOwners) + "','" + (countMarka) + "','" + (countModel) + "')";
                MySqlCommand command2 = new MySqlCommand(query2, Connection);
                command2.ExecuteNonQuery();
                MessageBox.Show("Данные добавлены!");
                this.f1.LoadDataFromTableOwner();

                Close();
          
            }
            else MessageBox.Show("Клиент не добавлен! Пропущены поля!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT id_Marka FROM marka  where Name_marka = '" + comboBox1.GetItemText(comboBox1.SelectedItem) + "'", Connection);
            int countMarka = Convert.ToInt32(cmd1.ExecuteScalar());
            AddModel(countMarka);
        }

        private void button3_Click(object sender, EventArgs e)
        {   
            
            /*
             string query = "Update `Owner` set (Surname_Owner = '" + textBox1.Text.ToString() + "', Name_Owner = '" + textBox2.Text.ToString() + "', MiddleName_Owner = '" + textBox3.Text.ToString() + "',PhoneNumber_Owner = '" + textBox4.Text.ToString() + "',)";

             MySqlCommand updt = new MySqlCommand(query, Connection);
             updt.ExecuteNonQuery();
             this.f1.LoadDataFromTableOwner();*/
            
        }
    }
}
