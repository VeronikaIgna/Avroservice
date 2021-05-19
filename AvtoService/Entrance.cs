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
        public static string ConnectionString ="Server=localhost;Database=avtoservice;Uid=root;pwd=MemoriesInHeart2020;";
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
                MySqlCommand cmd = new MySqlCommand("select id_Worker from worker where Login = '" + textBox1.Text + "' and Password = '" + textBox2.Text + "'", Connection);
                int id_Worker = 0;
                using (var reader = cmd.ExecuteReader()) 
                {
                    
                    while (reader.Read())
                    {
                        
                        id_Worker = reader.GetInt32("id_Worker");
                        break;
                    }
                }
                if (id_Worker != 0)
                {
                    MainForm f1 = new MainForm(id_Worker, this);
                    f1.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Логин или пароль не верен");
                }
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
