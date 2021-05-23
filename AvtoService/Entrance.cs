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
    public partial class Entrance : Form
    {
        public static string ConnectionString = $"Server=localhost;Database={Settings.DataBaseName};Uid=root;pwd={Settings.DataBasePassword};";
        private MySqlConnection Connection = new MySqlConnection(ConnectionString);

        public Entrance()
        {
            InitializeComponent();
            Connection.Open();
        }

        private void button1_Click(object sender, EventArgs e)//авторизация
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                MySqlCommand cmd = new MySqlCommand("select id_Worker, Surname_Work, Name_Work, Middlename_work, id_position from worker where Login = '" + textBox1.Text + "' and Password = '" + textBox2.Text + "'", Connection);
                int id_Worker = 0;
                MySqlDataReader reader = cmd.ExecuteReader();
                string name = "", surname = "", lastname = "";
                int id_position = -1;
                while (reader.Read())
                {
                    id_Worker = reader.GetInt32("id_Worker");
                    if (id_Worker != 0)
                    {
                        name = reader.GetString("Surname_Work");
                        surname = reader.GetString("Name_Work");
                        lastname = reader.GetString("Middlename_work");
                        id_position = reader.GetInt32("id_position");
                    }
                    break;
                }
                if (id_Worker != 0)
                {
                    if (id_position == 1)
                    {
                        MainForm f1 = new MainForm(id_Worker, name, surname, lastname, this);
                        f1.Show();
                        this.Hide();
                    }
                    else
                    {
                        FormaForTO f1 = new FormaForTO(this, id_Worker);
                        f1.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Логин или пароль не верен");
                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Поля не заполнены!");
            }
        }

        private void Entrance_Load(object sender, EventArgs e)
        {

        }
    }
}
