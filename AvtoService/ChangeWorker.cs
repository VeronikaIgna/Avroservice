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
        Form f11;

        private MySqlConnection Connection = new MySqlConnection(ConnectionString);

        public ChangeWorker()
        {
            InitializeComponent();
        }
        public ChangeWorker(Form f7)
        {
            InitializeComponent();
            this.f11 = f11;
            ChangePosition();
            Connection.Open();

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
