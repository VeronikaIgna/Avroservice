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
        public static string ConnectionString = "Server=localhost;Database=avtoservice;Uid=root;pwd=MemoriesInHeart2020;";
        Form f8;

        private MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public AddWorker()
        {
            InitializeComponent();
        }
        public AddWorker(Form f7)
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
