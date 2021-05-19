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
        Form f10;

        private MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public ChangeRecord()
        {
            InitializeComponent();
        }
        public ChangeRecord(Form f3)
        {
            InitializeComponent();
            this.f10 = f10;
            Connection.Open();
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
