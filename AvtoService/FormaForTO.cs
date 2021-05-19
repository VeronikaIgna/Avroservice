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
    public partial class FormaForTO : Form
    {
        public static string ConnectionString = $"Server=localhost;Database={Settings.DataBaseName};" +
            $"Uid={Settings.DataBaseUsername};" +
            $"pwd={Settings.DataBasePassword};charset=koi8r";
        Form f9;

        private MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public FormaForTO()
        {
            InitializeComponent();
            Connection.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Entrance entrance = new Entrance();
            entrance.Show();
            this.Hide();
        }

        private void FormaForTO_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.Close();
        }
    }
}
