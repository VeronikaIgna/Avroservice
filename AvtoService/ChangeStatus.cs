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
    public partial class ChangeStatus : Form
    {
        public static string ConnectionString = "Server=localhost;Database=avtoservice;Uid=root;pwd=MemoriesInHeart2020;charset=koi8r";
        Form f6;

        private MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public ChangeStatus()
        {
            InitializeComponent();
        }
        public ChangeStatus(Form f3)
        {
            InitializeComponent();
            this.f6 = f6;
            AddStatus();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Connection.Open();
            
           
        }
        private void AddStatus()
        {
            MySqlCommand obd1 = new MySqlCommand("SELECT `Name_Status` FROM status", Connection);// SQL запрос для загрузки данных
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd1);
            DataSet dt = new DataSet();
            dataAdapter.Fill(dt);// Загрузка 
            comboBox1.DisplayMember = "Name_Status";
            comboBox1.DataSource = dt.Tables[0];
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
